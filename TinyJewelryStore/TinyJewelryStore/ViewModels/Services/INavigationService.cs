using System.Threading.Tasks;
using TinyJewelryStore.models;

namespace TinyJewelryStore.ViewModels
{
   public interface INavigationService
    {
        Task NavigateToLogin();
        Task NavigateToEstimateGoldPage();

        Task ClosePage();
    }
}
