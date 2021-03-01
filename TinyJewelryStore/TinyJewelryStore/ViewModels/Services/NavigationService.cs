using System.Threading.Tasks;
using TinyJewelryStore.Views;
using Xamarin.Forms;

namespace TinyJewelryStore.ViewModels.Services
{
    public class NavigationService : INavigationService
    {
        #region INavigationService implementation

        public async Task NavigateToLogin()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        public async Task NavigateToEstimateGoldPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EstimationGoldPage());
        }

        public async Task ClosePage()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }



        #endregion

        public NavigationService()
        {
        }
    }
}
