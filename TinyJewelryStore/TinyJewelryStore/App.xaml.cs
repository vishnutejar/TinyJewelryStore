using System;
using TinyJewelryStore.ViewModels;
using TinyJewelryStore.ViewModels.Services;
using TinyJewelryStore.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TinyJewelryStore
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<IMessageService, MessageService>();
            DependencyService.Register<INavigationService, NavigationService>();
            MainPage = new NavigationPage( new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
