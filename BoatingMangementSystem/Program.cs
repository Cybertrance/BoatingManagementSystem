using System.Data.SQLite;
using System.IO;
using Terminal.Gui;

namespace BoatingMangementSystem
{
    class BoatingSystemMain
    {
        static void Main(string[] args)
        {
            
            System.Console.SetWindowSize(120, 36);
            System.Console.Title = "Chhota Kashmir Boating Club";
            
            Application.Init();
            InitializeDatabase();
            Application.Top.Add(new LoginScreenView());
            
            Application.Run();

        }

       static void InitializeDatabase()
        {
            if (!File.Exists(@"TicketData.sqlite"))
            {
                SQLiteConnection.CreateFile(@"TicketData.sqlite");
                SQLiteConnection dbConnection = new SQLiteConnection("Data Source=TicketData.sqlite;Version=3;");

                try
                {
                    dbConnection.Open();

                    string sql = "CREATE TABLE 'tickets' ('ticketnumber' INTEGER, 'tickettype' TEXT, 'adultcount' INTEGER, 'childcount' INTEGER, 'date' TEXT,'totalamount' INTEGER)";
                    SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                    command.ExecuteNonQuery();


                        sql = "insert into tickets (ticketnumber, tickettype, adultcount, childcount, date, totalamount) values " +
                        "('" + "0" + "', '" + "--" + "', " + "0" + ", " + "0" + ", '" + "--" + "', " + "0" + ")";
                        command = new SQLiteCommand(sql, dbConnection);

                        if (command.ExecuteNonQuery() < 1)
                        {
                            MessageBox.ErrorQuery(44, 10, "Error!", "Database Error! Please try again.", "Ok");
                        }
                    
                }
                catch(System.Exception e)
                {
                    MessageBox.ErrorQuery(44, 10, "Error!", "Database Error! Please contact developer", "Ok");
                }
                finally
                {
                    dbConnection.Close();
                }
            }
        }
    }
}
