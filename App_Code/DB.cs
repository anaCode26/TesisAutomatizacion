using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de DB
/// </summary>
public class DB
{
    public DB()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");

    public byte[] GetImage(string nombre)
    {

        string mySelectQuery = "SELECT (elemento) FROM (elementos) WHERE nombre = @nombre";
        //MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
        MySqlCommand myCommand = new MySqlCommand(mySelectQuery, myConnection);
        myConnection.Open();


        MySqlCommand cmd = new MySqlCommand(mySelectQuery, myConnection);
        cmd.Parameters.AddWithValue("@nombre", nombre);

        MySqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            var cosa = (byte[])reader["elemento"];
            reader.Close();
            myConnection.Close();
            return cosa;       //System.IndexOutOfRangeException: 'Could not find specified column in results: campoimg'

        }
        else
        {
            reader.Close();
            myConnection.Close();
            return null;
        }



    }
    public byte[] GetPDF()
    {

        string mySelectQuery = "SELECT (testpdf) FROM (pdf)";
        //MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
        MySqlCommand myCommand = new MySqlCommand(mySelectQuery, myConnection);
        myConnection.Open();
        MySqlCommand cmd = new MySqlCommand(mySelectQuery, myConnection);


        MySqlDataReader reader = cmd.ExecuteReader();       //MySql.Data.MySqlClient.MySqlException: 'Table 'pruebas.pdf' doesn't exist'

        if (reader.Read())
        {
            var cosa = (byte[])reader["testpdf"];
            reader.Close();
            myConnection.Close();
            return cosa;       //System.IndexOutOfRangeException: 'Could not find specified column in results: campoimg'

        }
        else
        {
            reader.Close();
            myConnection.Close();
            return null;
        }



    }

    public List<String> listaPruebasDB()
    {
        string mySelectQuery = "SELECT (test) FROM prueba";
        //MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
        MySqlCommand myCommand = new MySqlCommand(mySelectQuery, myConnection);
        myConnection.Open();     //MySql.Data.MySqlClient.MySqlException: 'Unable to connect to any of the specified MySQL hosts.'
        MySqlDataReader myReader;
        myReader = myCommand.ExecuteReader();
        // Siempre llame a Leer antes de acceder a los datos. 
        List<string> listaPruebas = new List<string>();
        while (myReader.Read())
        {
            //Console.WriteLine(myReader.GetInt32(0) + "," + myReader.GetString(1));
            listaPruebas.Add(myReader.GetString(0));
        }
        // Siempre llama Cerrar cuando termines de leer.
        myReader.Close();
        // Cierre la conexión cuando termine.
        myConnection.Close();

        return listaPruebas;
    }
    public string getRecomendacion(string test)
    {
        string mySelectQuery = $"SELECT (recomendacion) FROM prueba WHERE test = '{test}'";
        //MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
        MySqlCommand myCommand = new MySqlCommand(mySelectQuery, myConnection);
        myConnection.Open();     //MySql.Data.MySqlClient.MySqlException: 'Unable to connect to any of the specified MySQL hosts.'
        MySqlDataReader myReader;
        myReader = myCommand.ExecuteReader();
        // Siempre llame a Leer antes de acceder a los datos. 
        //List<string> listaPruebas = new List<string>();
        myReader.Read();
        //while (myReader.Read())
        //{
        //    //Console.WriteLine(myReader.GetInt32(0) + "," + myReader.GetString(1));
        //    listaPruebas.Add(myReader.GetString(0));
        //}
        // Siempre llama Cerrar cuando termines de leer.
        string recomendacion = myReader.GetString(0);
        myReader.Close();
        // Cierre la conexión cuando termine.
        myConnection.Close();

        return recomendacion;
    }




    public void IngresarImagenesDB(Stream stream, string tituloImg)
    {
        try
        {
            //obtener datos de la imagen 


            Image returnImage = Image.FromStream(stream);
            //insertar en la base de datos

            //MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
            MySqlCommand myCommand = new MySqlCommand();

            var userImage = ImageToByteArray(returnImage);


            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "INSERT INTO elementos(nombre,elemento) VALUES(@nombre, @elemento)";
            cmd.Parameters.Add("@elemento", MySqlDbType.MediumBlob).Value = userImage;
            cmd.Parameters.Add("@nombre", MySqlDbType.Text).Value = tituloImg;

            cmd.CommandType = CommandType.Text;
            cmd.Connection = myConnection;

            myConnection.Open();
            cmd.ExecuteNonQuery();

            myConnection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

    }
    public Image byteArrayToImage(byte[] byteBLOBData)
    {
        MemoryStream ms = new MemoryStream(byteBLOBData);
        Image returnImage = Image.FromStream(ms);
        return returnImage;
    }
    public byte[] ImageToByteArray(System.Drawing.Image imageIn)
    {
        using (var ms = new MemoryStream())
        {
            imageIn.Save(ms, imageIn.RawFormat);
            return ms.ToArray();
        }
    }

    public void guardandoPDF(MemoryStream stream, string nombre = "prueba")
    {
        try
        {

            var userPDF = stream.ToArray();



            //MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
            MySqlCommand myCommand = new MySqlCommand();



            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "INSERT INTO pdftest(nombre,test) VALUES(@nombre, @test)";
            cmd.Parameters.Add("@test", MySqlDbType.MediumBlob).Value = userPDF;
            cmd.Parameters.Add("@nombre", MySqlDbType.Text).Value = nombre;

            cmd.CommandType = CommandType.Text;
            cmd.Connection = myConnection;

            myConnection.Open();
            cmd.ExecuteNonQuery();

            myConnection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }


    public void borrarTodosElementosBaseDeDatos()
    {
        //MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
        MySqlCommand cmd = new MySqlCommand("DELETE  FROM elementos", myConnection);
        myConnection.Open();
        cmd.ExecuteNonQuery();
        myConnection.Close();
    }
    public void ingresarUsuario(string nombre, string apellido, string correo, string pass)
    {
        //MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
        MySqlCommand cmd = new MySqlCommand($"INSERT INTO usuarios(nombre,apellido,correo,contraseña) VALUES ('{nombre}','{apellido}','{correo}','{pass}')", myConnection);
        myConnection.Open();
        cmd.ExecuteNonQuery();
        myConnection.Close();

    }
    public bool ingresarSession(string nombre, string pass)
    {
        object obj;

        //MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
        MySqlCommand cmd = new MySqlCommand($"SELECT * FROM usuarios WHERE  (nombre = '{nombre}'  AND  contraseña = '{pass}') ", myConnection);
        myConnection.Open();


        obj = cmd.ExecuteScalar();
        if (Convert.ToInt32(obj) != 0)
        {
            myConnection.Close();
            return true;
        }
        else
        {
            myConnection.Close();
            return false;
        }
    }

    public int getIdUsuarioDb(string nombre)
    {
        int salida;
        try
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT id_usuarios FROM usuarios WHERE nombre = '{nombre}'", myConnection);
            myConnection.Open(); //abrimos conexion
                                 //Leemos nuestra tabla
            salida = Convert.ToInt32(cmd.ExecuteScalar());

            myConnection.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("error de conexion" + ex);
            return 0;
        }
        return salida;
    }

    public int getIdClienteDb(string nombre)
    {
        int salida;
        try
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT id_cliente FROM clientes WHERE nombre = '{nombre}'", myConnection);
            myConnection.Open(); //abrimos conexion
                                 //Leemos nuestra tabla
            salida = Convert.ToInt32(cmd.ExecuteScalar());

            myConnection.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("error de conexion" + ex);
            return 0;
        }
        return salida;
    }

    public string getDireccionDb(int id_pdf)
    {

        //MySqlDataAdapter cmd = new MySqlDataAdapter($"SELECT (direccion) FROM pdf WHERE id_pdf = '{id_pdf}'", myConnection);
        //DataTable dt = new DataTable();
        //cmd.Fill(dt);



        //string direccion = dt.Rows[0].ToString();
        //return direccion;
        MySqlCommand myCommand = new MySqlCommand($"SELECT (direccion) FROM pdf WHERE id_pdf = '{id_pdf}'", myConnection);
        myConnection.Open();     //MySql.Data.MySqlClient.MySqlException: 'Unable to connect to any of the specified MySQL hosts.'
        MySqlDataReader myReader;
        myReader = myCommand.ExecuteReader();
        
        List<string> listaDireccion = new List<string>();

        while (myReader.Read())
        {
            //    //Console.WriteLine(myReader.GetInt32(0) + "," + myReader.GetString(1));
            listaDireccion.Add(myReader.GetString(0));
        }
        
        string algo = listaDireccion.First();
        
        myReader.Close();
        myConnection.Close();

        return algo;



    }

    public void ingresarDatosPdfDB(string direccion, int cliente_id,double segundos)
    {

        DateTime dateTime = DateTime.UtcNow.Date;
        string formatForMySql = dateTime.ToString("yyyy-MM-dd");
        string tkdireccion = direccion;
        tkdireccion = tkdireccion.Replace("C:/Users/Vidal/Documents/ProgramaTesis/PlanATesisWeb/PlanATesisWeb/Descarga/", "");
        string nombre = "prueba" + dateTime.ToString("d");

        MySqlCommand cmd = new MySqlCommand($"INSERT INTO pdf(nombre,direccion,fecha,segundos,cliente_id) VALUES ('{tkdireccion}','{direccion}','{formatForMySql}','{segundos}','{cliente_id}')", myConnection);
        myConnection.Open();
        cmd.ExecuteNonQuery();   //MySql.Data.MySqlClient.MySqlException: 'Column count doesn't match value count at row 1'
        myConnection.Close();

    }
    public void IngresarClientesDB(string usuario, string cliente)
    {
        int usuario_id = this.getIdUsuarioDb(usuario);
        MySqlCommand cmd = new MySqlCommand($"INSERT INTO clientes(nombre,usuario_id) VALUES ('{cliente}','{usuario_id}')", myConnection);
        myConnection.Open();
        cmd.ExecuteNonQuery();   //MySql.Data.MySqlClient.MySqlException: 'Column count doesn't match value count at row 1'
        myConnection.Close();
    }

    public DataTable getClientes(string usuario)
    {
        int usuario_id = this.getIdUsuarioDb(usuario);
        MySqlDataAdapter cmd = new MySqlDataAdapter($"SELECT id_cliente,nombre FROM clientes  WHERE  usuario_id = '{usuario}'", myConnection);
        DataTable dt = new DataTable();
        cmd.Fill(dt);
        return dt;
    }

    public void borrarCliente(string nombre)
    {
        int idCliente = this.getIdClienteDb(nombre);
        MySqlCommand cmd = new MySqlCommand($"DELETE FROM  pdf WHERE cliente_id = '{idCliente}'", myConnection);
        myConnection.Open();
        cmd.ExecuteNonQuery();   //MySql.Data.MySqlClient.MySqlException: 'Column count doesn't match value count at row 1'
        MySqlCommand cmd2 = new MySqlCommand($"DELETE FROM clientes WHERE id_cliente =  '{idCliente}'", myConnection);
        cmd2.ExecuteNonQuery();   //MySql.Data.MySqlClient.MySqlException: 'Column count doesn't match value count at row 1'

        myConnection.Close();

    }

    public bool verificarClienteRegistrado(string nombre)
    {
        object obj;
        //MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
        MySqlCommand cmd = new MySqlCommand($"SELECT id_cliente FROM clientes WHERE nombre = '{nombre}'", myConnection);
        myConnection.Open();


        obj = cmd.ExecuteScalar();
        if (Convert.ToInt32(obj) != 0)
        {
            myConnection.Close();
            return false;
        }
        else
        {
            myConnection.Close();
            return true;
        }


    }

    public bool verificarUsuarioRegistrado(string nombre)
    {
        object obj;
        //MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
        MySqlCommand cmd = new MySqlCommand($"SELECT id_usuarios FROM usuarios WHERE nombre = '{nombre}'", myConnection);
        myConnection.Open();


        obj = cmd.ExecuteScalar();
        if (Convert.ToInt32(obj) != 0)
        {
            myConnection.Close();
            return false;
        }
        else
        {
            myConnection.Close();
            return true;
        }


    }

    public int cantidadPdf(int cliente_id)
    {
        string mySelectQuery = $"SELECT nombre FROM pdf WHERE cliente_id =   '{cliente_id}'   ";
        //MySqlConnection myConnection = new MySqlConnection("Server = localhost ; UserID =  root; Database=pruebas");
        MySqlCommand myCommand = new MySqlCommand(mySelectQuery, myConnection);
        myConnection.Open();     //MySql.Data.MySqlClient.MySqlException: 'Unable to connect to any of the specified MySQL hosts.'
        MySqlDataReader myReader;
        myReader = myCommand.ExecuteReader();
        // Siempre llame a Leer antes de acceder a los datos. 
        List<string> listaPruebas = new List<string>();
        while (myReader.Read())
        {
            //Console.WriteLine(myReader.GetInt32(0) + "," + myReader.GetString(1));
            listaPruebas.Add(myReader.GetString(0));
        }
        // Siempre llama Cerrar cuando termines de leer.
        myReader.Close();
        // Cierre la conexión cuando termine.
        myConnection.Close();

        return listaPruebas.Count;
    }

    

}