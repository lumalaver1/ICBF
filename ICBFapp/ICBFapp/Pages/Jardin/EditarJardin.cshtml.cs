using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;



namespace ICBFapp.Pages.Jardin
{
    public class EditarJardinModel : PageModel
    {
        public JardinInfo jardinInfo = new JardinInfo();
        public string errorMessage = "";
        public string successMessage = "";

        //Ruta Andres
        //String connectionString = "Data Source=DESKTOP-64KJT59;Initial Catalog=ICBFweb;Integrated Security = True";
        //Ruta Fernanda
        String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False";

        public void OnGet()
        {
            String Identificador_Jardin = Request.Query["Identificador_Jardin"];

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Registro_Jardin WHERE Identificador_Jardin = @Identificador_Jardin";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Identificador_Jardin", jardinInfo);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                jardinInfo.Identificador_Jardin = reader.GetInt32(0).ToString();
                                jardinInfo.Nombre_Jardin = reader.GetString(1);
                                jardinInfo.Direccion = reader.GetString(2);
                                jardinInfo.Estado = reader.GetString(3);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
        }

        public void OnPost()
        {
            jardinInfo.Identificador_Jardin = Request.Form["Identificador_Jardin"];
            jardinInfo.Nombre_Jardin = Request.Form["Nombre_Jardin"];
            jardinInfo.Direccion = Request.Form["Direccion"];
            jardinInfo.Estado = Request.Form["Estado"];

            if (string.IsNullOrEmpty(jardinInfo.Identificador_Jardin) || string.IsNullOrEmpty(jardinInfo.Nombre_Jardin) || string.IsNullOrEmpty(jardinInfo.Estado) ||
                string.IsNullOrEmpty(jardinInfo.Direccion))
            {
                errorMessage = "Debe llenar todos los campos";
                return;
            }

            try
            {
                String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlUpdate = "UPDATE Registro_Jardin SET Nombre_Jardin = @nombreJardin, " +
                                       "Direccion = @direccionJardin, Estado = @estadoJardin " +
                                       "WHERE Identificador_Jardin = @Identificador_Jardin";
                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@Identificador_Jardin", jardinInfo.Identificador_Jardin);
                        command.Parameters.AddWithValue("@nombreJardin", jardinInfo.Nombre_Jardin);
                        command.Parameters.AddWithValue("@direccionJardin", jardinInfo.Direccion);
                        command.Parameters.AddWithValue("@estadoJardin", jardinInfo.Estado);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            successMessage = "El jardín fue actualizado correctamente";
            Response.Redirect("/Jardin/Index");
        }
    }
}
        public class JardinInfo
        {
            public string Identificador_Jardin { get; set; }
            public string Nombre_Jardin { get; set; }
            public string Direccion { get; set; }
            public string Estado { get; set; }
        }
 
