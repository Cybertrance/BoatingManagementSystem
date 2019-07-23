using System;
using System.Windows.Forms;

namespace LPrinterTest
{
   public partial class Form1 : Form
   {
      LPrinter MyPrinter;      
      
      public Form1()
      {
         InitializeComponent();
         MyPrinter = new LPrinter();
      }

      private void button1_Click(object sender, EventArgs e)
      {
         MyPrinter.ChoosePrinter();
      }

      private void button2_Click(object sender, EventArgs e)
      {
         if(!MyPrinter.Open("Test Page")) return;
         MyPrinter.Print("This text is sent to a line printer\r\n");
         MyPrinter.Print("===================================\r\n");
         MyPrinter.Close();
      }
   }
}
