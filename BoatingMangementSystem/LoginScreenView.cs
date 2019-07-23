using System;
using Terminal.Gui;

namespace BoatingMangementSystem
{
    public class LoginScreenView : Window
    {
        #region Controls
        Window window;
        //Label lblLoginTitle;
        Label lblPasswordLabel;
        TextField txtPasswordField;
        Button btnLogin;
        #endregion
        public LoginScreenView() : base ("Chhota Kashmir Boat Club")
        {
            window = new Window(" -- Login -- ");
            this.Add(window);

            
            
            // Init controls
            //lblLoginTitle = new Label("Login") { X = Pos.Center() - 2, Y = Pos.Top(this) + 2, Width = 10, Height = 1 };
            lblPasswordLabel = new Label("Enter Password : ") { X = Pos.Center() - 17, Y = Pos.Center(), Width = 10, Height = 1 };
            txtPasswordField = new TextField("") { X = Pos.Center() + 1 , Y = Pos.Center(), Secret = true, Width = 15, Height = 1};
            btnLogin = new Button("Login", true) { X = Pos.Center(), Y = Pos.Center() + 3, Width = 10, Height = 2};

            window.Add(lblPasswordLabel, txtPasswordField, btnLogin);
            SetFocus(txtPasswordField);

            //Link Event Handlers
            btnLogin.Clicked = () =>
            {
                bool isValid = false;
                String fixedPassword = "admin123";

                string passwordBoxText = txtPasswordField.Text != null ? txtPasswordField.Text.ToString() : null;

                if (!string.IsNullOrEmpty(passwordBoxText))
                {
                    isValid = String.Equals(passwordBoxText, fixedPassword);
                }

                if (isValid)
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
                else
                {
                    MessageBox.ErrorQuery(44, 10, "Error!", "Password incorrect! Please try again.", "Ok");
                }
            };
        }
       
    }
}
