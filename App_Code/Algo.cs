using Herramientas;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;


namespace App_Code
{
    public class Algo
    {
        
        DB db = new DB();

        public List<string> HacerPrueba(string link, string carpeta)
        {


            List<string> listaCapturas = new List<string>();
            try
            {
                //StreamWriter sw = new StreamWriter($"{carpeta}/Test.txt");  //TODO: el txt por si a caso
                //StreamReader leerPruebas = new StreamReader($"");
                List<string> ListaPruebas = new List<string>();

                //while (!leerPruebas.EndOfStream)
                //{
                //    ListaPruebas.Add(leerPruebas.ReadLine());
                //}
                //leerPruebas.Close();
                //foreach (var elemento in ListaPruebas)
                //{
                //    Console.WriteLine(elemento);
                //    Thread.Sleep(200);
                //}

                ListaPruebas = db.listaPruebasDB();
                //sw.Close();    
                //iniciamos el chromeDriver
                var options = new ChromeOptions();
                //options.AddArgument("--disable-gpu");

                var chromeDriver = new ChromeDriver(options);
                Thread.Sleep(7000);
                chromeDriver.Manage().Window.Maximize();
                chromeDriver.Navigate().GoToUrl(link);


                Thread.Sleep(7000);


                var titulos = chromeDriver.FindElementsByTagName("input");



                List<Selector> selector = new List<Selector>();

                List<string> listaID = new List<string>();
                List<string> listaName = new List<string>();
                List<string> listaType = new List<string>();

                //------------------    ------------------------------extraigo los id y name------------------------------------------------


                foreach (var titulo in titulos)
                {
                    try
                    {
                        //Console.WriteLine(titulo.Text);


                        if ((titulo.GetAttribute("id") != null) && (titulo.GetAttribute("id") != "" && titulo.GetAttribute("id") != "--no-sandbox"))
                        {
                            Selector sl = new Selector();
                            //sw.WriteLine("--------------------------------------------------------------------------------");
                            Console.WriteLine("--------------------------------------------------------------------------------");
                            Console.WriteLine("id:    " + titulo.GetAttribute("id"));
                            Console.WriteLine("type:  " + titulo.GetAttribute("type"));
                            Console.WriteLine("name:  " + titulo.GetAttribute("name"));


                            sl.Id = Convert.ToString(titulo.GetAttribute("id"));
                            //sw.WriteLine(titulo.GetAttribute("id"));
                            sl.Type = Convert.ToString(titulo.GetAttribute("type"));
                            //sw.WriteLine(titulo.GetAttribute("type"));
                            sl.Name = Convert.ToString(titulo.GetAttribute("name"));
                            //sw.WriteLine(titulo.GetAttribute("name"));

                            selector.Add(sl);
                        }
                        //Console.WriteLine("name:  " + titulo.GetAttribute("name"));
                        Console.WriteLine("");

                        Thread.Sleep(100);
                    }
                    catch (Exception) { }



                }


                //----------------------------------------------------------------------------fin busqueda de selectores -----------------------------------
                //----------------------------------------------------------------le relizo pruebas a los formularios y los evaluo

                //TODO: listo ya se guardan las imagenes en la base de datos

                ScreenCapture sc = new ScreenCapture();
                Image img = sc.CaptureScreen();
                //extraigo la fecha y hora para el nombre de la carpeta
                //DateTime thisDay = DateTime.Today;



                string direccionCapturas = $"{carpeta}/"; // aqui tengo que ver como colocar la base de datos
                string formato = ".png";
                //sc.CaptureScreenToFile(direccionCapturas + "Test" + formato, ImageFormat.Png);
                //chromeDriver.Quit();


                //Console.WriteLine("llego hasta aqui");

                foreach (var elementoID in selector)
                {
                    //Console.WriteLine("");
                    //Console.WriteLine(elementoID.Type);
                    //Console.WriteLine("");
                    if (elementoID.Type == "text" || elementoID.Type == "email" || elementoID.Type == "password")
                    {
                        var ingreso = chromeDriver.FindElementById(elementoID.Id);
                        try
                        {
                            foreach (var elemento in ListaPruebas)
                            {
                                Console.WriteLine("id: " + elementoID);
                                //sw.WriteLine("id: " + elementoID);

                                Thread.Sleep(1000);
                                ingreso.SendKeys(elemento);
                                if (ingreso.Text != null)
                                {

                                    Thread.Sleep(500);
                                    //sw.WriteLine($"En el id:{elementoID.Id} el texto {elemento} esta permitido");
                                    Console.WriteLine($"En el id:{elementoID.Id} el texto {elemento} esta permitido");
                                    ingreso.Submit();
                                    Thread.Sleep(400);
                                    ingreso = chromeDriver.FindElementById(elementoID.Id);
                                    //sc.CaptureScreenToFile(direccionCapturas + elementoID.Id + elemento + formato, ImageFormat.Png);
                                    //TODO: metodo para almacenar en memoria
                                    img = sc.CaptureScreen();
                                    MemoryStream stream = new MemoryStream();
                                    img.Save(stream, ImageFormat.Png);

                                    db.IngresarImagenesDB(stream, elementoID.Id + elemento + formato);

                                    listaCapturas.Add(direccionCapturas + elementoID.Id + elemento + formato);

                                }

                                else
                                {
                                    Console.WriteLine("texto no permitido");
                                    //sw.WriteLine("texto no permitido");
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
                    //else if (elementoID.Type == "radio")
                    //{
                    //    Console.WriteLine("id: " + elementoID);
                    //    sw.WriteLine("id: " + elementoID);
                    //    var ingreso = chromeDriver.FindElementById(elementoID.Id);
                    //    Thread.Sleep(1000);
                    //    ingreso.Click();
                    //    if (ingreso.Selected)
                    //    {
                    //        Thread.Sleep(500);
                    //        sw.WriteLine($"En el id esta seleccionado");
                    //        Console.WriteLine($"En el id esta seleccionado");
                    //        sc.CaptureScreenToFile(direccionCapturas + elementoID.Id + "nombre" + formato, ImageFormat.Png);
                    //        //listaCapturas.Add(direccionCapturas + elementoID.Id + "nombre" + formato+".png");
                    //    }

                    //    else
                    //    {
                    //        Console.WriteLine("no permitido");
                    //        sw.WriteLine("no permitido");
                    //    }
                    //}

                }



                //sw.Close();    //--------------------------------------------------------------------------------bloqueado tempotalmente 
                               //foreach (var elementoName in listaName)
                               //{
                               //    Console.WriteLine("name: " + elementoName);
                               //}




                chromeDriver.Close();
                chromeDriver.Quit();
                return listaCapturas;

            }
            catch (Exception e)
            {
                return listaCapturas;
            }

        }


        //---------------------------------------------------------------------------------------------parte 2 PRUEBA MATRIZ----------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------




        //public void HacerPruebaMatriz(string link, string carpeta, string txt_pruebas)
        public List<string> HacerPruebaMatriz(string link, string carpeta, string txt_pruebas)
        {
            List<string> listaCapturas = new List<string>();
            try
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
                //sw.Close();
                //iniciamos el chromeDriver
                var options = new ChromeOptions();
                //options.AddArgument("--disable-gpu");

                var chromeDriver = new ChromeDriver(options);     //The chromedriver.exe file does not exist in the current directory or in a directory on the PATH environment variable
                Thread.Sleep(7000);
                chromeDriver.Manage().Window.Maximize();
                chromeDriver.Navigate().GoToUrl(link);


                Thread.Sleep(7000);


                var titulos = chromeDriver.FindElementsByTagName("input");



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
                        var ingreso = chromeDriver.FindElementById(elementoID.Id);
                        try
                        {
                            foreach (var elemento in ListaPruebas)
                            {
                                Console.WriteLine("id: " + elementoID);
                                sw.WriteLine("id: " + elementoID);

                                Thread.Sleep(1000);
                                ingreso.SendKeys(elemento);
                                if (ingreso.Text != null)
                                {
                                    Thread.Sleep(500);
                                    sw.WriteLine($"En el id:{elementoID.Id} el texto {elemento} esta permitido");
                                    Console.WriteLine($"En el id:{elementoID.Id} el texto {elemento} esta permitido");
                                    ingreso.Submit();
                                    
                                    //ingreso = chromeDriver.FindElementById(elementoID.Id);
                                    sc.CaptureScreenToFile(direccionCapturas + elementoID.Id + elemento + formato, ImageFormat.Png);
                                    listaCapturas.Add(direccionCapturas + elementoID.Id + elemento + formato);
                                    Thread.Sleep(400);
                                }

                                else
                                {
                                    Console.WriteLine("texto no permitido");
                                    sw.WriteLine("texto no permitido");
                                }
                                //ingreso.Clear();
                                Thread.Sleep(500);
                            }


                        }
                        catch (Exception)
                        {
                            //Console.WriteLine($"no pueden ingresar datos en le id: {elementoID}");
                        }
                    }
                    //else if (elementoID.Type == "radio")
                    //{
                    //    Console.WriteLine("id: " + elementoID);
                    //    sw.WriteLine("id: " + elementoID);
                    //    var ingreso = chromeDriver.FindElementById(elementoID.Id);
                    //    Thread.Sleep(1000);
                    //    ingreso.Click();
                    //    if (ingreso.Selected)
                    //    {
                    //        Thread.Sleep(500);
                    //        sw.WriteLine($"En el id esta seleccionado");
                    //        Console.WriteLine($"En el id esta seleccionado");
                    //        sc.CaptureScreenToFile(direccionCapturas + elementoID.Id + "nombre" + formato, ImageFormat.Png);
                    //        //listaCapturas.Add(direccionCapturas + elementoID.Id + "nombre" + formato+".png");
                    //    }

                    //    else
                    //    {
                    //        Console.WriteLine("no permitido");
                    //        sw.WriteLine("no permitido");
                    //    }
                    //}

                }



                sw.Close();    //--------------------------------------------------------------------------------bloqueado tempotalmente 
                               //foreach (var elementoName in listaName)
                               //{
                               //    Console.WriteLine("name: " + elementoName);
                               //}




                chromeDriver.Close();
                chromeDriver.Quit();
                return listaCapturas;

            }
            catch (Exception e)
            {
                return listaCapturas;
            }

        }







    }

}

