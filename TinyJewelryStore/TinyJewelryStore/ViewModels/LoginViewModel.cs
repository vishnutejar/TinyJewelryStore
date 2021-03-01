using System;
using System.ComponentModel;
using TinyJewelryStore.Views;
using Xamarin.Forms;

namespace TinyJewelryStore.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public LoginViewModel() { }

        private string _email { get; set; }
        private string _password { get; set; }

        public string Email
        {
            set
            {
                _email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
            get
            {

                return _email;
            }
        }
        public string Password
        {
            set
            {
                _password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
            get
            {

                return _email;
            }
        }

        public Command LoginCommand
        {

            get
            {

                return new Command(Login);
            }
        }
        public Command CancelCommand
        {
            get {

                return new Command(CancelPage);
            }
        }

        private void CancelPage()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void Login()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                Application.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            }
            else
            {
                Application.Current.MainPage.Navigation.PushAsync(new EstimationGoldPage());
            }
        }
    }
}
