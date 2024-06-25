using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using static AppICBF.Pages.Acudiente.IndexAcudienteModel;


namespace AppICBF.Pages.Acudiente
{
    public class IndexAcudienteModel : PageModel
    {
        public List<AcudienteInfo> listAcudiente = new List<AcudienteInfo>();

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

                    String sqlSelect = "SELECT * FROM Registro_Acudiente";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //Validacion de datos

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

                                    AcudienteInfo acudienteInfo = new AcudienteInfo();
                                    acudienteInfo.Cedula = reader.GetInt32(0);
                                    acudienteInfo.Nombre = reader.GetString(1);
                                    acudienteInfo.Telefono = reader.GetString(2);
                                    acudienteInfo.Celular = reader.GetString(3);
                                    acudienteInfo.Cedula = reader.GetInt32(4);
                         

                                    listAcudiente.Add(acudienteInfo);

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

        public class AcudienteInfo
        {
            public int Cedula { get; set; }
            public string Nombre { get; set; }
            public string Telefono { get; set; }
            public string Celular { get; set; }
            public string Direccion { get; set; }
            public string Correo { get; set; }
        }
    }

}


