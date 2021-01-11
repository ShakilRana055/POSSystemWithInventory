using Microsoft.EntityFrameworkCore.Migrations;

namespace POSSystemWithInventory.Migrations
{
    public partial class SalesInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesInvoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    GrandTotal = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    Dues = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VatAndTaxId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_SalesInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesInvoice_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesInvoice_VatAndTax_VatAndTaxId",
                        column: x => x.VatAndTaxId,
                        principalTable: "VatAndTax",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoiceDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalesInvoiceId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
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
                    table.PrimaryKey("PK_SalesInvoiceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesInvoiceDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesInvoiceDetail_SalesInvoice_SalesInvoiceId",
                        column: x => x.SalesInvoiceId,
                        principalTable: "SalesInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoice_CustomerId",
                table: "SalesInvoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoice_VatAndTaxId",
                table: "SalesInvoice",
                column: "VatAndTaxId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceDetail_ProductId",
                table: "SalesInvoiceDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceDetail_SalesInvoiceId",
                table: "SalesInvoiceDetail",
                column: "SalesInvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesInvoiceDetail");

            migrationBuilder.DropTable(
                name: "SalesInvoice");
        }
    }
}
