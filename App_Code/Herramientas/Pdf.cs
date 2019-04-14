using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Herramientas
{
    class Pdf
    {
        public void CrearPDF(string carpeta, string link,List<string> listaCaptura,string txt_pruebas)
        {
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc,new FileStream(carpeta+"/prueba.pdf", FileMode.Create));

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("PDF test");
            doc.AddCreator("Ana Guzman");

            // Abrimos el archivo
            doc.Open();

            //-------------------------------

            Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Font _standardFont3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            //Font imagen = new iTextSharp.text.Font(iTextSharp.text.Font.);
            // Escribimos el encabezamiento en el documento
            doc.Add(new Paragraph("Evaluacion de la pagina - " + link));
            doc.Add(Chunk.NEWLINE);

            // Creamos una tabla que contendrá el nombre, apellido y país
            // de nuestros visitante.
            PdfPTable tblPrueba = new PdfPTable(3);
            tblPrueba.WidthPercentage = 100;

            // Configuramos el título de las columnas de la tabla
            PdfPCell selector = new PdfPCell(new Phrase("SELECTOR", _standardFont2));
            selector.BorderWidth = 0;
            selector.BorderWidthBottom = 0.75f;

            PdfPCell caso = new PdfPCell(new Phrase("CASO", _standardFont2));
            caso.BorderWidth = 0;
            caso.BorderWidthBottom = 0.75f;

            PdfPCell captura = new PdfPCell(new Phrase("CAPTURA", _standardFont2));
            captura.BorderWidth = 0;
            captura.BorderWidthBottom = 0.75f;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(selector);
            tblPrueba.AddCell(caso);
            tblPrueba.AddCell(captura);


            //colocar el ciclo aqui 



           

            StreamReader leerPruebas = new StreamReader($"{txt_pruebas}");
            List<string> ListaPruebas = new List<string>();
            while (!leerPruebas.EndOfStream)
            {
                ListaPruebas.Add(leerPruebas.ReadLine());
            }
            leerPruebas.Close();


            foreach (string elemento in listaCaptura)
            {
                var imagenCaptura = crearImagen(elemento);

                string valorSelector = elemento.Replace(carpeta + "/","");
                string valorSelectorSinPng = valorSelector.Replace(".png", "");
                string valorFinalSelector = recortarTexto(valorSelectorSinPng, ListaPruebas);
                string valorFinalPrueba = valorSelectorSinPng.Replace(valorFinalSelector, "");

                selector = new PdfPCell(new Phrase($"Prueba realizada al selector ID: \n \n{valorFinalSelector}", _standardFont)) ;
                caso = new PdfPCell(new Phrase($"En este caso se le ingreso al selector  ID:  \n {valorFinalSelector} el valor  {valorFinalPrueba } y el resultado fue el siguiente", _standardFont));

                tblPrueba.AddCell(selector);
                tblPrueba.AddCell(caso);
                tblPrueba.AddCell(imagenCaptura);
            }




            //imprime los elementos de la tabla
            doc.Add(tblPrueba);
            //doc.Add(imagen);


            doc.Close();
            writer.Close();
        }

        private iTextSharp.text.Image crearImagen(string direccionImagen)
        {
            //  /firstname-0f2bb815-9809-4ed4-9d6e-64aabcff16c1_6060Ana.png

            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(direccionImagen);
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_RIGHT;
            float percentage = 0.0f;
            percentage = 150 / imagen.Width;
            imagen.ScalePercent(percentage * 100);

            return imagen;
        }
        private string recortarTexto(string texto, List<string> listaP)
        {
            
            foreach (string elementoPruebas in listaP)
            {
                texto = texto.Replace(elementoPruebas, "");
            }
            return texto;
        }

    }
}
