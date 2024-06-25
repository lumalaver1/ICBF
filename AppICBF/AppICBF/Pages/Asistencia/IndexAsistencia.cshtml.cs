
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System;
using System.Data.SqlClient;

namespace AppICBF.Pages.Asistencia
{
    public class IndexAsistenciaModel : PageModel {

        public List<AsistenciaInfo> listAsistencia = new List<AsistenciaInfo>();

    public void OnGet()
    {
            try
            {
                //Ruta Andres
                //String connectionString = "Data Source=DESKTOP-64KJT59;Initial Catalog=ICBFweb;Integrated Security = True";
                //Ruta Fernanda
                String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sqlSelect = "SELECT * FROM Registro_Asistencia";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //Validacion de datos

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    {
                                        while (reader.Read())
                                        {

                                            AsistenciaInfo asistenciasInfo = new AsistenciaInfo();
                                            asistenciasInfo.Identificacion_Nino = reader.GetInt32(0);
                                            asistenciasInfo.Fecha = reader.GetString(1);
                                            asistenciasInfo.Estado_Nino = reader.GetString(2);

                                            listAsistencia.Add(asistenciasInfo);





                                            {
                                                Console.WriteLine("No hay datos en el resultado");
                                            }
                                        }


                                    }

                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex.ToString());

            }

        }

        public class AsistenciaInfo
{
    public int Identificacion_Nino { get; set; }
    public string Fecha { get; set; }
    public string Estado_Nino { get; set; }
    }

}
    }
