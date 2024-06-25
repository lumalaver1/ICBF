using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using static AppICBF.Pages.Acudiente.IndexAcudienteModel;


namespace AppICBF.Pages.Acudiente
{
    public class EditarAcudienteModel : PageModel
    {
        public AcudienteInfo AcudienteInfo { get; set; } = new AcudienteInfo();
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
                                AcudienteInfo.Cedula = reader.GetInt32(0).ToString();
                                AcudienteInfo.Nombre = reader.GetString(1);
                                AcudienteInfo.Direccion = reader.GetString(2);
                                AcudienteInfo.Telefono = reader.GetString(3);
                                AcudienteInfo.Celular = reader.GetString(4);
                                AcudienteInfo.Correo = reader.GetString(3);
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
            AcudienteInfo.Cedula = Request.Form["Cedula"];
            AcudienteInfo.Nombre = Request.Form["Nombre"];
            AcudienteInfo.Telefono = Request.Form["Telefono"];
            AcudienteInfo.Celular = Request.Form["Celular"];
            AcudienteInfo.Direccion = Request.Form["Direccion"];


            // Verifica que ninguno de los campos sea nulo o vacío
            if (string.IsNullOrEmpty(AcudienteInfo.Cedula) ||
                string.IsNullOrEmpty(AcudienteInfo.Nombre) ||
                string.IsNullOrEmpty(AcudienteInfo.Telefono) ||
                string.IsNullOrEmpty(AcudienteInfo.Celular) ||
                string.IsNullOrEmpty(AcudienteInfo.Direccion))
            {
                ErrorMessage = "Debe llenar todos los campos";
                return Page();
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlUpdate = "UPDATE Registro_Acudientes SET Nombre = @Nombre, " +
                                       "Telefono = @Telefono, Celular = @Celular, Direccion = @Direccion, Correo= @Correo " +

                                       "WHERE Identificador_Jardin = @Identificador_Jardin";
                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@Cedula", AcudienteInfo.Cedula);
                        command.Parameters.AddWithValue("@Nombre", AcudienteInfo.Nombre);
                        command.Parameters.AddWithValue("@Telefono", AcudienteInfo.Telefono);
                        command.Parameters.AddWithValue("@Celular", AcudienteInfo.Celular);
                        command.Parameters.AddWithValue("@Direccion", AcudienteInfo.Direccion);
                        command.Parameters.AddWithValue("@Correo", AcudienteInfo.Correo);

                        command.ExecuteNonQuery();
                    }
                }

                SuccessMessage = "El jardín fue actualizado correctamente";
                return RedirectToPage("/Acudiente/IndexAcudiente");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }
    }

    public class AcudienteInfo
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }

        public string Direccion { get; set; }
        public string Correo { get; set; }
    }
}

