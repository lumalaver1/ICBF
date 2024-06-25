using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace AppICBF.Pages.Acudiente
{
    public class CrearAcudienteModel : PageModel
    {
        [BindProperty]
        public AcudienteInfo acudienteInfo { get; set; } = new AcudienteInfo();

        public List<AcudienteInfo> listAcudiente { get; set; } = new List<AcudienteInfo>();
        public string errorMessage { get; set; } = "";
        public string successMessage { get; set; } = "";

        public void OnGet()
        {
            CargarAcudiente();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                errorMessage = "Debe llenar todos los campos obligatorios.";
                CargarAcudiente();
                return Page();
            }

            try
            {
                String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlInsert = "INSERT INTO Registro_Acudientes (Cedula, Nombre, Telefono, Celular, Direccion, Correo " +
                                       "VALUES (@Cedula, @Nombre, @Telefono, @Celular, @Direccion,@Correo ";


                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@Cedula", acudienteInfo.Cedula);
                        command.Parameters.AddWithValue("@Nombre", acudienteInfo.Nombre);
                        command.Parameters.AddWithValue("@Telefono", acudienteInfo.Telefono);
                        command.Parameters.AddWithValue("@Celular", acudienteInfo.Celular);
                        command.Parameters.AddWithValue("@Direccion", acudienteInfo.Direccion);
                        command.Parameters.AddWithValue("@Correo", acudienteInfo.Correo);

                        command.ExecuteNonQuery();
                    }
                }

                // Limpiar el modelo después de la inserción exitosa
                acudienteInfo = new AcudienteInfo();
                successMessage = "El acudiente fue agregado correctamente";
            }
            catch (Exception e)
            {
                errorMessage = "Error al intentar agregar el acudiente: " + e.Message;
            }

            CargarAcudiente();
            return Page();
        }

        private void CargarAcudiente()
        {
            try
            {
                String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlSelect = "SELECT Cedula, Nombre, Telefono, Celular, Direccion, Correo FROM Registro_Acudientes";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AcudienteInfo acudienteInfo = new AcudienteInfo
                                {
                                    Cedula = reader.GetInt32(0),
                                    Nombre = reader.GetString(1)
                                };
                                listAcudiente.Add(acudienteInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = "Error al cargar los acudientes: " + e.Message;
            }
        }

        public class AcudienteInfo
        {
            [Required(ErrorMessage = "El campo cedula es obligatorio.")]
            public int Cedula { get; set; }

            [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "El campo Telefono es obligatorio.")]
            public String Telefono { get; set; }
            [Required(ErrorMessage = "El campo Celular es obligatorio.")]

            public string Celular { get; set; }
            [Required(ErrorMessage = "El campo Direccion es obligatorio.")]

            public string Direccion { get; set; }

            [Required(ErrorMessage = "El campo Correo es obligatorio.")]
            public String Correo { get; set; }
        }
    }
}
