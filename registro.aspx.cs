using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class registro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (name.Text != "" && apellido.Text != "" && email.Text != "" && pass.Text != "" &&  name.Text != null && apellido.Text != null && email.Text != null && pass.Text != null)
        {
            DB db = new DB();
            if (db.verificarUsuarioRegistrado(name.Text))
            {

                db.ingresarUsuario(name.Text, apellido.Text, email.Text, pass.Text);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Registro satisfactorio)", true);
                Response.Redirect("login.aspx");


                //db.IngresarClientesDB(Session["elUsuario"].ToString(), TextBox_agregarCliente.Text);

                //TextBox_agregarCliente.Text = "";
                //Response.Redirect("clientes.aspx");
                ////ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('cliente agregado con exito')", true);

                ////ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('error al agregar cliente')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('El usuario ya se encuentra registrado')", true);
            }
            //DB registro = new DB();
            //registro.ingresarUsuario(name.Text, apellido.Text, email.Text, pass.Text);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Registro satisfactorio)", true);
            //Response.Redirect("login.aspx");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Por favor ingrese todo los argumentos del formulario')", true);
        }
    }
}