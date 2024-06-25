using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace AppICBF.Pages.Jardin
{
    public class EditarJardinModel : PageModel
    {
        public JardinInfo JardinInfo { get; set; } = new JardinInfo();
        public string ErrorMessage { get; set; } = "";
        public string SuccessMessage { get; set; } = "";

        // Ruta de conexión a la base de datos
        // String connectionString = "Data Source=DESKTOP-64KJT59;Initial Catalog=ICBFweb;Integrated Security=True"; // Ruta Andres
        String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False"; // Ruta Fernanda

        public void OnGet()
        {
            String identificadorJardin = Request.Query["Id"];

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Registro_Jardin WHERE Identificador_Jardin = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", identificadorJardin);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                JardinInfo.Identificador_Jardin = reader.GetInt32(0).ToString();
                                JardinInfo.Nombre_Jardin = reader.GetString(1);
                                JardinInfo.Direccion = reader.GetString(2);
                                JardinInfo.Estado = reader.GetString(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public IActionResult OnPost()
        {
            JardinInfo.Identificador_Jardin = Request.Form["Identificador_Jardin"];
            JardinInfo.Nombre_Jardin = Request.Form["Nombre_Jardin"];
            JardinInfo.Direccion = Request.Form["Direccion"];
            JardinInfo.Estado = Request.Form["Estado"];

            // Verifica que ninguno de los campos sea nulo o vacío
            if (string.IsNullOrEmpty(JardinInfo.Identificador_Jardin) ||
                string.IsNullOrEmpty(JardinInfo.Nombre_Jardin) ||
                string.IsNullOrEmpty(JardinInfo.Direccion) ||
                string.IsNullOrEmpty(JardinInfo.Estado))
            {
                ErrorMessage = "Debe llenar todos los campos";
                return Page();
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlUpdate = "UPDATE Registro_Jardin SET Nombre_Jardin = @Nombre_Jardin, " +
                                       "Direccion = @Direccion, Estado = @Estado " +
                                       "WHERE Identificador_Jardin = @Identificador_Jardin";
                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@Identificador_Jardin", JardinInfo.Identificador_Jardin);
                        command.Parameters.AddWithValue("@Nombre_Jardin", JardinInfo.Nombre_Jardin);
                        command.Parameters.AddWithValue("@Direccion", JardinInfo.Direccion);
                        command.Parameters.AddWithValue("@Estado", JardinInfo.Estado);

                        command.ExecuteNonQuery();
                    }
                }

                SuccessMessage = "El jardín fue actualizado correctamente";
                return RedirectToPage("/Jardin/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
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
}

