using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Interface
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string url = "https://www.comboinfinito.com.br/principal/category/games/";
            Console.WriteLine("Iniciando a conexão com o webcrawler...");
            HtmlDocument htmlDocument = StartCrawler(url);
            
            Console.WriteLine("Buscando dados do site...\n\n");
            List<GameNews> gamenews = SelectDataComboInfinito(htmlDocument);

            ViewDataComboInfinito(gamenews);

            Console.WriteLine("Deseja salvar os dados no banco? [1 = sim | 2 = nao]");
            var input = Console.ReadLine();
            if(Int32.Parse(input) == 1)
            {
                Console.WriteLine("Salvando dados no banco...");
                DataSaveChanges(gamenews);
            }

            Console.WriteLine("Deseja apagar o banco? [1 = sim | 2 = nao]");
            input = Console.ReadLine();
            if (Int32.Parse(input) == 1)
            {
                Console.WriteLine("Apagando dados...");
                DeleteChanges();
            }
        }

        /// <summary>
        /// Iniciar o crawler de acordo com a url
        /// </summary>
        /// <param name="url"> url de um site para extração </param>
        /// <returns> Objeto doc html </returns>
        private static HtmlDocument StartCrawler(string url)
        {
            var httpClient = new WebClient();
            try
            {
                //Efetua o download da pagina html
                var page = httpClient.DownloadString(url);
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(page);
                return htmlDocument;
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERRO: " + e.Message + "]");
            }
            return null;
        }
    
        /// <summary>
        /// Captura os dados da pagina html
        /// </summary>
        /// <param name="htmlDocument"> Objeto que contem html ordenado </param>
        /// <returns> Lista de notícias </returns>
        private static List<GameNews> SelectDataComboInfinito(HtmlDocument htmlDocument)
        {
            //Captura de conteudo principal (recorte definido) do html de acordo com sua classe
             var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("article-info")).ToList();
            List<GameNews> listNews = null;

            //Verificação de objeto vazio ou nulo
            if (divs != null && divs.Count > 0)
            {
                listNews = new List<GameNews>();
                foreach (var div in divs)
                {
                    //Objeto
                    var gameNew = new GameNews();

                    //Autor
                    var nodes = div.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("authorship type-both")).ToList();
                    gameNew.Autor = HttpUtility.HtmlDecode(nodes.FirstOrDefault().Descendants("a").FirstOrDefault().InnerText);

                    //Captura a data do html e converte para datetime de acordo com o padrão brasileiro
                    var dataNodes = nodes.LastOrDefault().Descendants("span").LastOrDefault().InnerText;
                    var dataSplit = dataNodes.Split(";");
                    var date = dataSplit[2].Trim().Split("/");
                    gameNew.Date = new DateTime(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]));

                    //Descrição
                    nodes = div.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("excerpt")).ToList();
                    gameNew.Description = nodes.FirstOrDefault().Descendants("div").FirstOrDefault().InnerText;

                    //Titulo
                    gameNew.Title = HttpUtility.HtmlDecode(div.Descendants("a").FirstOrDefault().InnerText);                    
                    
                    //Link
                    gameNew.Link = div.Descendants("a").FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value;
                    
                    //Adição na lista de notícias
                    listNews.Add(gameNew); 
                }
            }
            return listNews;
        }
        
        /// <summary>
        /// Salvar dados no banco
        /// </summary>
        /// <param name="gameNews"></param>
        private static void DataSaveChanges(List<GameNews> gameNews)
        {
            WebCrawlerContext wcc = new WebCrawlerContext();
            wcc.StartConnection();

            if(gameNews != null)
            {
                foreach (var gn in gameNews)
                {
                    wcc.GameNews.Add(gn);
                    wcc.SaveChanges();
                }
            }
        }
    
        /// <summary>
        /// Apagar todo o banco
        /// </summary>
        private static void DeleteChanges()
        {
            WebCrawlerContext wcc = new WebCrawlerContext();
            wcc.StartConnection();

            wcc.Database.ExecuteSqlCommand("TRUNCATE TABLE [WebCrawlerDB].[dbo].[GameNews]");
        }

        /// <summary>
        /// Exibir dados adquiridos pelo HTML
        /// </summary>
        /// <param name="gameNews"></param>
        private static void ViewDataComboInfinito(List<GameNews> gameNews)
        {
            if(gameNews != null)
            {
                Console.WriteLine("============================");
                Console.WriteLine("         L I S T A          ");
                Console.WriteLine("============================");
                foreach (var gn in gameNews)
                {
                    Console.WriteLine("Titulo: " + gn.Title);
                    Console.WriteLine("Descrição: " + gn.Description);
                    Console.WriteLine("Link: " + gn.Link);
                    Console.WriteLine("Autor: " + gn.Autor);
                    Console.WriteLine("Publicado em: " + gn.Date.ToString() + "\n\n");
                }
                Console.WriteLine("============================");
            }
            else
            {
                Console.WriteLine("Não há dados disponiveis");
            }
            
        }
    }
}
