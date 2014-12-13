using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace LinkParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var aioLinks = new List<string>();

            for (int i = int.Parse(StartIndex.Text); i <= int.Parse(EndIndex.Text); i++)
            {
                try
                {
                    var titles = await GetTitleUrl(i);
                    foreach (var title in titles)
                    {
                        aioLinks.AddRange(await GetAiolinkUrl(title));
                    }
                }
                catch(Exception){}
            }

            var ret = new List<string>();
            foreach (var aioLink in aioLinks)
            {
                ret.AddRange(await GetFileHostLinks(aioLink));
            }

            File.WriteAllLines(string.Format("{0}-{1}.txt", StartIndex.Text, EndIndex.Text), ret);
        }

        private async Task<IEnumerable<string>> GetTitleUrl(int index)
        {
            var ret = new List<string>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Referrer = new Uri("http://jav365.com");

                using (var response = await client.GetAsync(string.Format("http://jav365.com/category/jav-censored/page/{0}/", index)))
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    const string startPattern = "<h3 class=\"entry-title\"><a href=\"";
                    const string endPattern = "\" rel=\"bookmark\"";
                    foreach (var searchStart in result.AllIndexesOf(startPattern))
                    {
                        var start = searchStart + startPattern.Length;
                        var end = result.IndexOf(endPattern, start, StringComparison.Ordinal);
                        if (end > 0)
                        {
                            ret.Add(result.Substring(start, end - start));
                        }
                    }
                }
            }
            return ret;
        }
        
        private async Task<IEnumerable<string>> GetLinkUrl(string titleUrl)
        {
            var ret = new List<string>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Referrer = new Uri("http://jav365.com");

                using (var response = await client.GetAsync(titleUrl))
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    const string startPattern = "http://jav365.com/dl";
                    const string endPattern = "\"";
                    foreach (var searchStart in result.AllIndexesOf(startPattern))
                    {
                        var start = searchStart;
                        var end = result.IndexOf(endPattern, start, StringComparison.Ordinal);
                        if (end > 0)
                        {
                            ret.Add(result.Substring(start, end - start));
                        }
                    }
                }
            }
            return ret;
        }

        private async Task<IEnumerable<string>> GetAiolinkUrl(string titleUrl)
        {
            var ret = new List<string>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Referrer = new Uri("http://jav365.com");

                using (var response = await client.GetAsync(titleUrl))
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    const string startPattern = "http://aiolinks.com";
                    const string endPattern = "\"";
                    foreach (var searchStart in result.AllIndexesOf(startPattern))
                    {
                        var start = searchStart;
                        var end = result.IndexOf(endPattern, start, StringComparison.Ordinal);
                        if (end > 0)
                        {
                            ret.Add(result.Substring(start, end - start));
                        }
                    }
                }
            }
            return ret;
        }

        private async Task<IEnumerable<string>> GetFileHostLinks(string aioLink)
        {
            var ret = new List<string>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Referrer = new Uri("http://jav365.com");

                using (var response = await client.GetAsync(aioLink))
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var startPattern = "http://rapidgator.net";
                    const string endPattern = "\"";
                    foreach (var searchStart in result.AllIndexesOf(startPattern))
                    {
                        var start = searchStart;
                        var end = result.IndexOf(endPattern, start, StringComparison.Ordinal);
                        if (end > 0)
                        {
                            ret.Add(result.Substring(start, end - start));
                        }
                    }

                    startPattern = "http://www.uploadable.ch";
                    foreach (var searchStart in result.AllIndexesOf(startPattern))
                    {
                        var start = searchStart;
                        var end = result.IndexOf(endPattern, start, StringComparison.Ordinal);
                        if (end > 0)
                        {
                            ret.Add(result.Substring(start, end - start));
                        }
                    }

                    startPattern = "http://uploadable.ch";
                    foreach (var searchStart in result.AllIndexesOf(startPattern))
                    {
                        var start = searchStart;
                        var end = result.IndexOf(endPattern, start, StringComparison.Ordinal);
                        if (end > 0)
                        {
                            ret.Add(result.Substring(start, end - start));
                        }
                    }

                    startPattern = "http://bitshare.com";
                    foreach (var searchStart in result.AllIndexesOf(startPattern))
                    {
                        var start = searchStart;
                        var end = result.IndexOf(endPattern, start, StringComparison.Ordinal);
                        if (end > 0)
                        {
                            ret.Add(result.Substring(start, end - start));
                        }
                    }

                    startPattern = "http://freakshare.com";
                    foreach (var searchStart in result.AllIndexesOf(startPattern))
                    {
                        var start = searchStart;
                        var end = result.IndexOf(endPattern, start, StringComparison.Ordinal);
                        if (end > 0)
                        {
                            ret.Add(result.Substring(start, end - start));
                        }
                    }
                }
            }
            return ret;
        }
    }
}
