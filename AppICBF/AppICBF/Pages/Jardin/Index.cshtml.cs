using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System;

namespace AppICBF.Pages.Jardin
{

    public class IndexModel : PageModel
    {
        public List<JardinInfo> listJardin = new List<JardinInfo>();

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

                    String sqlSelect = "SELECT * FROM Registro_Jardin";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //Validacion de datos

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

                                    JardinInfo jardinInfo = new JardinInfo();
                                    jardinInfo.Identificador_Jardin = reader.GetInt32(0);
                                    jardinInfo.Nombre_Jardin = reader.GetString(1);
                                    jardinInfo.Direccion = reader.GetString(2);
                                    jardinInfo.Estado = reader.GetString(3);

                                    listJardin.Add(jardinInfo);

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
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex.ToString());

            }

        }

        public class JardinInfo
        {
            public int Identificador_Jardin { get; set; }
            public string Nombre_Jardin { get; set; }
            public string Direccion { get; set; }
            public string Estado { get; set; }
        }
    }

}


