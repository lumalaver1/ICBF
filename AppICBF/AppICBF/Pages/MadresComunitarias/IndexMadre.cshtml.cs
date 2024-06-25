using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace AppICBF.Pages.MadresComunitarias
{
    public class IndexMadreModel : PageModel
    {
        public List<MadreComunitariaInfo> listMadresComunitarias = new List<MadreComunitariaInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source = FERNANDA; Initial Catalog = ICBFweb; Integrated Security = True; Encrypt = False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlSelect = "SELECT * FROM Registro_Madres_Comunitarias";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    MadreComunitariaInfo madreComunitariaInfo = new MadreComunitariaInfo();
                                    madreComunitariaInfo.Identificacion_Madre_Comunitaria = reader.GetInt32(0).ToString(); ;
                                    madreComunitariaInfo.Nombres = reader.GetString(1).ToString();
                                    madreComunitariaInfo.Telefono = reader.GetString(2).ToString();
                                    madreComunitariaInfo.Direccion_Residencia = reader.GetString(3).ToString();
                                    madreComunitariaInfo.Fecha_Nacimiento = reader.GetDateTime(4).Date.ToShortDateString();

                                    listMadresComunitarias.Add(madreComunitariaInfo);
                                }
                            }
                            else
                            {
                                Console.WriteLine("No hay datos en el resultado");
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

        public class MadreComunitariaInfo
        {
            public string Identificacion_Madre_Comunitaria { get; set; }
            public string Nombres { get; set; }
            public string Telefono { get; set; }
            public string Direccion_Residencia { get; set; }
            public string Fecha_Nacimiento { get; set; }
        }
    }
}
