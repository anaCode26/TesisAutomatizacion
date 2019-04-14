using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Ingresar_Click(object sender, EventArgs e)
    {
        string nombre_ususario = user.Text;
        DB session = new DB();
        if (session.ingresarSession(user.Text, pass.Text) == true )
        {
            Session["usuario"] = true;
            Session["elUsuario"] = nombre_ususario;
            Response.Redirect("Default.aspx");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Usuario no encontrado')", true);
        }
    }
}