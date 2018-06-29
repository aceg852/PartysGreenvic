[assembly: Xamarin.Forms.Dependency(typeof(PartysGreenvic.iOS.Implementations.Config))]
namespace PartysGreenvic.iOS.Implementations
{
    using System;
    using Interfaces;
    using SQLite.Net.Interop;
    public class Config:IConfig
    {
        private string directoryBD;
        private ISQLitePlatform platform;
        public string DirectoryBD
        {
            get
            {
                if(string.IsNullOrEmpty(directoryBD))
                {
                    var directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    directoryBD = System.IO.Path.Combine(directory, "..", "Library");
                }
                return directoryBD;
            }
        }
        public ISQLitePlatform Platform
        {
            get
            {
                if (platform == null)
                {
                    platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
                }
                return platform;
            }
        }
    }
}