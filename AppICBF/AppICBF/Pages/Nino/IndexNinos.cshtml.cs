using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AppICBF.Pages.Nino
{

    public class IndexNinosModel : PageModel
    {
        public List<NinoInfo> ListaNinos = new List<NinoInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source = FERNANDA; Initial Catalog = ICBFweb; Integrated Security = True; Encrypt = False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlSelect = "SELECT n.Registro_NIUP, n.Nombre, n.Fecha_Nacimiento, n.Tipo_Sangre, n.Ciudad_Nacimiento, " +
                                       "n.Identificacion_Acudiente, n.Telefono, n.Direccion, n.EPS, j.Nombre_Jardin " +
                                       "FROM Datos_Ninos n " +
                                       "INNER JOIN Registro_Jardin j ON n.Identificador_Jardin = j.Identificador_Jardin";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                NinoInfo nino = new NinoInfo
                                {
                                    Registro_NIUP = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Fecha_Nacimiento = reader.GetDateTime(2),
                                    Tipo_Sangre = reader.GetString(3),
                                    Ciudad_Nacimiento = reader.GetString(4),
                                    Identificacion_Acudiente = reader.GetInt32(5),
                                    Telefono = reader.GetString(6),
                                    Direccion = reader.GetString(7),
                                    EPS = reader.GetString(8),
                                    Nombre_Jardin = reader.GetString(9)
                                };
                                ListaNinos.Add(nino);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
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
            public string Nombre_Jardin { get; set; }
        }
    }
}
