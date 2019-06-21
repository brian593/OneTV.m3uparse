
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
            var itemobj = Content.Split(new[] { "#EXTINF" }, StringSplitOptions.None);
           
            ObservableCollection<Item> Lista = new ObservableCollection<Item>();
            Item itemAux = new Item();
            foreach (var item in itemobj)
            {
                Console.WriteLine("-----------------------------------------------------------------------------");
                var lines = item.Replace("\r", "").Split('\n');
                foreach (var itemline in lines.Select((Value, Index) => new { Value, Index }))
                {
                   // Console.WriteLine(itemline.Index + " " + itemline.Value);
                    string[] words = itemline.Value.Split(' ').ToArray();
                    int inicio=0, fin=0, cont=0;
                    foreach (var word in words.Select((Value, Index) => new { Value, Index }))
                    {

                        if (IsUrl(word.Value))
                        {
                            if (word.Value.Contains("tvg-logo="))
                            {


                                var a = word.Value.Remove(0, 10).Replace('"', ' ');
                                Console.WriteLine("Imagen:" + a);
                                itemAux.Logo = a;

                            }
                            else
                            {
                                //string[] miniwords = word.Split('\n').ToArray();
                                //if(miniwords.Length>1)
                                //{                                                                                                
                                //    Console.WriteLine("mnw1:" + miniwords[0]);
                                //    Console.WriteLine("mnw2:" + miniwords[1]);
                                //}

                                itemAux.Url = word.Value;
                                Console.WriteLine("Url:" + word.Value);
                            }

                        }
                        else
                        {
                            if (word.Value.Contains("group-title="))
                            {
                                var a = word.Value.Remove(0, 13).Replace('"', ' ');
                                Console.WriteLine("Imagen:" + a);
                            }
                            else
                            {
                                Console.WriteLine("No url:" + word.Value);
                            }

                        }

                    }
                   // Console.WriteLine("el inicio {0} y el fin{1}", inicio, fin);

                    Lista.Add(itemAux);
                    itemAux = new Item();

                    inicio = new int();
                    fin = new int();
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
