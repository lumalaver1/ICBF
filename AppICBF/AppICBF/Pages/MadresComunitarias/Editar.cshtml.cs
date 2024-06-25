using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System;

using static AppICBF.Pages.MadresComunitarias.IndexMadreModel;

namespace AppICBF.Pages.MadresComunitarias
{
    public class EditarMadreComunitariaModel : PageModel
    {
        public MadreComunitariaInfo madreComunitariaInfo = new MadreComunitariaInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            int id = Convert.ToInt32(Request.Query["id"]);

            try
            {
                String connectionString = "Data Source = FERNANDA; Initial Catalog = ICBFweb; Integrated Security = True; Encrypt = False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Registro_Madres_Comunitarias WHERE Identificacion_Madre_Comunitaria = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                madreComunitariaInfo.Identificacion_Madre_Comunitaria = reader.GetInt32(0).ToString();
                                madreComunitariaInfo.Nombres = reader.GetString(1);
                                madreComunitariaInfo.Telefono = reader.GetString(2);
                                madreComunitariaInfo.Direccion_Residencia = reader.GetString(3);
                                madreComunitariaInfo.Fecha_Nacimiento = reader.GetDateTime(4).ToString("yyyy-MM-dd");
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
            madreComunitariaInfo.Identificacion_Madre_Comunitaria = Request.Form["id"];
            madreComunitariaInfo.Nombres = Request.Form["Nombres"];
            madreComunitariaInfo.Telefono = Request.Form["Telefono"];
            madreComunitariaInfo.Direccion_Residencia = Request.Form["Direccion_Residencia"];
            madreComunitariaInfo.Fecha_Nacimiento = Request.Form["Fecha_Nacimiento"];

            if (string.IsNullOrEmpty(madreComunitariaInfo.Nombres) || string.IsNullOrEmpty(madreComunitariaInfo.Telefono) ||
                string.IsNullOrEmpty(madreComunitariaInfo.Direccion_Residencia) || string.IsNullOrEmpty(madreComunitariaInfo.Identificacion_Madre_Comunitaria) || string.IsNullOrEmpty(madreComunitariaInfo.Fecha_Nacimiento))
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
                    string sqlUpdate = "UPDATE Registro_Madres_Comunitarias SET " +
                                       "Nombres = @Nombres, " +
                                       "Telefono = @Telefono, " +
                                       "Direccion_Residencia = @Direccion_Residencia, " +
                                       "Fecha_Nacimiento = @Fecha_Nacimiento " +
                                       "WHERE Identificacion_Madre_Comunitaria = @id";

                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@id", madreComunitariaInfo.Identificacion_Madre_Comunitaria);
                        command.Parameters.AddWithValue("@Nombres", madreComunitariaInfo.Nombres);
                        command.Parameters.AddWithValue("@Telefono", madreComunitariaInfo.Telefono);
                        command.Parameters.AddWithValue("@Direccion_Residencia", madreComunitariaInfo.Direccion_Residencia);
                        command.Parameters.AddWithValue("@Fecha_Nacimiento", madreComunitariaInfo.Fecha_Nacimiento);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }

            successMessage = "La madre comunitaria fue actualizada correctamente";
            Response.Redirect("/MadresComunitarias/IndexMadre");
        }


    }
}
