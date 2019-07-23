using System;
using System.Data.SQLite;
using System.Linq;
using Terminal.Gui;

namespace BoatingMangementSystem
{
    class TicketSettingsView : Window
    {
        #region Controls

        Window window;        
        Label lblNewTicketNumber;
        TextField txtNewTicketNumber;        
        Button btnNewTicketNumberApply;        
        Button btnBack;

        #endregion

        #region Variables
        public int NewTicketNumber { get; set; }        
        #endregion

        public TicketSettingsView() : base("Chhota Kashmir Boat Club")
        {
            window = new Window(" -- Ticket Settings -- ");
            this.Add(window);

            // Ticket Type Controls           
            lblNewTicketNumber = new Label("New ticket number : ") { X = 1, Y = 1 };
            txtNewTicketNumber = new TextField("") { X = 21, Y = 1, Width = 10 };

            btnNewTicketNumberApply = new Button("Apply new ticket number") { X = 32, Y = 1 };
            btnBack = new Button("_Back") { X = 1, Y = 26 };
            window.Add(lblNewTicketNumber, txtNewTicketNumber, btnNewTicketNumberApply, btnBack);

            // Link event handlers.
            txtNewTicketNumber.Changed += TxtTicketSelection_Changed;            
            
            btnBack.Clicked = BtnBack_Clicked;
            btnNewTicketNumberApply.Clicked = BtnNewTicketNumberApply_Clicked;
        }
        
        private void BtnBack_Clicked()
        {
            Toplevel mainMenuLevel = new Toplevel()
            {
                X = Application.Top.X,
                Y = Application.Top.Y,
                Height = Dim.Fill(),
                Width = Dim.Fill()
            };

            mainMenuLevel.Add(new MainMenuView());

            Application.RequestStop();

            Application.Top.RemoveAll();
            Application.Top.Add(mainMenuLevel);
            Application.Top.FocusFirst();
            Application.Top.LayoutSubviews();

            Application.Run(mainMenuLevel);

        }

        private void BtnNewTicketNumberApply_Clicked()
        {
            int result;
            if (!txtNewTicketNumber.Text.IsEmpty && int.TryParse(NewTicketNumber.ToString(), out result))
            {
                int ticketNumberToUpdate = result - 1;

                if (ticketNumberToUpdate <= GetLastId())
                {
                    MessageBox.ErrorQuery(40, 10, "Error!", "Ticket number already used.", "OK");                    
                }
                else
                {
                    ChangeTicketNumber(ticketNumberToUpdate);
                }
            }
            else
            {
                MessageBox.ErrorQuery(40, 10, "Error!", "Please enter a valid ticket number", "OK");
            }
        }
       
        #region Event Handlers

        private void TxtTicketSelection_Changed(object sender, EventArgs e)
        {
            if (!txtNewTicketNumber.Text.IsEmpty)
            {
                int newTicketNumber = 0;

                if (int.TryParse(txtNewTicketNumber.Text.ToString(), out newTicketNumber))
                {
                    if (newTicketNumber > 0)
                    {
                        NewTicketNumber = newTicketNumber;
                    }
                }
                else
                {
                    MessageBox.ErrorQuery(40, 20, "Error!", "Please enter a number", "OK");

                    //TODO
                }
            }
            else
            {
                NewTicketNumber = 0;
            }
        }
        
        #endregion

        #region Methods
        
        private void ChangeTicketNumber(int newNumber)
        {
            SQLiteConnection dbConnection = new SQLiteConnection("Data Source=TicketData.sqlite;Version=3;");

            try
            {
                dbConnection.Open();

                string sql = "insert into tickets (ticketnumber, tickettype, adultcount, childcount, date, totalamount) values " +
                "('" + newNumber.ToString() + "', '" + "--" + "', " + "0" + ", " + "0" + ", '" + "--" + "', " + "0" + ")";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

                if (command.ExecuteNonQuery() < 1)
                {
                    MessageBox.ErrorQuery(44, 10, "Error!", "Database Error! Please try again.", "Ok");
                }
                else
                {
                    MessageBox.Query(44, 10, "Success!", "Ticket number updated.", "Ok");
                }

            }
            catch (System.Exception e)
            {
                MessageBox.ErrorQuery(44, 10, "Error!", "Database Error! Please contact developer", "Ok");
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private int GetLastId()
        {
            int id = -1;

            SQLiteConnection dbConnection = new SQLiteConnection("Data Source=TicketData.sqlite;Version=3;");

            try
            {
                dbConnection.Open();

                string sql = "SELECT TicketNumber FROM Tickets ORDER BY TicketNumber DESC LIMIT 1";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int.TryParse(reader["TicketNumber"].ToString(), out id);
                }
            }
            catch (System.Exception e)
            {
                MessageBox.ErrorQuery(44, 10, "Error!", "Database Error! Please contact developer", "Ok");
            }
            finally
            {
                dbConnection.Close();
            }

            return id;
        }

        #endregion
    }
}
