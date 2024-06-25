using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations; // Para las anotaciones de validación

namespace AppICBF.Pages.Nino
{
    public class CrearNinoModel : PageModel
    {
        [BindProperty]
        public NinoInfo ninoInfo { get; set; } = new NinoInfo();

        public List<JardinInfo> listJardines { get; set; } = new List<JardinInfo>();
        public string errorMessage { get; set; } = "";
        public string successMessage { get; set; } = "";

        public void OnGet()
        {
            CargarJardines();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                errorMessage = "Debe llenar todos los campos obligatorios.";
                CargarJardines();
                return Page();
            }

            try
            {
                String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlInsert = "INSERT INTO Datos_Ninos (Registro_NIUP, Nombre, Fecha_Nacimiento, Tipo_Sangre, Ciudad_Nacimiento, " +
                                       "Identificacion_Acudiente, Telefono, Direccion, EPS, Identificador_Jardin) " +
                                       "VALUES (@Registro_NIUP, @Nombre, @Fecha_Nacimiento, @Tipo_Sangre, @Ciudad_Nacimiento, " +
                                       "@Identificacion_Acudiente, @Telefono, @Direccion, @EPS, @Identificador_Jardin)";

                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@Registro_NIUP", ninoInfo.Registro_NIUP);
                        command.Parameters.AddWithValue("@Nombre", ninoInfo.Nombre);
                        command.Parameters.AddWithValue("@Fecha_Nacimiento", ninoInfo.Fecha_Nacimiento);
                        command.Parameters.AddWithValue("@Tipo_Sangre", ninoInfo.Tipo_Sangre);
                        command.Parameters.AddWithValue("@Ciudad_Nacimiento", ninoInfo.Ciudad_Nacimiento);
                        command.Parameters.AddWithValue("@Identificacion_Acudiente", ninoInfo.Identificacion_Acudiente);
                        command.Parameters.AddWithValue("@Telefono", ninoInfo.Telefono);
                        command.Parameters.AddWithValue("@Direccion", ninoInfo.Direccion);
                        command.Parameters.AddWithValue("@EPS", ninoInfo.EPS);
                        command.Parameters.AddWithValue("@Identificador_Jardin", ninoInfo.Identificador_Jardin);

                        command.ExecuteNonQuery();
                    }
                }

                // Limpiar el modelo después de la inserción exitosa
                ninoInfo = new NinoInfo();
                successMessage = "El niño fue agregado correctamente";
            }
            catch (Exception e)
            {
                errorMessage = "Error al intentar agregar el niño: " + e.Message;
            }

            CargarJardines();
            return Page();
        }

        private void CargarJardines()
        {
            try
            {
                String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlSelect = "SELECT Identificador_Jardin, Nombre_Jardin FROM Registro_Jardin";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                JardinInfo jardinInfo = new JardinInfo
                                {
                                    Identificador_Jardin = reader.GetInt32(0),
                                    Nombre_Jardin = reader.GetString(1)
                                };
                                listJardines.Add(jardinInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = "Error al cargar los jardines: " + e.Message;
            }
        }

        public class NinoInfo
        {
            [Required(ErrorMessage = "El campo Registro NIUP es obligatorio.")]
            public int Registro_NIUP { get; set; }

            [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "El campo Fecha de Nacimiento es obligatorio.")]
            public DateTime Fecha_Nacimiento { get; set; }

            public string Tipo_Sangre { get; set; }

            public string Ciudad_Nacimiento { get; set; }

            [Required(ErrorMessage = "El campo Identificación Acudiente es obligatorio.")]
            public int Identificacion_Acudiente { get; set; }

            public string Telefono { get; set; }

            public string Direccion { get; set; }

            public string EPS { get; set; }

            [Required(ErrorMessage = "El campo Identificador Jardín es obligatorio.")]
            public int Identificador_Jardin { get; set; }
        }

        public class JardinInfo
        {
            public int Identificador_Jardin { get; set; }
            public string Nombre_Jardin { get; set; }
        }
    }
}
