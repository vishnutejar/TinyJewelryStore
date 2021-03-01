using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using TinyJewelryStore.models;
using TinyJewelryStore.utils;
using TinyJewelryStore.ViewModels.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TinyJewelryStore.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private IMessageService _messageService;
        private INavigationService _navigationService;

        public List<UserInformation> userInformation { get; set; }
        public LoginViewModel()
        {
            _messageService = DependencyService.Get<IMessageService>();
            _navigationService = DependencyService.Get<INavigationService>();
        }

        private string _email { get; set; }
        private string _password { get; set; }


        public string Email
        {
            set
            {
                _email = value;
                this.Notify("Email");
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
                this.Notify("Password");
            }
            get
            {

                return _password;
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
            get
            {

                return new Command(CancelPage);
            }
        }

        private void CancelPage()
        {
            Process.GetCurrentProcess().CloseMainWindow();
            Process.GetCurrentProcess().Close();
        }

        private void Login()
        {


            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Password))
            {
                _messageService.ShowAsync("Please enter Email and Password");
            }
            else
            {

                List<UserInformation> userdata = LoadXMLData().Result;
                UserInformation loginUserInf = userdata?.FirstOrDefault(u => u.Email.Equals(Email) && u.Password.Equals(Password));
                if (loginUserInf != null)
                {
                    Preferences.Set(Constances.UserType, loginUserInf?.UserType);
                    _navigationService.NavigateToEstimateGoldPage();
                }
                else {
                    _messageService.ShowAsync("Please enter Email and Password");
                }
            }
        }

        private async Task<List<UserInformation>> LoadXMLData()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("TinyJewelryStore" + "." + "userinformation.xml");

            XDocument doc = XDocument.Load(stream);
            IEnumerable<UserInformation> users = from s in doc.Descendants("user")
                                                 select new UserInformation
                                                 {
                                                     Email = s.Attribute("Email").Value,
                                                     UserType = s.Attribute("UserType").Value,
                                                     Password = s.Attribute("Password").Value,
                                                     UserId = s.Attribute("UserId").Value
                                                 };
            return users.ToList();
        }

    }
}
