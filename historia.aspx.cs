using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class historia : System.Web.UI.Page
{

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
        //MySqlCommand cmd = new MySqlCommand($"SELECT id_cliente,nombre FROM clientes  WHERE  usuario_id = '{usuario_id}'", myConnection);
        MySqlDataAdapter cmd = new MySqlDataAdapter($"SELECT id_cliente,nombre FROM clientes  WHERE  usuario_id = '{usuario_id}'", myConnection);
        DataTable tabla = new DataTable();
        cmd.Fill(tabla);
        //myConnection.Open();

        //DropDownList1.DataSource = cmd.ExecuteReader();
        DropDownList1.DataSource = tabla;
        DropDownList1.DataTextField = "nombre";
        DropDownList1.DataValueField = "id_cliente";
        DropDownList1.DataBind();





    }

    protected void cerrar_sesion_Click(object sender, EventArgs e)
    {
        Session["usuario"] = false;
        Session["elUsuario"] = "";
        Response.Redirect("login.aspx");
    }



    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DB db = new DB();
        //int[] barras = new int[3];//POR AHORA 3
        //string[] fechas = new string[3];//POR AHORA


        int elemento = GridView1.SelectedIndex;
        GridViewRow row = GridView1.SelectedRow;

        string mensaje = Convert.ToString(GridView1.DataKeys[elemento].Values["id_pdf"]);
        string nombre = Session["elUsuario"].ToString();
        //string direccion = $"C:/Users/Vidal/Documents/ProgramaTesis/PlanATesisWeb/PlanATesisWeb/Descarga/{mensaje}.pdf";
        //TODO: voy a extraer la direccion

        //string direccion = db.getDireccionDb(Convert.ToInt16(GridView1.DataKeys[elemento].Values["id_pdf"]));
        //string tkdireccion = direccion;
        //tkdireccion = tkdireccion.Replace("C:/Users/Vidal/Documents/ProgramaTesis/PlanATesisWeb/PlanATesisWeb/Descarga/", "");

        mensaje = mensaje.Replace("/", "-");
        mensaje = mensaje.Replace(" ", "--");
        mensaje = mensaje.Replace(":", "-");
        mensaje = mensaje.Replace("prueba", nombre);
        //string tkdireccion2 = tkdireccion.Replace(".pdf", "");
        //C:/Users/Vidal/Documents/ProgramaTesis/PlanATesisWeb/PlanATesisWeb/Descarga/Ana03-02-2019.pdf
        Response.ContentType = "aplication/pdf";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader($"Content-Disposition", $"attachment;filename=\'{GridView1.DataKeys[elemento].Values["nombre"]}'");
        Response.TransmitFile($"C:/Users/Vidal/Documents/ProgramaTesis/PlanATesisWeb/PlanATesisWeb/Descarga/{GridView1.DataKeys[elemento].Values["nombre"]}");
        Response.End();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('el usuario es: {GridView1.DataKeys[elemento].Values["nombre"] } ')", true);
       

    }

    protected void btn_cliente_Click(object sender, EventArgs e)
    {

        
        

        //try
        //{
            DB llenarHistoria = new DB();
            //GridView1.DataSource = llenarHistoria.recuperaDaatosPdfHistoria(llenarHistoria.getIdUsuarioDb(Session["elUsuario"].ToString()));


            MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
            myConnection.Open();

            GridView1.DataBind();
            MySqlDataAdapter adaptador = new MySqlDataAdapter($"SELECT    id_pdf,nombre , fecha   FROM pdf WHERE cliente_id = '{DropDownList1.SelectedItem.Value}'", myConnection);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            if (tabla.Rows.Count == 0)
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('No hay reportes')", true);
            //
            GridView1.DataSource = tabla;
            GridView1.DataBind();

            Label1.Text = "";
            //TODO: colocar aqui un if  
            //int cantidadElementos = Convert.ToInt32( DropDownList1.SelectedItem.Value);
            //double[] barras = new double[llenarHistoria.cantidadPdf(cantidadElementos)];//POR AHORA 3
            //string[] fechas = new string[llenarHistoria.cantidadPdf(cantidadElementos)];//POR AHORA

            //int count = 0;
            ////MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
            //MySqlCommand cmd = new MySqlCommand($"SELECT  fecha ,segundos, cliente_id FROM pdf  WHERE  cliente_id = '{DropDownList1.SelectedItem.Value}'", myConnection);
            //MySqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    barras[count] = Convert.ToDouble(reader.GetString(1));
            //    fechas[count] = reader.GetString(0);
            //    count++;
            //}
            //reader.Close();
            //myConnection.Close();

            //Grafica.Series["Series"].Points.DataBindXY(fechas,barras);

        //}
        //catch (Exception ex)
        //{
        //    Label1.Text = ("ERROR");
        //}


    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}