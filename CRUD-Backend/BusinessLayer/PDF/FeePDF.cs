using DatabaseLayer.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.PDF
{


    public class FeePDF : IDocument
    {
        private readonly GetFee _fee;
        public FeePDF(GetFee fee)
        {
            this._fee = fee;
        }
        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(50);
                    page.Header().Element(ComposeHeader);
                    page.Content().TranslateY(50).AlignTop().Element(ComposeTable);
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"Invoice #{_fee.FeeId}").Style(titleStyle);

                    column.Item().Text(text =>
                    {
                        text.Span("Paid date: ").SemiBold();
                        text.Span($"{DateTime.Now:d}");
                    });

                  
                });

                row.ConstantItem(100).Height(50).Placeholder();
            });
        }

        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                
                // step 1
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3);
                    columns.RelativeColumn();
                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Fee type");
                    header.Cell().Element(CellStyle).AlignRight().Text("Fee Amount");
                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });


                table.Cell().Element(CellStyle).Text("Bus Fee");
                table.Cell().Element(CellStyle).AlignRight().Text($"{this._fee.BusFee}");
                table.Cell().Element(CellStyle).Text("Exam Fee");
                table.Cell().Element(CellStyle).AlignRight().Text($"{_fee.ExamFee}");
                table.Cell().Element(CellStyle).Text("Tution Fee");
                table.Cell().Element(CellStyle).AlignRight().Text($"{_fee.TutionFee}");
                table.Cell().Element(TotalStyle).Text("Total");
                table.Cell().Element(TotalStyle).AlignRight().Text($"{_fee.TotalFee}")
                .FontColor(Colors.Green.Darken4).ExtraBold();


                static IContainer CellStyle(IContainer container)
                {
                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }

                static IContainer TotalStyle(IContainer container)
                {
                    return container.PaddingVertical(5);
                }

            });
        }

    }
}
