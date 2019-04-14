using App_Code;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    string carpeta;
    string txt_pruebas;
    string link;
    List<string> listaCapturas = new List<string>();
    string Descarga;

    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["usuario"] == null) || ((bool)Session["usuario"] == false))
        {
            Response.Redirect("login.aspx");
        }
        usuario.Text = Session["elUsuario"].ToString();
        Descarga = Path.Combine(Request.PhysicalApplicationPath, "Descarga");
        Descarga = "C:/Users/Vidal/Documents/ProgramaTesis/PlanATesisWeb/PlanATesisWeb/Descarga";
        if (IsPostBack)
            return;

        DB db = new DB();
        MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
        int usuario_id = db.getIdUsuarioDb(Session["elUsuario"].ToString());
        MySqlCommand cmd = new MySqlCommand($"SELECT id_cliente,nombre FROM clientes  WHERE  usuario_id = '{usuario_id}'", myConnection);
        
        myConnection.Open();
        
        DropDownList1.DataSource = cmd.ExecuteReader();
        DropDownList1.DataTextField = "nombre";
        DropDownList1.DataValueField = "id_cliente";
        DropDownList1.DataBind();

        

        myConnection.Close();

        //int algo = db.getIdClienteDb(DropDownList1.Text);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('el usuario es: {DropDownList1.Text} ')", true);


    }

    protected void BtnPrueba_Click(object sender, EventArgs e)
    {

        //Label2.Attributes.Add("class", "lds-dual-ring");
        //Response.Redirect("Default.aspx");

        //carga.Attributes.Add("class ", "lds-dual-ring");
        if (Url.Text != null && Url.Text != "")
        {
            if (Url.Text.Contains("https://") || Url.Text.Contains("http://"))
            {

                TimeSpan stop;
                TimeSpan start = new TimeSpan(DateTime.Now.Ticks);


                Algo a = new Algo(); //objeto prueba
                listaCapturas = a.HacerPrueba(Url.Text, "C:/Users/Vidal/Desktop/prueba");
                Pdf pdf = new Pdf();//objeto pdf
                DB guardarPDF = new DB();


                DateTime dateTime = DateTime.UtcNow.Date;
                
                string nombre = Session["elUsuario"].ToString() + dateTime.ToString("d");
                nombre = nombre.Replace("/", "-");
                nombre = nombre.Replace(" ", "--");
                nombre = nombre.Replace(":", "-");

                stop = new TimeSpan(DateTime.Now.Ticks);
                //Console.WriteLine(stop.Subtract(start).TotalMilliseconds);


                //nombre = nombre.Replace(" ", "");
                int idUsuario = guardarPDF.getIdUsuarioDb(Session["elUsuario"].ToString());    //id usuario 
                string direccion = pdf.CrearPDF("C:/Users/Vidal/Desktop/prueba", Url.Text, listaCapturas, nombre);

                string tkdireccion = direccion;
                tkdireccion = tkdireccion.Replace("C:/Users/Vidal/Documents/ProgramaTesis/PlanATesisWeb/PlanATesisWeb/Descarga/", "");
                
                //int cliente = guardarPDF.getIdClienteDb(DropDownList1.Text);
                guardarPDF.ingresarDatosPdfDB( direccion , Convert.ToInt16(DropDownList1.Text), stop.Subtract(start).TotalMilliseconds);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('La Prueba a terminado satisfactoriamente')", true);
                DB baseBorrar = new DB();
                baseBorrar.borrarTodosElementosBaseDeDatos();

                Url.Text = "";
                //------------------ Seccion descarga




                Response.ContentType = "aplication/pdf";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AppendHeader($"Content-Disposition", $"attachment;filename=\'{tkdireccion}'");
                Response.TransmitFile(direccion);
                Response.End();
                //Label1.Text = "EXITO";
                //-------------------FIN seccion descarga

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Por favor ingresar un Link valido')", true);
            }
            //Label1.Text = "LISTO";

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Por favor ingresar el Link a evaluar')", true);
        }
        //loading.Attributes.Add("class ", "loaderx");
        //carga.Attributes.Add("class ", "");
        //Label2.Attributes.Add("class", "");
        //Response.Redirect("Default.aspx");


    }

  

    protected void Url_TextChanged(object sender, EventArgs e)
    {

    }



    protected void cerrar_sesion_Click(object sender, EventArgs e)
    {
        Session["usuario"] = false;
        Session["elUsuario"] = "";
        Response.Redirect("login.aspx");
    }


}