using System;
using System.IO;
using Xamarin.Forms;

namespace Scorpion.Services
{
    public class PathService
    {
        private static readonly string LocalAppPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db");
        private static readonly string RootPath = "/storage/emulated/0"; //сделать так чтобы можно было выбрать откуда брать базу
        private static readonly string ExportPath = "/storage/emulated/0/database.db";

        public static string GetLocalAppPath => LocalAppPath;
        public static string GetRootPath => RootPath;
        public static string GetExportDbPath => ExportPath;
    }
}
//     public static string dbOldPath = "/storage/emulated/0/database_old.db";