using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;


namespace AppICBF.Pages.Nino
{
    public class EditarNinoModel : PageModel
    {
        public NinoInfo Nino { get; set; } = new NinoInfo();
        public string ErrorMessage { get; set; } = "";
        public string SuccessMessage { get; set; } = "";

        public void OnGet()
        {
            int registroNIUP = Convert.ToInt32(Request.Query["Registro_NIUP"]);

            try
            {
                String connectionString = "Data Source = FERNANDA; Initial Catalog = ICBFweb; Integrated Security = True; Encrypt = False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Datos_Ninos WHERE Registro_NIUP = @Registro_NIUP";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Registro_NIUP", registroNIUP);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Nino.Registro_NIUP = reader.GetInt32(0);
                                Nino.Nombre = reader.GetString(1);
                                Nino.Fecha_Nacimiento = reader.GetDateTime(2);
                                Nino.Tipo_Sangre = reader.GetString(3);
                                Nino.Ciudad_Nacimiento = reader.GetString(4);
                                Nino.Identificacion_Acudiente = reader.GetInt32(5);
                                Nino.Telefono = reader.GetString(6);
                                Nino.Direccion = reader.GetString(7);
                                Nino.EPS = reader.GetString(8);
                                Nino.Identificador_Jardin = reader.GetInt32(9);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }

        public IActionResult OnPost()
        {
            Nino.Registro_NIUP = Convert.ToInt32(Request.Form["Registro_NIUP"]);
            Nino.Nombre = Request.Form["Nombre"];
            Nino.Fecha_Nacimiento = Convert.ToDateTime(Request.Form["Fecha_Nacimiento"]);
            Nino.Tipo_Sangre = Request.Form["Tipo_Sangre"];
            Nino.Ciudad_Nacimiento = Request.Form["Ciudad_Nacimiento"];
            Nino.Identificacion_Acudiente = Convert.ToInt32(Request.Form["Identificacion_Acudiente"]);
            Nino.Telefono = Request.Form["Telefono"];
            Nino.Direccion = Request.Form["Direccion"];
            Nino.EPS = Request.Form["EPS"];
            Nino.Identificador_Jardin = Convert.ToInt32(Request.Form["Identificador_Jardin"]);

            if (string.IsNullOrEmpty(Nino.Nombre) || Nino.Fecha_Nacimiento == DateTime.MinValue || string.IsNullOrEmpty(Nino.Tipo_Sangre) || string.IsNullOrEmpty(Nino.Ciudad_Nacimiento) || Nino.Identificacion_Acudiente == 0 || string.IsNullOrEmpty(Nino.Telefono) || string.IsNullOrEmpty(Nino.Direccion) || string.IsNullOrEmpty(Nino.EPS) || Nino.Identificador_Jardin == 0)
            {
                ErrorMessage = "Debe llenar todos los campos";
                return Page();
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-64KJT59;Initial Catalog=ICBFweb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlUpdate = "UPDATE Datos_Ninos SET " +
                                       "Nombre = @Nombre, " +
                                       "Fecha_Nacimiento = @Fecha_Nacimiento, " +
                                       "Tipo_Sangre = @Tipo_Sangre, " +
                                       "Ciudad_Nacimiento = @Ciudad_Nacimiento, " +
                                       "Identificacion_Acudiente = @Identificacion_Acudiente, " +
                                       "Telefono = @Telefono, " +
                                       "Direccion = @Direccion, " +
                                       "EPS = @EPS, " +
                                       "Identificador_Jardin = @Identificador_Jardin " +
                                       "WHERE Registro_NIUP = @Registro_NIUP";

                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@Registro_NIUP", Nino.Registro_NIUP);
                        command.Parameters.AddWithValue("@Nombre", Nino.Nombre);
                        command.Parameters.AddWithValue("@Fecha_Nacimiento", Nino.Fecha_Nacimiento);
                        command.Parameters.AddWithValue("@Tipo_Sangre", Nino.Tipo_Sangre);
                        command.Parameters.AddWithValue("@Ciudad_Nacimiento", Nino.Ciudad_Nacimiento);
                        command.Parameters.AddWithValue("@Identificacion_Acudiente", Nino.Identificacion_Acudiente);
                        command.Parameters.AddWithValue("@Telefono", Nino.Telefono);
                        command.Parameters.AddWithValue("@Direccion", Nino.Direccion);
                        command.Parameters.AddWithValue("@EPS", Nino.EPS);
                        command.Parameters.AddWithValue("@Identificador_Jardin", Nino.Identificador_Jardin);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return Page();
            }

            SuccessMessage = "Niño actualizado correctamente.";
            return RedirectToPage("/Nino/IndexNinos");
        }

        public class NinoInfo
        {
            public int Registro_NIUP { get; set; }
            public string Nombre { get; set; }
            public DateTime Fecha_Nacimiento { get; set; }
            public string Tipo_Sangre { get; set; }
            public string Ciudad_Nacimiento { get; set; }
            public int Identificacion_Acudiente { get; set; }
            public string Telefono { get; set; }
            public string Direccion { get; set; }
            public string EPS { get; set; }
            public int Identificador_Jardin { get; set; }
        }
    }
}
