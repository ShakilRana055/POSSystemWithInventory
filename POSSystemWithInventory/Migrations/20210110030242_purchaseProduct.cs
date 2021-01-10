using Microsoft.EntityFrameworkCore.Migrations;

namespace POSSystemWithInventory.Migrations
{
    public partial class purchaseProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    GrandTotal = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    Dues = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseProduct_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseProductDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseProductId = table.Column<int>(type: "int", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    VatAndTaxId = table.Column<int>(type: "int", nullable: true),
                    BasePrice = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    SellPrice = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseProductDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseProductDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseProductDetail_PurchaseProduct_PurchaseProductId",
                        column: x => x.PurchaseProductId,
                        principalTable: "PurchaseProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseProductDetail_VatAndTax_VatAndTaxId",
                        column: x => x.VatAndTaxId,
                        principalTable: "VatAndTax",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProduct_SupplierId",
                table: "PurchaseProduct",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProductDetail_ProductId",
                table: "PurchaseProductDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProductDetail_PurchaseProductId",
                table: "PurchaseProductDetail",
                column: "PurchaseProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProductDetail_VatAndTaxId",
                table: "PurchaseProductDetail",
                column: "VatAndTaxId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseProductDetail");

            migrationBuilder.DropTable(
                name: "PurchaseProduct");
        }
    }
}
