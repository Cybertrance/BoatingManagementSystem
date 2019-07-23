using System;

namespace BoatingMangementSystem
{
    class PrintService
    {
        String ticketNumber;
        String ticketType;
        int adultCount;
        int childCount;
        decimal totalAmount;
        string date;

        public PrintService()
        {
            this.ticketType = "TicketType";
            this.ticketNumber = "TicketNumber";
            this.adultCount = 0;
            this.childCount = 0;
            this.totalAmount = 0;
        }

        public PrintService(int ticketNumber, string ticketType, int adultCount, int childCount, int totalAmount)
        {
            this.ticketType = ticketType;
            this.ticketNumber = ticketNumber.ToString() + " " + ticketType.Substring(0, 1);
            this.adultCount = adultCount;
            this.childCount = childCount;
            this.totalAmount = totalAmount;
            this.date = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
        }

        private string FormatAdultCount()
        {
            return this.adultCount == 0 ? "---" : adultCount.ToString();
        }

        private string FormatChildCount()
        {
            return this.childCount == 0 ? "---" : childCount.ToString();
        }

        public void Print()
        {
            LPrinter printer = new LPrinter();

            if (printer.ChoosePrinter())
            {
                printer.Open("Ticket");

                ////printer.Print("\r\n");
                ////printer.Print("\r\n");
                ////printer.Print("\r\n");
                ////printer.Print("\x0e");
                ////printer.Print("        " + ticketType);
                ////printer.Print("\x14");
                ////printer.Print("\r\n");
                ////printer.Print("                " + ticketNumber + "                " + DateTime.Now.ToShortDateString() + "\r\n");
                ////printer.Print("                " + adultCount + "                   " + childCount + "\r\n");
                ////printer.Print("\r\n");
                ////printer.Print("             " + totalAmount  + "\r\n");

                ////printer.Print("\x0C");  // Print FormFeed


                printer.Print("\r\n");
                printer.Print("\r\n");
                printer.Print("\r\n");
                printer.Print("    " + ticketType);
                printer.Print("\r\n");
                printer.Print("\r\n");
                printer.Print("                " + ticketNumber + "          " + this.date + "\r\n");
                printer.Print("\r\n");
                printer.Print("               " + FormatAdultCount() + "                        " + FormatChildCount() + "\r\n");
                printer.Print("\r\n");
                printer.Print("\r\n");
                printer.Print("       " + String.Format("{0,1:F2}", totalAmount) + "\r\n");

                printer.Print("\x0C");  // Print FormFeed

                printer.Close();

            }
        }
    }
}
