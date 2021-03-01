using System;
using System.IO;
using TinyJewelryStore.utils;
using TinyJewelryStore.ViewModels.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TinyJewelryStore.ViewModels
{
    public class EstimationGoldViewModel : ViewModelBase
    {
        private IMessageService _messageService;
        private INavigationService _navigationService;
        public string _GoldPrice;
        public string _Weight;
        public string _TotalPrice;
        public string _Discount;

        public string _DisplayUserType;
        public bool _IsPrivilegedUser;

        public EstimationGoldViewModel()
        {
            _messageService = DependencyService.Get<IMessageService>();
            _navigationService = DependencyService.Get<INavigationService>();
            InitEstimateUI();
        }

        private void InitEstimateUI()
        {
            this.DisplayUserType = Path.Combine("Welcome:", Preferences.Get(Constances.UserType, ""));
            if (Preferences.Get(Constances.UserType, "").Equals("Privileged"))
            {
                IsPrivilegedUser = true;
            }
            else
            {
                IsPrivilegedUser = false;
            }
        }

        public bool IsPrivilegedUser
        {
            get
            {

                return _IsPrivilegedUser;


            }
            set
            {
                _IsPrivilegedUser = value;
                this.Notify("IsPrivilegedUser");
            }
        }
        public string DisplayUserType
        {
            get
            {

                return _DisplayUserType;


            }
            set
            {
                _DisplayUserType = value;
                this.Notify("DisplayUserType");
            }
        }

        public string GoldPrice
        {
            get
            {

                return _GoldPrice;
            }
            set
            {
                _GoldPrice = value;
                this.Notify("GoldPrice");
            }
        }

        public string Weight
        {
            get
            {

                return _Weight;
            }
            set
            {

                _Weight = value;
                this.Notify("Weight");
            }
        }

        public string TotalPrice
        {
            get
            {

                return _TotalPrice;
            }
            set
            {

                _TotalPrice = value;
                this.Notify("TotalPrice");
            }
        }

        public string Discount
        {
            get
            {
                return _Discount;
            }
            set
            {

                _Discount = value;

                this.Notify("Discount");

            }
        }



        public Command Calculate
        {

            get
            {

                return new Command(CalcuatePrice);
            }
        }

        private void CalcuatePrice()
        {
            if (string.IsNullOrEmpty(GoldPrice))
            {
                _messageService.ShowAsync("Enter Gold Prices");
            }
            else if (string.IsNullOrEmpty(Weight))
            {
                _messageService.ShowAsync("Enter Gold Weight");
            }
            else if (_IsPrivilegedUser)
            {
                if (string.IsNullOrEmpty(Discount))
                {

                    _messageService.ShowAsync("Enter Gold Discount");
                }
            }
            else
            {
                try
                {
                    float totalprice = (float.Parse(GoldPrice) * float.Parse(Weight)) - float.Parse(Discount);
                    TotalPrice = totalprice.ToString();
                }
                catch (Exception e)
                {
                }

            }
        }

        public Command PrintToScreen
        {
            get
            {

                return new Command(PrintToScreeValue);
            }
        }

        private void PrintToScreeValue()
        {
            _messageService.ShowAsync("GoldPrice :" + GoldPrice + "Weight" + Weight + "Discount" + Discount);
        }

        public Command PrintToFile
        {

            get
            {

                return new Command(PrintToFileValue);
            }
        }

        private void PrintToFileValue(object obj)
        {
            if (File.Exists(FileUtils.fileName))
            {
                File.WriteAllText(FileUtils.fileName, "GoldPrice :" + GoldPrice + "Weight" + Weight + "Discount" + Discount);
            }
        }

        public Command PrintToPaper
        {

            get
            {
                return new Command(PrintToPaperValue);
            }
        }

        private void PrintToPaperValue()
        {

        }

        public Command Close
        {

            get
            {

                return new Command(CloseValue);
            }
        }

        private void CloseValue()
        {
            _navigationService.ClosePage();
        }

    }
}
