using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace AppICBF.Pages.Asistencia
{
    public class CrearAsistenciaModel : PageModel
    {

            public AsistenciaInfo asistenciaInfo = new AsistenciaInfo();
            public string errorMessage = "";
            public string successMessage = "";

            public void OnGet()
            {
            }

            public void OnPost()
            {
                asistenciaInfo.Identificacion_Nino = Request.Form["Identificacion_Nino"];
                asistenciaInfo.Fecha = Request.Form["Fecha"];
                asistenciaInfo.Estado_Nino = Request.Form["Estado_Nino"];

                if (asistenciaInfo.Identificacion_Nino.Length == 0 || asistenciaInfo.Fecha.Length == 0 || asistenciaInfo.Estado_Nino.Length == 0)
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

                        //Espacio para validar si la asistencia no existe
                        String sqlInsert = "INSERT INTO Registro_Asistencia (Identificacion_Nino, Fecha, Estado_Nino) " +
                       "VALUES ( @Identificacion_Nino, @Fecha, @Estado_Nino)";



                        using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                        {

                            command.Parameters.AddWithValue("@Identificacion_Nino", asistenciaInfo.Identificacion_Nino);
                            command.Parameters.AddWithValue("@Fecha", asistenciaInfo.Direccion);
                            command.Parameters.AddWithValue("@Estado_Nino", asistenciaInfo.Estado_Nino);

                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception e)
                {
                    errorMessage += e.Message;
                    return;
                }

                asistenciaInfo.Identificacion_Nino = ""; asistenciaInfo.Fecha = ""; asistenciaInfo.Estado_Nino = "";
                successMessage = "El jardin fue agregado correctamente";
                Response.Redirect("/Asistencia/IndexAsistencia");
            }

            private string ObtenerProximoIdentificacion_Nino()
            {
                throw new NotImplementedException();
            }

            public class AsistenciaInfo
            {
                
                public string Identificacion_Nino { get; set; } = string.Empty;
                public string Direccion { get; set; } = string.Empty;
                public string Estado_Nino { get; set; } = string.Empty;
                public string Fecha { get; set; } = string.Empty;
            }
        }
    }




