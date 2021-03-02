using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using TinyJewelryStore.utils;
using TinyJewelryStore.ViewModels.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TinyJewelryStore.ViewModels
{
    public class EstimationGoldViewModel : ViewModelBase
    {
        private IBlueToothService _blueToothService;
        private IMessageService _messageService;
        private INavigationService _navigationService;
        public string _GoldPrice;
        public string _Weight;
        public string _TotalPrice;
        public string _Discount;

        public string _DisplayUserType;
        public bool _IsPrivilegedUser;
        float totalprice;

        public EstimationGoldViewModel()
        {
            _messageService = DependencyService.Get<IMessageService>();
            _navigationService = DependencyService.Get<INavigationService>();
            _blueToothService = DependencyService.Get<IBlueToothService>();
            BindDeviceList();

            InitEstimateUI();
        }
        private IList<string> _deviceList;
        public IList<string> DeviceList
        {
            get
            {
                if (_deviceList == null)
                    _deviceList = new ObservableCollection<string>();
                return _deviceList;
            }
            set
            {
                _deviceList = value;
            }
        }
        private string _printMessage;
        public string PrintMessage
        {
            get
            {
                return _printMessage;
            }
            set
            {
                _printMessage = value;
            }
        }
        private string _selectedDevice;
        public string SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }
            set
            {
                _selectedDevice = value;
            }
        }

        void BindDeviceList()
        {
            var list = _blueToothService.GetDeviceList();
            DeviceList.Clear();
            foreach (var item in list)
                DeviceList.Add(item);
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

                return new Command(async () =>
                {

                    if (string.IsNullOrEmpty(GoldPrice))
                    {
                        await _messageService.ShowAsync("Enter Gold Prices");
                    }
                    else if (string.IsNullOrEmpty(Weight))
                    {
                        await _messageService.ShowAsync("Enter Gold Weight");
                    }
                    else if (_IsPrivilegedUser)
                    {
                        if (string.IsNullOrEmpty(Discount))
                        {

                            await _messageService.ShowAsync("Enter Gold Discount");
                        }
                        else
                        {
                            totalprice = (float.Parse(GoldPrice) * float.Parse(Weight)) - float.Parse(Discount);
                        }
                    }
                    else
                    {
                        totalprice = float.Parse(GoldPrice) * float.Parse(Weight);

                    }

                    TotalPrice = totalprice.ToString();
                });
            }
        }

        public Command PrintToScreen
        {
            get
            {

                return new Command(async () =>
                {
                    await _messageService.ShowAsync("GoldPrice :" + GoldPrice + "Weight" + Weight + "Discount" + Discount);
                });
            }
        }

        public Command PrintToFile
        {

            get
            {

                return new Command(
                    async () =>
                    {
                        if (File.Exists(FileUtils.fileName))
                        {
                            try
                            {
                                File.WriteAllText(FileUtils.fileName, "GoldPrice :" + GoldPrice + "Weight" + Weight + "Discount" + Discount);
                                await _messageService.ShowAsync("File saved to: " + FileUtils.fileName);
                            }
                            catch (Exception ex)
                            {
                                await _messageService.ShowAsync(ex.Message);
                            }

                        }

                    });
            }
        }


        public ICommand PrintToPaper
        {

            get
            {
                return new Command(async () =>
                {
                    PrintMessage += "GoldPrice :" + GoldPrice + "Weight" + Weight + "Discount" + Discount;
                    await _blueToothService.Print(SelectedDevice, PrintMessage);
                });
            }
        }

        public Command Close
        {

            get
            {

                return new Command(async () =>
                {
                    await _navigationService.ClosePage();
                });
            }
        }

    }
}
