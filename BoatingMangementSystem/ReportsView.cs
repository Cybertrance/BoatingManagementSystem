using System;
using System.Data.SQLite;
using System.Linq;
using Terminal.Gui;

namespace BoatingMangementSystem
{
    public class ReportsView : Window
    {
        #region Controls
        Window window;
        Label lblReportRange;
        TextField txtReportRange;
        Button btnGetReport;
        Button btnBack;

        FrameView vwReportResults;
        FrameView vwReportHeader;

        Label lblTypeTitle;
        Label lblAdultCountTitle;
        Label lblChildCountTitle;
        Label lblTotalTitle;

        Label lblboatH;
        Label lblboatA;
        Label lblboatC;
        Label lblboatT;
        Label lbltrainH;
        Label lbltrainA;
        Label lbltrainC;
        Label lbltrainT;
        Label lblmgrH;
        Label lblmgrA;
        Label lblmgrC;
        Label lblmgrT;
        Label lblmwalkH;
        Label lblmwalkA;
        Label lblmwalkC;
        Label lblmwalkT;
        Label lblkangarooH;
        Label lblkangarooA;
        Label lblkangarooC;
        Label lblkangarooT;
        Label lblcaterpH;
        Label lblcaterpA;
        Label lblcaterpC;
        Label lblcaterpT;
        Label lblcarH;
        Label lblcarA;
        Label lblcarC;
        Label lblcarT;
        #endregion

        public ReportsView() : base("Chhota Kashmir Boating Club")
        {
            window = new Window(" -- Reports -- ");
            this.Add(window);

            btnBack = new Button("_Back")
            {
                X = 1,
                Y = 26
            };

            //Init Controls
            lblReportRange = new Label("Fetch Reports for Date : ")
            {
                X = 1,
                Y = 1
            };
            txtReportRange = new TextField("")
            {
                X = 26,
                Y = 1,
                Width = 11
            };
            btnGetReport = new Button("Get Reports")
            {
                X = 1,
                Y = 4,
            };

            // Report Results View
            vwReportHeader = new FrameView("[Please enter a date]")
            {
                X = 1,
                Y = 6,
                Height = 20,
                Width = Dim.Width(this) - 5
            };
            lblTypeTitle = new Label("Type")
            {
                Y = 1,
                X = 2,

            };
            lblAdultCountTitle = new Label("|Adl")
            {
                Y = 1,
                X = 20
            };
            lblChildCountTitle = new Label("|Chi")
            {
                Y = 1,
                X = 24
            };
            lblTotalTitle = new Label("|Tot |")
            {
                Y = 1,
                X = 28
            };
            


            vwReportResults = new FrameView("")
            {
                Y = 2,
                Height = 16,
                Width = Dim.Width(this) - 7
            };

            lblboatH = new Label("Boat")
            {
                X = 1,
                Y = 1
            };
            lblboatA = new Label("---")
            {
                X = 20,
                Y = 1,
                Width = 3
            };
            lblboatC = new Label("---")
            {
                X = 24,
                Y = 1,
                Width = 3
            };
            lblboatT = new Label("---")
            {
                X = 28,
                Y = 1,
                Width = 3
            };

            lbltrainH = new Label("Train")
            {
                X = 1,
                Y = 3
            };
            lbltrainA = new Label("---")
            {
                X = 20,
                Y = 3,
                Width = 3
            };
            lbltrainC = new Label("---")
            {
                X = 24,
                Y = 3,
                Width = 3
            };
            lbltrainT = new Label("---")
            {
                X = 28,
                Y = 3,
                Width = 3
            };

            lblmgrH = new Label("Merry-Go-Round")
            {
                X = 1,
                Y = 5
            };
            lblmgrA = new Label("---")
            {
                X = 20,
                Y = 5,
                Width = 3
            };
            lblmgrC = new Label("---")
            {
                X = 24,
                Y = 5,
                Width = 3
            };
            lblmgrT = new Label("---")
            {
                X = 28,
                Y = 5,
                Width = 3
            };

            lblmwalkH = new Label("Moon Walker")
            {
                X = 1,
                Y = 7
            };
            lblmwalkA = new Label("---")
            {
                X = 20,
                Y = 7,
                Width = 3
            };
            lblmwalkC = new Label("---")
            {
                X = 24,
                Y = 7,
                Width = 3
            };
            lblmwalkT = new Label("---")
            {
                X = 28,
                Y = 7,
                Width = 3
            };

            lblkangarooH = new Label("Kangaroo")
            {
                X = 1,
                Y = 9
            };
            lblkangarooA = new Label("---")
            {
                X = 20,
                Y = 9,
                Width = 3
            };
            lblkangarooC = new Label("---")
            {
                X = 24,
                Y = 9,
                Width = 3
            };
            lblkangarooT = new Label("---")
            {
                X = 28,
                Y = 9,
                Width = 3
            };

            lblcaterpH = new Label("Caterpillar")
            {
                X = 1,
                Y = 11
            };
            lblcaterpA = new Label("---")
            {
                X = 20,
                Y = 11,
                Width = 3
            };
            lblcaterpC = new Label("---")
            {
                X = 24,
                Y = 11,
                Width = 3
            };
            lblcaterpT = new Label("---")
            {
                X = 28,
                Y = 11,
                Width = 3
            };

            lblcarH = new Label("Car")
            {
                X = 1,
                Y = 13
            };
            lblcarA = new Label("---")
            {
                X = 20,
                Y = 13,
                Width = 3
            };
            lblcarC = new Label("---")
            {
                X = 24,
                Y = 13,
                Width = 3
            };
            lblcarT = new Label("---")
            {
                X = 28,
                Y = 13,
                Width = 3
            };

            vwReportResults.Add(lblboatH, lblboatA, lblboatC, lblboatT,
                lbltrainH,lbltrainA, lbltrainC, lbltrainT,
                lblmgrH, lblmgrA, lblmgrC, lblmgrT,
                lblmwalkH, lblmwalkA, lblmwalkC, lblmwalkT,
                lblkangarooH, lblkangarooA, lblkangarooC, lblkangarooT,
                lblcaterpH, lblcaterpA, lblcaterpC, lblcaterpT,
                lblcarH, lblcarA, lblcarC, lblcarT);

            vwReportHeader.Add(lblTypeTitle, lblAdultCountTitle, lblChildCountTitle, lblTotalTitle, vwReportResults);

            window.Add(lblReportRange, txtReportRange, btnGetReport, btnBack, vwReportHeader);

            //Link Event Handlers
            txtReportRange.Changed += TxtReportRange_Changed;
            btnBack.Clicked = () =>
            {
                Toplevel mainmenuLevel = new Toplevel()
                {
                    X = Application.Top.X,
                    Y = Application.Top.Y,
                    Height = Dim.Fill(),
                    Width = Dim.Fill()
                };

                mainmenuLevel.Add(new MainMenuView());

                Application.RequestStop();

                Application.Top.RemoveAll();
                Application.Top.Add(mainmenuLevel);
                Application.Top.FocusFirst();
                Application.Top.LayoutSubviews();

                Application.Run(mainmenuLevel);
            };
            btnGetReport.Clicked = () =>
            {
                if(ValidateDate(txtReportRange.Text.ToString()))
                {
                    int adultCount = 0;
                    int childCount = 0;
                    int totalAmount = 0;

                    string formattedDate = FormatDateString(txtReportRange.Text.ToString());

                    
                    vwReportHeader.Title = "Reports for " + txtReportRange.Text.ToString();
                    
                    // Update Boat
                    GetReportData(formattedDate, "Boat", out adultCount, out childCount, out totalAmount);
                    lblboatA.Text = adultCount.ToString();
                    lblboatC.Text = childCount.ToString();
                    lblboatT.Text = totalAmount.ToString();

                    // Update Train
                    GetReportData(formattedDate, "Train", out adultCount, out childCount, out totalAmount);
                    lbltrainA.Text = adultCount.ToString();
                    lbltrainC.Text = childCount.ToString();
                    lbltrainT.Text = totalAmount.ToString();

                    // Update Merry-Go-Round
                    GetReportData(formattedDate, "Merry-Go-Round", out adultCount, out childCount, out totalAmount);
                    lblmgrA.Text = adultCount.ToString();
                    lblmgrC.Text = childCount.ToString();
                    lblmgrT.Text = totalAmount.ToString();

                    // Update Moon Walker
                    GetReportData(formattedDate, "Moon Walker", out adultCount, out childCount, out totalAmount);
                    lblmwalkA.Text = adultCount.ToString();
                    lblmwalkC.Text = childCount.ToString();
                    lblmwalkT.Text = totalAmount.ToString();

                    // Update Kangaroo
                    GetReportData(formattedDate, "Kangaroo", out adultCount, out childCount, out totalAmount);
                    lblkangarooA.Text = adultCount.ToString();
                    lblkangarooC.Text = childCount.ToString();
                    lblkangarooT.Text = totalAmount.ToString();

                    // Update Caterpillar
                    GetReportData(formattedDate, "Caterpillar", out adultCount, out childCount, out totalAmount);
                    lblcaterpA.Text = adultCount.ToString();
                    lblcaterpC.Text = childCount.ToString();
                    lblcaterpT.Text = totalAmount.ToString();

                    // Update Car
                    GetReportData(formattedDate, "Car", out adultCount, out childCount, out totalAmount);
                    lblcarA.Text = adultCount.ToString();
                    lblcarC.Text = childCount.ToString();
                    lblcarT.Text = totalAmount.ToString();
                }
                else
                {
                    MessageBox.ErrorQuery(44, 10, "Error!", "Invalid Date", "Ok");
                    FocusPrev();
                }
            };
        }

        private void TxtReportRange_Changed(object sender, EventArgs e)
        {
            if(txtReportRange.Text.Count() == 10)
            {
                FocusNext();
            }
        }
        
        private bool ValidateDate(String date)
        {
            bool isValid = true;

            if (date.Length == 10)
            {
                string day = date.Substring(0, 2);
                string month = date.Substring(3, 2);
                string year = date.Substring(6, 4);

                foreach (var ch in date)
                {
                    if (!(char.IsDigit(ch) || char.Equals(Convert.ToChar("/"), ch)))
                    {
                        isValid = false;
                    }
                    
                }

                if (isValid)
                {
                    int dayVal = 0; 
                    int monthVal = 0;
                    int yearVal = 0;

                    isValid = isValid && int.TryParse(day, out dayVal);
                    isValid = isValid && int.TryParse(month, out monthVal);
                    isValid = isValid && int.TryParse(year, out yearVal);

                    isValid = isValid && (dayVal >= 1 && dayVal <= 31) ? true : false;
                    isValid = isValid && (monthVal >= 01 && monthVal <= 12) ? true : false;
                    isValid = isValid && (yearVal >= 2017 && yearVal <= 2999) ? true : false;
                }
            }
            else
            {
                isValid = false;
            }

            return isValid;
        }

        private void GetReportData(string date, string type, out int adultCount, out int childCount, out int totalAmount)
        {
            adultCount = 0;
            childCount = 0;
            totalAmount = 0;

            SQLiteConnection dbConnection = new SQLiteConnection("Data Source=TicketData.sqlite;Version=3;");

            try
            {
                dbConnection.Open();

                string sql = "select SUM(AdultCount) as AdultCount, SUM(ChildCount) as ChildCount, SUM(TotalAmount) as TotalAmount from tickets where Date = '" + date + "' and TicketType = '" + type + "'";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int.TryParse(reader["AdultCount"].ToString(), out adultCount);
                    int.TryParse(reader["ChildCount"].ToString(), out childCount);
                    int.TryParse(reader["TotalAmount"].ToString(), out totalAmount);
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

        private string FormatDateString(string date)
        {
            string formattedString;

            string day = date.Substring(0, 2);
            string month = date.Substring(3, 2);
            string year = date.Substring(6, 4);

            return formattedString = year + "-" + month + "-" + day;
        }
    }
}
