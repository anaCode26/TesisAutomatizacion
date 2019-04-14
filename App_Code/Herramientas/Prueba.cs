using Herramientas;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Herramientas
{
    class Prueba
    {
        public Prueba() { }

        public List<string> HacerPrueba(string link, string carpeta,string txt_pruebas)
        {

            StreamWriter sw = new StreamWriter($"{carpeta}/Test.txt");
            StreamReader leerPruebas = new StreamReader($"{txt_pruebas}");
            List<string> ListaPruebas = new List<string>();
            while (!leerPruebas.EndOfStream)
            {
                ListaPruebas.Add(leerPruebas.ReadLine());
            }
            leerPruebas.Close();
            foreach (var elemento in ListaPruebas)
            {
                Console.WriteLine(elemento);
                Thread.Sleep(200);
            }
            //iniciamos el chromeDriver
            var options = new ChromeOptions();
            //options.AddArgument("--disable-gpu");

            var chromeDriver = new ChromeDriver(options);
            Thread.Sleep(7000);
            chromeDriver.Navigate().GoToUrl(link);   //The chromedriver.exe file does not exist in the current directory or in a directory on the PATH environment variable

            var titulos = chromeDriver.FindElementsByTagName("input");


            List<string> listaCapturas = new List<string>();
            List<Selector> selector = new List<Selector>();

            List<string> listaID = new List<string>();
            List<string> listaName = new List<string>();
            List<string> listaType = new List<string>();

            //------------------------------------------------extraigo los id y name------------------------------------------------


            foreach (var titulo in titulos)
            {
                try
                {
                    //Console.WriteLine(titulo.Text);


                    if ((titulo.GetAttribute("id") != null) && (titulo.GetAttribute("id") != "" && titulo.GetAttribute("id") != "--no-sandbox"))
                    {
                        Selector sl = new Selector();
                        sw.WriteLine("--------------------------------------------------------------------------------");
                        Console.WriteLine("--------------------------------------------------------------------------------");
                        Console.WriteLine("id:    " + titulo.GetAttribute("id"));
                        Console.WriteLine("type:  " + titulo.GetAttribute("type"));
                        Console.WriteLine("name:  " + titulo.GetAttribute("name"));


                        sl.Id = Convert.ToString(titulo.GetAttribute("id"));
                        sw.WriteLine(titulo.GetAttribute("id"));
                        sl.Type = Convert.ToString(titulo.GetAttribute("type"));
                        sw.WriteLine(titulo.GetAttribute("type"));
                        sl.Name = Convert.ToString(titulo.GetAttribute("name"));
                        sw.WriteLine(titulo.GetAttribute("name"));

                        selector.Add(sl);
                    }
                    //Console.WriteLine("name:  " + titulo.GetAttribute("name"));
                    Console.WriteLine("");
                 
                    Thread.Sleep(100);
                }
                catch (Exception) { }



            }


            //----------------------------------------------------------------------------fin busqueda de selectores -----------------------------------
            //le relizo pruebas a los formularios y los evaluo
            ScreenCapture sc = new ScreenCapture();
            Image img = sc.CaptureScreen();
            //extraigo la fecha y hora para el nombre de la carpeta
            //DateTime thisDay = DateTime.Today;

            string direccionCapturas = $"{carpeta}/";
            string formato = ".png";
            sc.CaptureScreenToFile(direccionCapturas + "Test" + formato, ImageFormat.Png);
            //chromeDriver.Quit();


            //Console.WriteLine("llego hasta aqui");

            foreach (var elementoID in selector)
            {
                //Console.WriteLine("");
                //Console.WriteLine(elementoID.Type);
                //Console.WriteLine("");
                if (elementoID.Type == "text" || elementoID.Type == "email" || elementoID.Type == "password")
                {

                    try
                    {
                        foreach (var elemento in ListaPruebas)
                        {
                            Console.WriteLine("id: " + elementoID);
                            sw.WriteLine("id: " + elementoID);
                            var ingreso = chromeDriver.FindElementById(elementoID.Id);
                            Thread.Sleep(1000);
                            ingreso.SendKeys(elemento);
                            if (ingreso.Text != null)
                            {
                                Thread.Sleep(500);
                                sw.WriteLine($"En el id:{elementoID.Id} el texto {elemento} esta permitido");
                                Console.WriteLine($"En el id:{elementoID.Id} el texto {elemento} esta permitido");
                                ingreso.Submit();
                                sc.CaptureScreenToFile(direccionCapturas + elementoID.Id + elemento + formato, ImageFormat.Png);
                                listaCapturas.Add(direccionCapturas + elementoID.Id + elemento +  formato );

                            }

                            else
                            {
                                Console.WriteLine("texto no permitido");
                                sw.WriteLine("texto no permitido");
                            }
                            ingreso.Clear();
                            Thread.Sleep(500);
                        }

                        
                    }
                    catch (Exception)
                    {
                        //Console.WriteLine($"no pueden ingresar datos en le id: {elementoID}");
                    }
                }
                else if (elementoID.Type == "radio")
                {
                    Console.WriteLine("id: " + elementoID);
                    sw.WriteLine("id: " + elementoID);
                    var ingreso = chromeDriver.FindElementById(elementoID.Id);
                    Thread.Sleep(1000);
                    ingreso.Click();
                    if (ingreso.Selected)
                    {
                        Thread.Sleep(500);
                        sw.WriteLine($"En el id esta seleccionado");
                        Console.WriteLine($"En el id esta seleccionado");
                        sc.CaptureScreenToFile(direccionCapturas + elementoID.Id + "nombre" + formato, ImageFormat.Png);
                        //listaCapturas.Add(direccionCapturas + elementoID.Id + "nombre" + formato+".png");
                    }

                    else
                    {
                        Console.WriteLine("no permitido");
                        sw.WriteLine("no permitido");
                    }
                }

            }
            


            sw.Close();
            //foreach (var elementoName in listaName)
            //{
            //    Console.WriteLine("name: " + elementoName);
            //}




            chromeDriver.Close();
            chromeDriver.Quit();
            return listaCapturas;
        }


        //---------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------




    //    public void HacerPruebaMatriz(string link, string carpeta, string txt_pruebas)
    //    {



    //        StreamWriter sw = new StreamWriter($"{carpeta}/Test.txt");
    //        StreamReader leerPruebas = new StreamReader($"{txt_pruebas}");
    //        List<string> ListaPruebas = new List<string>();
    //        while (!leerPruebas.EndOfStream)
    //        {
    //            ListaPruebas.Add(leerPruebas.ReadLine());
    //        }
    //        leerPruebas.Close();
    //        foreach (var elemento in ListaPruebas)
    //        {
    //            Console.WriteLine(elemento);
    //            Thread.Sleep(200);
    //        }
    //        iniciamos el chromeDriver
    //        var options = new ChromeOptions();
    //        options.AddArgument("--disable-gpu");

    //        var chromeDriver = new ChromeDriver(options);
    //        Thread.Sleep(7000);
    //        chromeDriver.Navigate().GoToUrl(link);

    //        var titulos = chromeDriver.FindElementsByTagName("input");



    //        List<Selector> selector = new List<Selector>();

    //        List<string> listaID = new List<string>();
    //        List<string> listaName = new List<string>();
    //        List<string> listaType = new List<string>();

    //        ------------------------------------------------extraigo los id y name------------------------------------------------


    //        foreach (var titulo in titulos)
    //        {
    //            try
    //            {
    //                Console.WriteLine(titulo.Text);


    //                if ((titulo.GetAttribute("id") != null) && (titulo.GetAttribute("id") != "" && titulo.GetAttribute("id") != "--no-sandbox"))
    //                {
    //                    Selector sl = new Selector();
    //                    sw.WriteLine("--------------------------------------------------------------------------------");
    //                    Console.WriteLine("--------------------------------------------------------------------------------");
    //                    Console.WriteLine("id:    " + titulo.GetAttribute("id"));
    //                    Console.WriteLine("type:  " + titulo.GetAttribute("type"));
    //                    Console.WriteLine("name:  " + titulo.GetAttribute("name"));


    //                    sl.Id = Convert.ToString(titulo.GetAttribute("id"));
    //                    sw.WriteLine(titulo.GetAttribute("id"));
    //                    sl.Type = Convert.ToString(titulo.GetAttribute("type"));
    //                    sw.WriteLine(titulo.GetAttribute("type"));
    //                    sl.Name = Convert.ToString(titulo.GetAttribute("name"));
    //                    sw.WriteLine(titulo.GetAttribute("name"));

    //                    selector.Add(sl);
    //                }
    //                Console.WriteLine("name:  " + titulo.GetAttribute("name"));
    //                Console.WriteLine("");

    //                Thread.Sleep(100);
    //            }
    //            catch (Exception) { }



    //        }


    //        ----------------------------------------------------------------------------fin busqueda de selectores---------------------------------- -
    //        le relizo pruebas a los formularios y los evaluo
    //        ScreenCapture sc = new ScreenCapture();
    //        Image img = sc.CaptureScreen();
    //        extraigo la fecha y hora para el nombre de la carpeta
    //        DateTime thisDay = DateTime.Today;

    //        string direccionCapturas = $"{carpeta}/";
    //        string formato = ".png";
    //        sc.CaptureScreenToFile(direccionCapturas + "Test" + formato, ImageFormat.Png);
    //        chromeDriver.Quit();

    //        foreach (var elementoID in selector)
    //        {

    //            if (elementoID.Type == "text" || elementoID.Type == "email" || elementoID.Type == "password")
    //            {

    //                try
    //                {
    //                    foreach (var elemento in ListaPruebas)
    //                    {
    //                        Console.WriteLine("id: " + elementoID);
    //                        sw.WriteLine("id: " + elementoID);
    //                        var ingreso = chromeDriver.FindElementById(elementoID.Id);
    //                        Thread.Sleep(1000);
    //                        ingreso.SendKeys(elemento);
    //                        if (ingreso.Text != null)
    //                        {
    //                            Thread.Sleep(500);
    //                            sw.WriteLine($"En el id:{elementoID.Id} el texto {elemento} esta permitido");
    //                            Console.WriteLine($"En el id:{elementoID.Id} el texto {elemento} esta permitido");
    //                            ingreso.Submit();
    //                            sc.CaptureScreenToFile(direccionCapturas + elementoID.Id + elemento + formato, ImageFormat.Png);
    //                            break;

    //                        }

    //                        else
    //                        {
    //                            Console.WriteLine("texto no permitido");
    //                            sw.WriteLine("texto no permitido");
    //                        }
    //                        ingreso.Clear();
    //                        Thread.Sleep(500);
    //                    }

    //                }
    //                catch (Exception e) { Console.WriteLine("ERROR"); }
    //            }
    //        }


    //        Console.WriteLine("llego hasta aqui");
    //        for (int a = 0; a > ListaPruebas.Count; a++)
    //        {

    //            foreach (var elementoID in selector)
    //            {

    //                if (elementoID.Type == "text" || elementoID.Type == "email" || elementoID.Type == "password")
    //                {

    //                    try
    //                    {
    //                        foreach (var elemento in ListaPruebas)
    //                        {
    //                            Console.WriteLine("id: " + elementoID);
    //                            sw.WriteLine("id: " + elementoID);
    //                            var ingreso = chromeDriver.FindElementById(elementoID.Id);
    //                            Thread.Sleep(1000);
    //                            ingreso.SendKeys(elemento + a);
    //                            if (ingreso.Text != null)
    //                            {
    //                                Thread.Sleep(500);
    //                                sw.WriteLine($"En el id:{elementoID.Id} el texto {elemento} esta permitido");
    //                                Console.WriteLine($"En el id:{elementoID.Id} el texto {elemento} esta permitido");
    //                                ingreso.Submit();
    //                                sc.CaptureScreenToFile(direccionCapturas + elementoID.Id + elemento + formato, ImageFormat.Png);
    //                                break;

    //                            }

    //                            else
    //                            {
    //                                Console.WriteLine("texto no permitido");
    //                                sw.WriteLine("texto no permitido");
    //                            }
    //                            ingreso.Clear();
    //                            Thread.Sleep(500);
    //                        }

    //                    }
    //                    catch (Exception)
    //                    {
    //                        //Console.WriteLine($"no pueden ingresar datos en le id: {elementoID}");
    //                    }
    //                }
    //                else if (elementoID.Type == "radio")
    //                {
    //                    Console.WriteLine("id: " + elementoID);
    //                    sw.WriteLine("id: " + elementoID);
    //                    var ingreso = chromeDriver.FindElementById(elementoID.Id);
    //                    Thread.Sleep(1000);
    //                    ingreso.Click();
    //                    if (ingreso.Selected)
    //                    {
    //                        Thread.Sleep(500);
    //                        sw.WriteLine($"En el id esta seleccionado");
    //                        Console.WriteLine($"En el id esta seleccionado");
    //                        sc.CaptureScreenToFile(direccionCapturas + elementoID.Id + "nombre" + formato, ImageFormat.Png);

    //                    }

    //                    else
    //                    {
    //                        Console.WriteLine("no permitido");
    //                        sw.WriteLine("no permitido");
    //                    }
    //                }

    //            }
    //        }



    //        sw.Close();
    //        foreach (var elementoName in listaName)
    //        {
    //            Console.WriteLine("name: " + elementoName);
    //        }




    //        chromeDriver.Close();
    //        chromeDriver.Quit();

    //    }








     }

}

