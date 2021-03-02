using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TinyJewelryStore.ViewModels.Services
{
   public interface IBlueToothService
    {
        IList<string> GetDeviceList();
        Task Print(string deviceName, string text);
    }
}
