using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class Pdf
{
    private PdfPCell byteArrayToImage;

    public string CrearPDF(string carpeta, string link, List<string> listaCapturas, string nombre)
    {
        int longitud = 7;
        Guid miGuid = Guid.NewGuid();
        string token = Convert.ToBase64String(miGuid.ToByteArray());
        token = token.Replace("=", "").Replace("+", "").Replace("/", "");
        //Console.WriteLine(token.Substring(0, longitud));

        //C:/Users/Vidal/Documents/ProgramaTesis/PlanATesisWeb/PlanATesisWeb/Descarga/


        Document doc = new Document(PageSize.LETTER);
        // Indicamos donde vamos a guardar el documento
        //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(carpeta + "/prueba.pdf", FileMode.Create));  //TODO: el original que funciona  PDF
        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream($"C:/Users/Vidal/Documents/ProgramaTesis/PlanATesisWeb/PlanATesisWeb/Descarga/{nombre + token}.pdf", FileMode.Create));
        //MemoryStream streamPDF = new MemoryStream();
        //TODO: Aqui colocare la direccion:
        string direccion = $"C:/Users/Vidal/Documents/ProgramaTesis/PlanATesisWeb/PlanATesisWeb/Descarga/{nombre+token}.pdf";



        //PdfWriter writer = PdfWriter.GetInstance(doc, streamPDF);   //System.UnauthorizedAccessException: 'Acceso denegado a la ruta de acceso 'C:\Program Files (x86)\IIS Express\~prueba.pdf'.'


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
        //StreamReader leerPruebas = new StreamReader($"{txt_pruebas}");
        List<string> ListaPruebas = new List<string>();
        //while (!leerPruebas.EndOfStream)
        //{
        //    ListaPruebas.Add(leerPruebas.ReadLine());
        //}
        //leerPruebas.Close();

        DB db = new DB();
        ListaPruebas = db.listaPruebasDB();



        foreach (string elemento in listaCapturas)         //TODO: lista de imagenes, cambiar por imagenes de la base de datos
        {

            string valorSelector = elemento.Replace(carpeta + "/", "");
            MySqlConnection connection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
            DB sacandoImagen = new DB();
            var imageBytes = sacandoImagen.GetImage(valorSelector);
            MemoryStream buf = new MemoryStream(imageBytes);     //System.ArgumentNullException: 'El búfer no puede ser nulo. Nombre del parámetro: buffer'
            System.Drawing.Image image = System.Drawing.Image.FromStream(buf, true);
            MemoryStream stream = new MemoryStream(imageBytes);
            image.Save(stream, ImageFormat.Png);  //System.NullReferenceException: 'Referencia a objeto no establecida como instancia de un objeto.'   img fue null.
            var imagenCaptura = crearImagenDb(imageBytes);  //en esta seccion se extraen las imagenes y se colocan en el pdf

            string valorSelectorSinPng = valorSelector.Replace(".png", "");
            string valorFinalSelector = recortarTexto(valorSelectorSinPng, ListaPruebas);
            string valorFinalPrueba = valorSelectorSinPng.Replace(valorFinalSelector, "");
            string textoRecomendacion;
            textoRecomendacion = sacandoImagen.getRecomendacion(valorFinalPrueba);
            selector = new PdfPCell(new Phrase($"Prueba realizada al selector ID: \n \n{valorFinalSelector}", _standardFont));
            caso = new PdfPCell(new Phrase($"En este caso se le ingreso al selector  ID:  \n\n {valorFinalSelector} el valor  {valorFinalPrueba } y se concluye:  \n-{textoRecomendacion} ", _standardFont));

            DB dbTransformacion = new DB();
            tblPrueba.AddCell(selector);
            tblPrueba.AddCell(caso);
            tblPrueba.AddCell(imagenCaptura);
            //}
            //catch (Exception e) { }
        }

        //imprime los elementos de la tabla
        doc.Add(tblPrueba);
        //doc.Add(imagen);


        doc.Close();
        writer.Close();
        // return streamPDF;
        return direccion;
    }



    public MemoryStream CrearPDFdB(string carpeta, string link, List<string> listaCapturas)
    {
        Document doc = new Document(PageSize.LETTER);
        // Indicamos donde vamos a guardar el documento
        MemoryStream streamPDF = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(doc, streamPDF);  //TODO: el original que funciona  PDF



        //PdfWriter writer = PdfWriter.GetInstance(doc, streamPDF);   //System.UnauthorizedAccessException: 'Acceso denegado a la ruta de acceso 'C:\Program Files (x86)\IIS Express\~prueba.pdf'.'


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
        //StreamReader leerPruebas = new StreamReader($"{txt_pruebas}");
        List<string> ListaPruebas = new List<string>();
        //while (!leerPruebas.EndOfStream)
        //{
        //    ListaPruebas.Add(leerPruebas.ReadLine());
        //}
        //leerPruebas.Close();

        DB db = new DB();
        ListaPruebas = db.listaPruebasDB();



        foreach (string elemento in listaCapturas)         //TODO: lista de imagenes, cambiar por imagenes de la base de datos
        {



            string valorSelector = elemento.Replace(carpeta + "/", "");


            MySqlConnection connection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");


            DB sacandoImagen = new DB();
            var imageBytes = sacandoImagen.GetImage(valorSelector);

            MemoryStream buf = new MemoryStream(imageBytes);     //System.ArgumentNullException: 'El búfer no puede ser nulo. Nombre del parámetro: buffer'
            System.Drawing.Image image = System.Drawing.Image.FromStream(buf, true);


            MemoryStream stream = new MemoryStream(imageBytes);


            image.Save(stream, ImageFormat.Png);  //System.NullReferenceException: 'Referencia a objeto no establecida como instancia de un objeto.'   img fue null.

            var imagenCaptura = crearImagenDb(imageBytes);  //en esta seccion se extraen las imagenes y se colocan en el pdf



            string valorSelectorSinPng = valorSelector.Replace(".png", "");
            string valorFinalSelector = recortarTexto(valorSelectorSinPng, ListaPruebas);
            string valorFinalPrueba = valorSelectorSinPng.Replace(valorFinalSelector, "");
            string textoRecomendacion;
            textoRecomendacion = sacandoImagen.getRecomendacion(valorFinalPrueba);
            selector = new PdfPCell(new Phrase($"Prueba realizada al selector ID: \n \n{valorFinalSelector}", _standardFont));
            caso = new PdfPCell(new Phrase($"En este caso se le ingreso al selector  ID:  \n\n {valorFinalSelector} el valor  {valorFinalPrueba } y se concluye:   {textoRecomendacion} ", _standardFont));

            DB dbTransformacion = new DB();

            tblPrueba.AddCell(selector);
            tblPrueba.AddCell(caso);
            tblPrueba.AddCell(imagenCaptura);
            //}
            //catch (Exception e) { }
        }

        //imprime los elementos de la tabla
        doc.Add(tblPrueba);
        //doc.Add(imagen);


        doc.Close();
        //MemoryStream salida = new MemoryStream();
        //streamPDF.CopyTo(salida);
        writer.Close();
        return streamPDF;
    }


    private iTextSharp.text.Image crearImagenDb(byte[] imagenDB)
    {
        //  /firstname-0f2bb815-9809-4ed4-9d6e-64aabcff16c1_6060Ana.png

        iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(imagenDB);
        imagen.BorderWidth = 0;
        imagen.Alignment = Element.ALIGN_RIGHT;
        float percentage = 0.0f;
        percentage = 150 / imagen.Width;
        imagen.ScalePercent(percentage * 100);

        return imagen;
    }

    private iTextSharp.text.Image crearImagen(Stream direccionImagen)
    {
        //  /firstname-0f2bb815-9809-4ed4-9d6e-64aabcff16c1_6060Ana.png

        iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(direccionImagen);     //System.IndexOutOfRangeException: 'Índice fuera de los límites de la matriz.'
        imagen.BorderWidth = 0;
        imagen.Alignment = Element.ALIGN_RIGHT;
        float percentage = 0.0f;
        percentage = 150 / imagen.Width;
        imagen.ScalePercent(percentage * 100);

        return imagen;
    }
    public string recortarTexto(string texto, List<string> listaP)
    {

        foreach (string elementoPruebas in listaP)
        {
            texto = texto.Replace(elementoPruebas, "");
        }
        return texto;
    }
}