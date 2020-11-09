using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace BoatingMangementSystem
{
    public class ReportPrintService
    {
        #region Properties
        public PrintDocument PrintDocument { get; set; }

        public List<ReportPrintDto> PrintDataList { get; set; }

        public string ReportDate { get; set; }

        public DataGridView PrintTable { get; set; }

        public DataGridViewPrinter DataGridViewPrinter { get; set; }

        #endregion Properties

        public ReportPrintService(List<ReportPrintDto> printDto, string reportDate)
        {
            PrintDocument = new PrintDocument();
            PrintDocument.PrintPage += PrintDocument_PrintPage;
            PrintDataList = printDto;
            ReportDate = reportDate;
            PopulateDataTable();
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            bool more = DataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }

        private void PopulateDataTable()
        {
            PrintTable = new DataGridView { AllowUserToAddRows = false };
            PrintTable.Columns.Add("Type", "Type");
            PrintTable.Columns.Add("Adult", "Adult");
            PrintTable.Columns.Add("Child", "Child");
            PrintTable.Columns.Add("Total", "Total");

            // Populate the table 
            foreach (ReportPrintDto printDto in PrintDataList)
            {
                PrintTable.Rows.Add(printDto.Type, printDto.AdultCount, printDto.ChildCount, printDto.Total);
            }
        }

        public bool SetupPrinter()
        {
            PrintTable.Font = new Font("Arial", 10);
            PrintTable.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            // Set header style for grand total row.
            PrintTable.Rows[PrintTable.Rows.Count - 1].DefaultCellStyle = PrintTable.ColumnHeadersDefaultCellStyle;
            
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;

            if (MyPrintDialog.ShowDialog() != DialogResult.OK)
                return false;

            PrintDocument.DocumentName = "Ticket Report";
            PrintDocument.PrinterSettings =
                                MyPrintDialog.PrinterSettings;
            PrintDocument.DefaultPageSettings =
            MyPrintDialog.PrinterSettings.DefaultPageSettings;
            PrintDocument.DefaultPageSettings.Margins =
                             new Margins(40, 40, 40, 40);

            PrintTable.Width = MyPrintDialog.PrinterSettings.DefaultPageSettings.Bounds.Width - PrintDocument.DefaultPageSettings.Margins.Left;
            DataGridViewPrinter = new DataGridViewPrinter(PrintTable,
                PrintDocument, false, true, "Reports for: " + ReportDate, new Font("Tahoma", 12,
                FontStyle.Regular, GraphicsUnit.Point), Color.Black, true);

            return true;
        }
    }
}
