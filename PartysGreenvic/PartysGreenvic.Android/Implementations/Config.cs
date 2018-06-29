[assembly: Xamarin.Forms.Dependency(typeof(PartysGreenvic.Droid.Implementations.Config))]
namespace PartysGreenvic.Droid.Implementations
{
    using Interfaces;
    using SQLite.Net.Interop;
    public class Config : IConfig
    {
        private string directoryBD;
        private ISQLitePlatform platform;

        public string DirectoryBD
        {
            get
            {
                if (string.IsNullOrEmpty(directoryBD))
                {
                    directoryBD = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
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
                    platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }
                return platform;
            }
        }
    }
}