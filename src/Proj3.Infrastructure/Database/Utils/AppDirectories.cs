namespace Proj3.Infrastructure.Database.Utils
{
    public static class AppDirectories
    {
        public static string getDatabasePath
        {
            get
            {
                string path = Directory.GetCurrentDirectory();
                return path.Substring(0, path.LastIndexOf("Proj3.Api")) + @"Proj3.Infrastructure\Database\SQLite\Database.db";
            }
        }
    }
}