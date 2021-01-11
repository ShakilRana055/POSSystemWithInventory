﻿using Microsoft.AspNetCore.Mvc.Rendering;
using POSSystemWithInventory.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace POSSystemWithInventory.ConstantAndHelpers
{
    public static class POSHelper
    {
        public static List<SelectListItem> Brand(List<Brand>brands)
        {
            List<SelectListItem> brandsSelectList = new List<SelectListItem>();
            brandsSelectList.AddRange(brands.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }));
            return brandsSelectList;
        }
        public static List<SelectListItem> Category(List<Category> categories)
        {
            List<SelectListItem> categorySelectList = new List<SelectListItem>();
            categorySelectList.AddRange(categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }));
            return categorySelectList;
        }
        public static List<SelectListItem> Unit(List<Unit> units)
        {
            List<SelectListItem> unitSelectList = new List<SelectListItem>();
            unitSelectList.AddRange(units.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.ShortForm }));
            return unitSelectList;
        }
        public static List<SelectListItem> Supplier(List<Supplier> supplier)
        {
            List<SelectListItem> supplierSelectList = new List<SelectListItem>();
            supplierSelectList.AddRange(supplier.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = (x.Name +"|" + x.Phone) }));
            return supplierSelectList;
        }
        public static List<SelectListItem> Customer(List<Customer> customer)
        {
            List<SelectListItem> customerSelectList = new List<SelectListItem>();
            customerSelectList.AddRange(customer.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = (x.Name +"|" + x.Phone) }));
            return customerSelectList;
        }
        public static List<SelectListItem> Product(List<Product> products)
        {
            List<SelectListItem> productSelectList = new List<SelectListItem>();
            productSelectList.AddRange(products.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }));
            return productSelectList;
        }
        public static List<SelectListItem> VatAndTax(List<VatAndTax> vatAndTaxes)
        {
            List<SelectListItem> vatAndTaxSelectList = new List<SelectListItem>();
            vatAndTaxSelectList.AddRange(vatAndTaxes.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name + "|" + x.Amount }));
            return vatAndTaxSelectList;
        }

        public static string DecimalToText(string decimalPart)
        {
            string[] digits = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            string result = "";
            foreach (char c in decimalPart)
            {
                int i = (int)c - 48;
                if (i < 0 || i > 9) return ""; // invalid number, don't return anything
                result += " " + digits[i];
            }
            return result;
        }
        public static string NumberToText(int number, bool useAnd)
        {
            if (number == 0) return "Zero";

            if (number == -2147483648) return "Minus Two Hundred " + "and " + "Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred " + "and " + "Forty Eight";

            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }

            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;

                u = num[i] % 10; // ones 
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens

                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i < first) sb.Append("and ");

                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }

            string temp = sb.ToString().TrimEnd();


            return temp;
        }
        public static string AmountInWords(decimal number)
        {
            int firstPart = (int)number;
            string decimalPart = number.ToString().Split('.')[1];
            string text = NumberToText(firstPart, true);
            if (decimalPart != "00")
            {
                text += " Point" + DecimalToText(decimalPart);
            }
            return text;
        }
    }
}
