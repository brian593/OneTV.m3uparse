
namespace OneTV.m3uparse
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class M3u8Parser 
    {
     

        public async void ParseUrl(string urlFile)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(urlFile);
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();

            ParseContent(content);
        }
        private async void ParseContent(string Content)
        {
            var lines = Content.Split(new[] { "#EXTINF" }, StringSplitOptions.None);
            
            foreach (var item in lines)
            {
                string[] words = item.Split(' ').ToArray();
                foreach (var word in words)
                {
                    if (IsUrl(word))
                    {
                        Console.WriteLine(word);
                    }
                }

            }
        }
        private static bool IsUrl(string url)
        {
            string pattern = @"((https?|ftp|file)\://|www.)[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }




    }
    
}
