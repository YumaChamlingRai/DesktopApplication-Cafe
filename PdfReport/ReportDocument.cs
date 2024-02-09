using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bislerium_Cafe.Models;
using Microsoft.Maui.Controls;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Colors = QuestPDF.Helpers.Colors;
using IContainer = QuestPDF.Infrastructure.IContainer;


namespace Bislerium_Cafe.PdfReport
{

    public class ReportDocument : IDocument
    {
        public Report ReportObj;

        public ReportDocument(Report model)
        {
            ReportObj = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {

            container
            .Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.DefaultTextStyle(x => x.FontSize(10));
                page.Header().Text("Bislerium Cafe Cales Transaction Report").FontColor("#8B4513").FontSize(18).FontFamily("Times New Roman");
                page.Content().Element(ComposeContent);
            });
        }

   
        // This method  of the PDF content.
        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(20).Column(column =>
            {

                

                string pdfTitle = "Bislerium Cafe Top 5 Most Purchased Coffee";

                column.Item().Text(pdfTitle).Bold();

                column.Item().PaddingTop(7).Element(ComposeMostPurchasedAddInsItemTable);
                column.Item().PaddingTop(7).Element(ComposeMostPurchasedCoffesTable);

                // Sales Transactions
                column.Item().PaddingTop(20).Element(ComposeSalesTransactionsHeader);
                column.Item().PaddingTop(10).Element(ComposeSalesTransactionsTable);

            });
        }

   

        // This method composes the header of the Sales Transactions table.
        void ComposeSalesTransactionsHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(13).SemiBold();

            string title = "Bislerium Cafe Cales Transaction Report";

            container.Row(row =>
            {

                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"{title}").Style(titleStyle);

                    column.Item().PaddingTop(2).Text(text =>
                    {
                        text.Span("Total Revenue of Bislerium Cafe: ").FontColor("#8B4513").FontSize(15).FontFamily("Times New Roman");
                        text.Span($"Rs. {ReportObj.TotalRevenue}").FontColor("#8B4513").FontSize(15).FontFamily("Times New Roman");
                    });
                });
            });

        }

        // This method generates the Sales Transactions table.
        void ComposeSalesTransactionsTable(IContainer container)
        {
            container.Table(table =>
            {
                
                table.ColumnsDefinition(columns =>
                {

                    columns.ConstantColumn(20);
                    columns.ConstantColumn(130);
                    columns.ConstantColumn(90);
                    columns.ConstantColumn(80);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();

                });

               
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Customer Name");
                    header.Cell().Element(CellStyle).Text("Customer Phone Number");
                    header.Cell().Element(CellStyle).Text("Employee");
                    header.Cell().Element(CellStyle).Text("Total Amount");
                    header.Cell().Element(CellStyle).Text("Discount Amount");
                    header.Cell().Element(CellStyle).Text("Grand Total");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                
                foreach (var order in ReportObj.Orders)
                {
                    table.Cell().Element(CellStyle).Text((ReportObj.Orders.IndexOf(order) + 1).ToString());
                    table.Cell().Element(CellStyle).Text(order.CustomerName);
                    table.Cell().Element(CellStyle).Text(order.CustomerPhoneNum);
                    table.Cell().Element(CellStyle).Text(order.EmployeeUserName);
                    table.Cell().Element(CellStyle).Text($"Rs.{order.OrderTotalAmount}");
                    table.Cell().Element(CellStyle).Text($"Rs.{order.DiscountAmount}");
                    table.Cell().Element(CellStyle).Text($"Rs.{order.OrderTotalAmount - order.DiscountAmount}");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }


        // This method generates the Top 5 Most Purchased Coffees table.
        void ComposeMostPurchasedCoffesTable(IContainer container)
        {
            container.Table(table =>
            {            
                table.ColumnsDefinition(columns =>
                {

                    columns.ConstantColumn(20);
                    columns.ConstantColumn(150);
                    columns.ConstantColumn(70);

                });
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Bislerium Coffee Type");
                    header.Cell().Element(CellStyle).Text("Quantity");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                foreach (var coffee in ReportObj.CoffeeList)
                {
                    table.Cell().Element(CellStyle).Text((ReportObj.CoffeeList.IndexOf(coffee) + 1).ToString());
                    table.Cell().Element(CellStyle).Text(coffee.ItemName);
                    table.Cell().Element(CellStyle).Text(coffee.Quantity.ToString());

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

        void ComposeMostPurchasedAddInsItemTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {

                    columns.ConstantColumn(20);
                    columns.ConstantColumn(150);
                    columns.ConstantColumn(70);

                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Add-In Item Name");
                    header.Cell().Element(CellStyle).Text("Quantity");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var addInItem in ReportObj.AddInsList)
                {
                    table.Cell().Element(CellStyle).Text((ReportObj.AddInsList.IndexOf(addInItem) + 1).ToString());
                    table.Cell().Element(CellStyle).Text(addInItem.ItemName);
                    table.Cell().Element(CellStyle).Text(addInItem.Quantity.ToString());

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

    }
}
