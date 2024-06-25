using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;


namespace ICBFapp.Pages.Jardin
{

    public class CrearJardinModel : PageModel
    {
        public JardinInfo jardinInfo = new JardinInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            jardinInfo.Nombre_Jardin = Request.Form["Nombre_Jardin"];
            jardinInfo.Direccion = Request.Form["Direccion"];
            jardinInfo.Estado = Request.Form["Estado"];

            if (jardinInfo.Nombre_Jardin.Length == 0 || jardinInfo.Direccion.Length == 0 || jardinInfo.Estado.Length == 0)
            {
                errorMessage = "Debe llenar todos los campos";
            }

            try
            {
                //Ruta Andres
                //String connectionString = "Data Source=DESKTOP-64KJT59;Initial Catalog=ICBFweb;Integrated Security = True";
                //Ruta Fernanda
                String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False";



                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    //Espacio para validar si el jardin no existe
                    String sqlInsert = "INSERT INTO Registro_Jardin (Nombre_Jardin, Direccion, Estado) " +
                   "VALUES ( @Nombre_Jardin, @Direccion, @Estado)";



                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {

                        command.Parameters.AddWithValue("@Nombre_Jardin", jardinInfo.Nombre_Jardin);
                        command.Parameters.AddWithValue("@direccion", jardinInfo.Direccion);
                        command.Parameters.AddWithValue("@estado", jardinInfo.Estado);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage += e.Message;
                return;
            }

            jardinInfo.Nombre_Jardin = ""; jardinInfo.Direccion = ""; jardinInfo.Estado = "";
            successMessage = "El jardin fue agregado correctamente";
            Response.Redirect("/Jardin/Index");
        }

        private string ObtenerProximoIdentificador_Jardin()
        {
            throw new NotImplementedException();
        }

        public class JardinInfo
        {
            internal object Identificador_Jardin;

            public string Nombre_Jardin { get; set; } = string.Empty;
            public string Direccion { get; set; } = string.Empty;
            public string Estado { get; set; } = string.Empty;
        }
    }
}



