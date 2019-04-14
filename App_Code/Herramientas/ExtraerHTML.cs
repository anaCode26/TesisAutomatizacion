using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Herramientas
{
    class ExtraerHTML
    {
        public static void SacandoHtml(string link)
        {

        }
        //internal static async Task<string> ObteneraPaginaWeb(HttpResponseMessage respuesta, Uri URL)
        //{
        //    string linea, contenido = "";
        //    HttpClient cliente = new HttpClient();
        //    linea = await respuesta.Content.ReadAsStringAsync();
        //    linea = linea.Replace("<br>", Environment.NewLine);
        //    contenido += linea;
        //    return contenido;
        //}

        public static  String getCodigo(String url)
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            // Realizamos la petición
            HttpWebResponse miPeticionWeb = (HttpWebResponse)myHttpWebRequest.GetResponse();
            // Obtenemos el flujo de la respuesta
            Stream datosRecibidos = miPeticionWeb.GetResponseStream();
            // Leemos el flujo de la respuesta obtenida, seleccionando el tipo de codificación que deseamos
            Encoding codificacion = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(datosRecibidos, codificacion);
            // Realizamos la conversión a String y devolvemos el valor
            return (readStream.ReadToEnd());
        }

        static public void almancenandoTXT(string texto)
        {
            try
            {

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter("C:/Users/jv/Desktop/Test.txt");

                //Write a line of text
                sw.WriteLine(texto);

                

                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        static void Leer()
        {
            StreamReader objReader = new StreamReader("C:/Users/jv/Desktop/Test.txt");
            string sLine = "";
            ArrayList arrText = new ArrayList();

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    arrText.Add(sLine);
            }
            objReader.Close();

            foreach (string sOutput in arrText)
                Console.WriteLine(sOutput);
            Console.ReadLine();
        }
        private String extraerValor(String cadena, String stringInicial, String stringFinal)
        {
            int terminaString = cadena.LastIndexOf(stringFinal);
            String nuevoString = cadena.Substring(0, terminaString);
            int offset = stringInicial.Length;
            int iniciaString = nuevoString.LastIndexOf(stringInicial) + offset;
            int cortar = nuevoString.Length - iniciaString;
            nuevoString = nuevoString.Substring(iniciaString, cortar);
            return nuevoString;
        }


    }
 
}
