using System;
using System.IO;
using SitemapChecker.Data.Interfaces;
using System.Net.Http;
using System.Xml.Serialization;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;

namespace SitemapChecker.Data.Models
{
    public class UrlModel:IWorkWithUri
    {
        public string Url { get; set; }
        public List<string> Sitemaps { get; set; }
        private HttpClient client;
        public UrlModel()
        {
            client = new HttpClient();
        }
        public void GetSitemaps()
        {
            Sitemaps = GetLinks(Url+"/robots.txt", "Sitemap: ");
        }
        public Dictionary<string, string> GetResponceTime()
        {
            List<string> allLinks = new List<string>();
            Dictionary<string, string> websiteReponceTime = new Dictionary<string, string>();
            Stopwatch timer = new Stopwatch();
            HttpResponseMessage temp;
            foreach (var i in Sitemaps)
            {
                allLinks.AddRange(GetLinks(i, "<loc>", "</loc>"));
            }
            for (int i = 0; i < allLinks.Count; i++)
            {
                timer.Start();
                temp = client.GetAsync(allLinks[i]).Result;
                timer.Stop();
                websiteReponceTime.Add(allLinks[i], string.Format("{0:00}:{1:00}:{2:000} ms", timer.Elapsed.Minutes, timer.Elapsed.Seconds, timer.Elapsed.Milliseconds));
                timer.Reset();
            }
            return websiteReponceTime;
        }
        private List<string> GetLinks(string url, string startStr, string endStr = "\n")
        {
            List<string> listOfLinks = new List<string>();
            string[] sitemapLink = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result.Split(endStr);
            foreach (var str in sitemapLink)
            {
                if (str.Contains(startStr) && endStr == "\n")
                {
                   listOfLinks.Add(StringFormat(str,startStr));
                }
                else if (str.Contains(startStr) && endStr != "\n")
                {
                    listOfLinks.Add(StringFormat(str, startStr, endStr));
                }
            }
            return listOfLinks;
        }
        private string StringFormat(string str, string startStr, string endStr)
        {
            return str.Substring(str.IndexOf(startStr) + startStr.Length, str.Length - (str.IndexOf(startStr) + startStr.Length));

        }
        private string StringFormat(string str, string startStr)
        {
            return str.Substring(str.IndexOf(startStr) + startStr.Length);
        }
    }
}