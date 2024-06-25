using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System;

namespace AppICBF.Pages.MadresComunitarias
{
    public class CrearMadreModel : PageModel

    {
        public MadresComunitariasInfo madresComunitariasInfo = new MadresComunitariasInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            madresComunitariasInfo.Nombres = Request.Form["Nombres"];
            madresComunitariasInfo.Telefono = Request.Form["Telefono"];
            madresComunitariasInfo.Direccion_Residencia = Request.Form["Direccion_Residencia"];
            madresComunitariasInfo.Fecha_Nacimiento = Request.Form["Fecha_Nacimiento"];

            if (string.IsNullOrEmpty(madresComunitariasInfo.Nombres) || string.IsNullOrEmpty(madresComunitariasInfo.Telefono) ||
                string.IsNullOrEmpty(madresComunitariasInfo.Direccion_Residencia) || madresComunitariasInfo.Fecha_Nacimiento == null)
            {
                errorMessage = "Debe llenar todos los campos";
                return;
            }

            try
            {
                String connectionString = "Data Source = FERNANDA; Initial Catalog = ICBFweb; Integrated Security = True; Encrypt = False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlInsert = "INSERT INTO Registro_Madres_Comunitarias (Nombres, Telefono, Direccion_Residencia, Fecha_Nacimiento) " +
                        "VALUES (@Nombres, @Telefono, @Direccion, @Fecha_Nacimiento)";
                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@Nombres", madresComunitariasInfo.Nombres);
                        command.Parameters.AddWithValue("@Telefono", madresComunitariasInfo.Telefono);
                        command.Parameters.AddWithValue("@Direccion_Residencia", madresComunitariasInfo.Direccion_Residencia);
                        command.Parameters.AddWithValue("@Fecha_Nacimiento", madresComunitariasInfo.Fecha_Nacimiento);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }

        }

        private static string ObtenerProximoIdentificacion_Madre_Comunitaria()
        {
            throw new NotImplementedException();
        }
        public class MadresComunitariasInfo
        {
            internal object Identificacion_Madre_Comunitaria;

            public string Nombres { get; set; } = string.Empty;
            public string Telefono { get; set; } = string.Empty;
            public string Direccion_Residencia { get; set; } = string.Empty;
            public string Fecha_Nacimiento { get; set; } = string.Empty;
        }

    }


}

