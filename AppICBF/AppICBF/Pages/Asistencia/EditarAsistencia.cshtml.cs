using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using static AppICBF.Pages.Asistencia.IndexAsistenciaModel;


namespace AppICBF.Pages.Asistencia
{
    public class EditarAsistenciaModel : PageModel
    {
        public AsistenciaInfo AsistenciaInfo { get; set; } = new AsistenciaInfo();
        public string ErrorMessage { get; set; } = "";
        public string SuccessMessage { get; set; } = "";

        // Ruta de conexión a la base de datos
        // String connectionString = "Data Source=DESKTOP-64KJT59;Initial Catalog=ICBFweb;Integrated Security=True"; // Ruta Andres
        String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False"; // Ruta Fernanda

        public void OnGet()
        {
            String identificacionNino = Request.Query["Id"];

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Registro_Asistencia WHERE Identificacion_Nino = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", identificacionNino);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                AsistenciaInfo.Identificacion_Nino = reader.GetInt32(0).ToString();
                                AsistenciaInfo.Fecha = reader.GetString(1);
                                AsistenciaInfo.Estado_Nino = reader.GetString(2);
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
            AsistenciaInfo.Identificacion_Nino = Request.Form["Identificacion_Nino"];
            AsistenciaInfo.Fecha = Request.Form["Fecha"];
            AsistenciaInfo.Estado_Nino = Request.Form["Estado_Nino"];


            // Verifica que ninguno de los campos sea nulo o vacío
            if (string.IsNullOrEmpty(AsistenciaInfo.Identificacion_Nino) ||
                string.IsNullOrEmpty(AsistenciaInfo.Fecha) ||
                string.IsNullOrEmpty(AsistenciaInfo.Estado_Nino))
            {
                ErrorMessage = "Debe llenar todos los campos";
                return Page();
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlUpdate = "UPDATE Registro_Asistencia SET Fecha = @Fecha, " +
                                       "Estado_Nino = @Estado_Nino " +
                                       "WHERE Identificacion_Nino = @Identificacion_Nino";
                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@Identificacion_Nino", AsistenciaInfo.Identificacion_Nino);
                        command.Parameters.AddWithValue("@Fecha", AsistenciaInfo.Fecha);
                        command.Parameters.AddWithValue("@Estado_Nino", AsistenciaInfo.Estado_Nino);
                      

                        command.ExecuteNonQuery();
                    }
                }

                SuccessMessage = "El jardín fue actualizado correctamente";
                return RedirectToPage("/Asistencia/IndexAsistencia");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }
    }

    public class AsistenciaInfo
    {
        public string Identificacion_Nino { get; set; }
        public string Fecha { get; set; }
        public string Estado_Nino { get; set; }
        
    }
}


