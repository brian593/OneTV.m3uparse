
namespace OneTV.m3uparse
{
    using OneTV.m3uparse.Entitie;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class M3u8Parser 
    {
     

        public async Task<ObservableCollection<Item>> ParseUrl(string urlFile)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(urlFile);
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();          
            return await ParseContent(content);
        }
        private async Task<ObservableCollection<Item>> ParseContent(string Content)
        {
            var itemobj = Content.Split(new[] { "#EXTINF" }, StringSplitOptions.None);
           
            ObservableCollection<Item> Lista = new ObservableCollection<Item>();
            Item itemAux = new Item();
            foreach (var item in itemobj)
            {
                var lines = item.Replace("\r", "").Split('\n');
                foreach (var itemline in lines.Select((Value, Index) => new { Value, Index }))
                {
                   // Console.WriteLine(itemline.Index + " " + itemline.Value);
                    string[] words = itemline.Value.Split(' ').ToArray();
                    foreach (var word in words)
                    {

                        if (IsUrl(word))
                        {
                            if (word.Contains("tvg-logo="))
                            {
                                var a = word.Remove(0, 10).Replace('"', ' ');
                                itemAux.Logo = a;
                                Console.WriteLine("Logo: "+a);
                            }
                            else
                            {
                                itemAux.Url = word;
                                Console.WriteLine("Url: "+word);

                            }
                        }
                        else
                        {
                            if (word.Contains("group-title="))
                            {
                                var a = word.Remove(0, 13).Replace('"', ' ');
                                if (a.Length > 1)
                                {
                                    itemAux.Nombre = itemAux.Nombre + a;
                                }
                            }
                            else
                            {
                                if (!word.Contains('\n')) ;
                                var a = word.Replace('"', ' ');
                                if (a.Length > 1)
                                {
                                    itemAux.Nombre = itemAux.Nombre + a;
                                }
                            }
                        }
                    }                               
                }
                if (itemAux.Url != null) Lista.Add(itemAux);
                itemAux = new Item();
            }
            return Lista;
        }
        private static bool IsUrl(string url)
        {
            string pattern = @"((https?|ftp|file)\://|www.)[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }
    }    
}
