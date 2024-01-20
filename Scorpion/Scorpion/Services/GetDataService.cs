using System;
using System.Net;
using System.Threading.Tasks;

namespace Scorpion.Services
{
    public static class GetDataService
    {
        public static Task GetData()
        {
            Uri uri = new("http://62.220.53.49:5000/api/values");
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpWebRequest.Timeout = 20000;
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                WebClient webClient = new();
                webClient.DownloadFile(uri, PathService.GetLocalAppPath);
            }
            catch
            {
                SqliteAccess.CreateDb();
            }

            return Task.CompletedTask;
        }

        //этот метод нужен шоб когда рефреш жал и инета не было у тя твоя бд не замещалась пустой))
        public static Task GetDB()
        {
            Uri uri = new("http://62.220.53.49:5000/api/values");
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpWebRequest.Timeout = 20000;
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                WebClient webClient = new();
                webClient.DownloadFile(uri, PathService.GetLocalAppPath);
            }
            catch
            {
                SqliteAccess.CreateDb();
            }

            return Task.CompletedTask;
        }
    }
}


            // var isAvailable = false;
            // Ping png = new Ping();
            // PingReply pingReply = png.Send("http://62.220.53.49:5000/api/values");
            // if (pingReply.Status == IPStatus.Success)
            //     isAvailable = true;
            //
            // if (isAvailable == true)
            // {
            // }

            // webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;

        //private static void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        //{
        //    if (e.UserState is null)
        //    {
        //        SqliteAccess.CreateDb();
        //    }
        //}