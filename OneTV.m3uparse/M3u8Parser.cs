
namespace OneTV.m3uparse
{
    using OneTV.m3uparse.Entitie;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
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
            ObservableCollection<Item> Lista = new ObservableCollection<Item>();
            Item itemAux = new Item();
            foreach (var item in lines)
            {
                
                string[] words = item.Split(' ').ToArray();
                foreach (var word in words)
                {
                    if (IsUrl(word))
                    {
                        if (word.Contains("tvg-logo="))
                        {
                            var a=  word.Remove(0, 10).Replace('"',' ').Replace(',', ' ');
                            Console.WriteLine("Imagen:"+a);
                            itemAux.Logo = a;
                        }
                        else
                        {
                            itemAux.Url = word;
                            Console.WriteLine("Url:" + word);
                        }
                    }
                }
                Lista.Add(itemAux);
                itemAux = new Item();
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
