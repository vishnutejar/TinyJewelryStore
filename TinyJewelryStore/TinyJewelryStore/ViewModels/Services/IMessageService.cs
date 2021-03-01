using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TinyJewelryStore.ViewModels.Services
{
    interface IMessageService
    {
        Task ShowAsync(string message);
    }
}
