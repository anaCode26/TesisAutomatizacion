using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Herramientas
{
    class Scraping
    {
        public static void ScrapingWeb()
        {

            var options = new ChromeOptions();
            options.AddArgument("--disable-gpu");
            var chromeDriver = new ChromeDriver(options);
            chromeDriver.Navigate().GoToUrl("https://reddit.com");
            Thread.Sleep(10000);
            var titulos = chromeDriver.FindElementsByClassName("title");
            StreamWriter sw = new StreamWriter("C:/Users/jv/Desktop/Test.txt");
            foreach (var titulo in titulos)
            {
                
                sw.WriteLine(titulo.Text);
            }


            sw.Close();
                  
         }
            
        



    }
    
}
