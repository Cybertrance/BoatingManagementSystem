using Terminal.Gui;

namespace BoatingMangementSystem
{
    public class MainMenuView : Window
    {
        #region Controls
        Window window;
        Button btnTicket;
        Button btnReports;
        Button btnTicketSettings;
        CheckBox chkSpecialRates;
        Button btnExitApplication;
        #endregion

        public MainMenuView() : base ("Chhota Kashmir Boat Club")
        {
            window = new Window(" -- Main Menu -- ");
            this.Add(window);

            // Init Controls
            btnTicket = new Button("[1] _Ticket Printing")
            {
                X = 1,
                Y = 1,
                Width = 22
            };
            btnReports = new Button("[2] _View Reports")
            {
                X = 1,
                Y = 2,
                Width = 22
            };
            btnTicketSettings = new Button("[3] Ticket _Settings")
            {
                X = 1,
                Y = 3,
                Width = 22
            };
            chkSpecialRates = new CheckBox("Apply _special Rates for all tickets (holiday/spcecial day)?")
            {
                X = 1,
                Y = 5
            };
            btnExitApplication = new Button("_Exit Application")
            {
                X = 1,
                Y = 8
            };

            window.Add(btnTicket, btnReports, btnTicketSettings, chkSpecialRates, btnExitApplication);

            // Link Event Handlers
            btnTicket.Clicked = () => {

                Toplevel ticketLevel = new Toplevel()
                {
                    X = Application.Top.X,
                    Y = Application.Top.Y,
                    Height = Dim.Fill(),
                    Width = Dim.Fill()
                };

                ticketLevel.Add(new TicketPrintScreenView());

                Application.RequestStop();

                Application.Top.RemoveAll();
                Application.Top.Add(ticketLevel);
                Application.Top.FocusFirst();
                Application.Top.LayoutSubviews();

                Application.Run(ticketLevel);
            };
            btnReports.Clicked = () =>
            {
                Toplevel reportsLevel = new Toplevel()
                {
                    X = Application.Top.X,
                    Y = Application.Top.Y,
                    Height = Dim.Fill(),
                    Width = Dim.Fill()
                };

                reportsLevel.Add(new ReportsView());

                Application.RequestStop();

                Application.Top.RemoveAll();
                Application.Top.Add(reportsLevel);
                Application.Top.FocusFirst();
                Application.Top.LayoutSubviews();

                Application.Run(reportsLevel);
            };
            btnTicketSettings.Clicked = () =>
            {
                Toplevel ticketSettingsLevel = new Toplevel()
                {
                    X = Application.Top.X,
                    Y = Application.Top.Y,
                    Height = Dim.Fill(),
                    Width = Dim.Fill()
                };

                ticketSettingsLevel.Add(new TicketSettingsView());

                Application.RequestStop();

                Application.Top.RemoveAll();
                Application.Top.Add(ticketSettingsLevel);
                Application.Top.FocusFirst();
                Application.Top.LayoutSubviews();

                Application.Run(ticketSettingsLevel);
            };
            btnExitApplication.Clicked = () =>
            {
                if (MessageBox.Query(44, 10, "Exit Application", "Are you sure?", "Yes", "No") == 0)
                {
                    Application.RequestStop();
                    System.Environment.Exit(-1);
                }
            };
        }
    }
}
