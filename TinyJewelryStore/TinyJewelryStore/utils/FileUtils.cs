using System;
using System.IO;
using Xamarin.Forms.PlatformConfiguration;

namespace TinyJewelryStore.utils
{
    public static class FileUtils
    {
      public static string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "invoiceofgoldproduct.txt");

    }
}
