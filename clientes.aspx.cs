using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class clientes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["usuario"] == null) || ((bool)Session["usuario"] == false))
        {
            Response.Redirect("login.aspx");
        }
        usuario.Text = Session["elUsuario"].ToString();
        

        if (IsPostBack) //TODO: linea importante para que todo funcione 
            return;

        //DB llenarHistoria = new DB();
        //GridView1.DataSource = llenarHistoria.recuperaDaatosPdfHistoria(llenarHistoria.getIdUsuarioDb(Session["elUsuario"].ToString()));
        DB db = new DB();
        int usuario_id = db.getIdUsuarioDb(Session["elUsuario"].ToString());

        MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
        MySqlDataAdapter adaptador = new MySqlDataAdapter($"SELECT    nombre   FROM  clientes WHERE  usuario_id = '{usuario_id}' ", myConnection);
        myConnection.Open();
        DataTable tabla = new DataTable();
        adaptador.Fill(tabla);
        
        GridView1.DataSource = tabla;
        GridView1.DataBind();



    }

    protected void BtnPrueba2_Click(object sender, EventArgs e)
    {
        //carga.Attributes.Add("class ", "lds-dual-ring");
        //TODO: AGREGAR LO DE "EL CLIENTE YA SE ENCUENTRA REGISTRADO" y lo de formulario vacio 
        DB db = new DB();
        if (db.verificarClienteRegistrado(TextBox_agregarCliente.Text)  ) {
            

            db.IngresarClientesDB(Session["elUsuario"].ToString(), TextBox_agregarCliente.Text);

            TextBox_agregarCliente.Text = "";
            Response.Redirect("clientes.aspx");
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('cliente agregado con exito')", true);

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('error al agregar cliente')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('El cliente ya se encuentra registrado')", true);
        }


    }
    protected void cerrar_sesion_Click(object sender, EventArgs e)
    {
        Session["usuario"] = false;
        Session["elUsuario"] = "";
        Response.Redirect("login.aspx");
    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int elemento = GridView1.SelectedIndex;
        string mensaje = Convert.ToString(GridView1.DataKeys[elemento].Values["nombre"]);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "if(confirm('" + algo() + "')) alert('presiono aceptar'); else (alert('presiono cancelar'));", true);
        DB db =new DB();
        db.borrarCliente(mensaje);
       
        Response.Redirect("clientes.aspx");
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('El cliente {mensaje} y todos sus reportes han sido eliminados')", true);
    }
    public string algo()
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('se ejecuta el algo')", true);
        return "algo";
    }
    
}