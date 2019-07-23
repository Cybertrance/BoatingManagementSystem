using System;
using System.Data.SQLite;
using System.Linq;
using Terminal.Gui;

namespace BoatingMangementSystem
{
    public class TicketPrintScreenView : Window
    {
        #region Controls

        Window window;
        Label lblTicketTypesHeader;
        Label lblTicketTypes;
        Label lblTicketSelection;
        TextField txtTicketSelection;
        Label lblTicketSelectionDisplay;
        FrameView ticketTypeView;
        Label lblAdultCount;
        TextField txtAdultCount;
        Label lblChildCount;
        TextField txtChildCount;
        Label lblTotal;
        Label txtTotal;
        FrameView ticketDetailsView;
        Button btnPrint;
        Button btnCancel;
        Button btnBack;

        #endregion

        #region Variables
        public String SelectedTicket { get; set; }
        public int AdultCount { get; set; }
        public int ChildrenCount { get; set; }
        public int TotalAmount { get; set; }

        const int BaseBoatAmountAdult = 30;
        const int BaseBoatAmountChild = 30;
        #endregion

        public TicketPrintScreenView() : base ("Chhota Kashmir Boat Club")
        {
            window = new Window(" -- Print Ticket -- ");
            this.Add(window);

            // Ticket Type Controls
            lblTicketTypesHeader = new Label("Ticket Types:") { X = 1, Y = 1 };
            lblTicketTypes = new Label("[B]oat / [T]rain / [M]erry-Go-Round / Moon [W]alker / [K]angaroo / C[a]terpillar / [C]ar") { X = 1, Y = 2};

            lblTicketSelection = new Label("Ticket Selection : ") { X = 1 , Y = 4 };
            txtTicketSelection = new TextField("") { X = 20, Y = 4 , Width = 2};
            lblTicketSelectionDisplay = new Label("") { X = 23, Y = 4};


            ticketTypeView = new FrameView("Ticket Type") { X = 1, Y = 1, Height = 8, Width = Dim.Width(this) - 5};
            ticketTypeView.Add(lblTicketTypesHeader, lblTicketTypes, lblTicketSelection, txtTicketSelection, lblTicketSelectionDisplay);

            // Ticket Details Controls
            lblAdultCount = new Label("No. of Adults : ") {Y = 1};
            txtAdultCount = new TextField("") { X = 18, Y = 1, Width = 3 };
            lblChildCount = new Label("No. of Children : ") { Y = 3 };
            txtChildCount = new TextField("") { X = 18, Y = 3, Width = 3 };
            lblTotal = new Label("Total Bill Amount : ") { Y = 6 };
            txtTotal = new Label("") { X = 20, Y = 6, Width = 5};
            ticketDetailsView = new FrameView("Ticket Details") { X = 1, Y = 11 , Width = Dim.Width(this) - 5 , Height = 10};
            ticketDetailsView.Add(lblAdultCount, txtAdultCount, lblChildCount, txtChildCount, lblTotal, txtTotal);


            btnPrint = new Button("_Print Ticket") { X = 1, Y = 26};
            btnCancel = new Button("_Cancel") { X = 27, Y = 26 };
            btnBack = new Button("_Back") { X = 18, Y = 26 };
            window.Add(ticketTypeView, ticketDetailsView, btnPrint, btnCancel, btnBack);

            // Link event handlers.
            txtTicketSelection.Changed += TxtTicketSelection_Changed;
            txtAdultCount.Changed += TxtAdultCount_Changed;
            txtChildCount.Changed += TxtChildCount_Changed;
            
            btnCancel.Clicked = BtnCancel_Clicked;
            btnBack.Clicked = BtnBack_Clicked;
            btnPrint.Clicked = BtnPrint_Clicked;
        }

        private void BtnPrint_Clicked()
        {
            if (txtTicketSelection.Text.IsEmpty || txtAdultCount.Text.IsEmpty || txtChildCount.Text.IsEmpty || txtTotal.Text.IsEmpty)
            {
                MessageBox.ErrorQuery(44, 10, "Error!", "Please fill all required fields.", "Ok");
            }
            else
            {
                String type = lblTicketSelectionDisplay.Text.ToString();
                int ticketId = -1;
                if ((ticketId = UpdateTable(type,AdultCount,ChildrenCount,TotalAmount)) > 0)
                {
                    PrintTicket(ticketId);
                    MessageBox.Query(44, 10, "Success!", "Ticket Printing...", "Ok");
                    ClearFeilds();
                }
            }
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

        private void BtnCancel_Clicked()
        {
            if (MessageBox.Query(40, 15, "Confirm", "Are you sure you want to cancel?", "Yes", "No") == 0)
            {
                ClearFeilds();

            }
        }

        public void PrintTicket(int ticketId)
        {
            PrintService ps = new PrintService(ticketId, FormatTicketType(lblTicketSelectionDisplay.Text.ToString()), AdultCount, ChildrenCount, TotalAmount);

            ps.Print();
        }

        private string FormatTicketType(string ticketType)
        {
            string formattedType;

            switch (ticketType)
            {
                case "Boat":
                    formattedType = "B O A T";
                    break;
                default:
                    formattedType = ticketType.ToUpper();
                    break;
            }

            return formattedType;
        }

        private void ClearFeilds()
        {
            AdultCount = 0;
            ChildrenCount = 0;
            TotalAmount = 0;

            txtAdultCount.Text = "";
            txtChildCount.Text = "";
            txtTicketSelection.Text = "";
            txtTotal.Text = "";
            lblTicketSelectionDisplay.Text = "";
        }

        private void TxtChildCount_Changed(object sender, EventArgs e)
        {
            if (!txtChildCount.Text.IsEmpty)
            {
                int childCount = 0;

                if (int.TryParse(txtChildCount.Text.ToString(), out childCount))
                {
                    if (childCount > 0)
                    {
                        ChildrenCount = childCount;
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
                ChildrenCount = 0;
            }

            txtTotal.Text = CalculateTotalBillAmount().ToString();
        }

        private void TxtAdultCount_Changed(object sender, EventArgs e)
        {
            if(!txtAdultCount.Text.IsEmpty)
            {
                int adultCount = 0;

                if (int.TryParse(txtAdultCount.Text.ToString(), out adultCount))
                {
                    if (adultCount > 0)
                    {
                        AdultCount = adultCount;
                    }
                }
                else
                {
                    MessageBox.ErrorQuery(44, 10, "Error!", "Please enter a number", "OK");

                    //TODO
                }

            }
            else
            {
                AdultCount = 0;
            }

            txtTotal.Text = CalculateTotalBillAmount().ToString();
        }


        #region Event Handlers

        private void TxtTicketSelection_Changed(object sender, EventArgs e)
        {
            if (!txtTicketSelection.Text.IsEmpty)
            {
                String[] allowedTicketSelections = new String[] { "B", "T", "M", "W", "K", "A", "C" };

                String ticketSelectionText = txtTicketSelection.Text.ToString();
                ticketSelectionText = ticketSelectionText.Substring(ticketSelectionText.Count() - 1);
                ticketSelectionText = ticketSelectionText.ToUpper();

                if (!string.IsNullOrEmpty(ticketSelectionText) && allowedTicketSelections.Contains(ticketSelectionText))
                {
                    SetTicketSelectionDisplay(ticketSelectionText);
                    SelectedTicket = ticketSelectionText;

                    FocusNext();
                }
                else
                {
                    MessageBox.ErrorQuery(44, 10, "Error!", "Invalid Selection", "OK");

                    this.SetFocus(txtTicketSelection);
                }
            }
        }

        private int CalculateTotalBillAmount()
        {
            TotalAmount = (AdultCount * BaseBoatAmountAdult) + (ChildrenCount * BaseBoatAmountChild);

            return TotalAmount;
        }
        #endregion

        #region Methods

        private void SetTicketSelectionDisplay(String option)
        {
            lblTicketSelectionDisplay.Width = Dim.Fill();
            switch (option)
            {
                case "B":
                    lblTicketSelectionDisplay.Text = "Boat";
                    break;
                case "T":
                    lblTicketSelectionDisplay.Text = "Train";
                    break;
                case "W":
                    lblTicketSelectionDisplay.Text = "Moon Walker";
                    break;
                case "M":
                    lblTicketSelectionDisplay.Text = "Merry-Go-Round";
                    break;
                case "K":
                    lblTicketSelectionDisplay.Text = "Kangaroo";
                    break;
                case "A":
                    lblTicketSelectionDisplay.Text = "Caterpillar";
                    break;
                case "C":
                    lblTicketSelectionDisplay.Text = "Car";
                break;

                default:
                    lblTicketSelectionDisplay.Text = "";
                    lblTicketSelectionDisplay.Width = 0;
                    break;
            }
        }

        private int UpdateTable(string type, int adultCount, int childCount, int totalAmount)
        {
            bool tableUpdated = false;
            int lastTicketId = GetLastId();
            int newTicketId = lastTicketId + 1;

            if(lastTicketId > 0)
            {
                SQLiteConnection dbConnection = new SQLiteConnection("Data Source=TicketData.sqlite;Version=3;");

                try
                {
                    dbConnection.Open();

                    string sql = "insert into tickets (ticketnumber, tickettype, adultcount, childcount, date, totalamount) values " +
                        "('" + newTicketId + "', '" + type + "', " + adultCount + ", " + childCount + ", '" + DateTime.Now.ToString("yyyy-MM-dd") + "', " + totalAmount + ")";
                    SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

                    if (command.ExecuteNonQuery() < 1)
                    {
                        MessageBox.ErrorQuery(44, 10, "Error!", "Database Error! Please try again.", "Ok");
                    }
                    else
                    {
                        tableUpdated = true;
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
            return tableUpdated ? newTicketId : -1;
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
