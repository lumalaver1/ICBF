using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System;
using System.Data.SqlClient;

namespace AppICBF.Pages.AvanceAcademico
{

    public class IndexAvanceAModel : PageModel
    {
        public List<AvanceAInfo> listAvanceA = new List<AvanceAInfo>();

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

                    String sqlSelect = "SELECT * FROM Registro_Avance_Academico";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //Validacion de datos

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

                                    AvanceAInfo avanceAInfo = new AvanceAInfo();
                                    avanceAInfo.Identificacion_Nino = reader.GetInt32(0);
                                    avanceAInfo.Ano_Escolar = reader.GetString(1);
                                    avanceAInfo.Nivel = reader.GetInt32(2);
                                    avanceAInfo.Notas = reader.GetString(3);
                                    avanceAInfo.Descripcion = reader.GetString(4);
                                    avanceAInfo.Fecha_Entrega_Nota = reader.GetString(5);


                                    listAvanceA.Add(avanceAInfo);

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

        public class AvanceAInfo
        {
            public int Identificacion_Nino { get; set; }
            public string Ano_Escolar { get; set; }
            public int Nivel { get; set; }
            public string Notas { get; set; }
            public string Descripcion { get; set; }
            public string Fecha_Entrega_Nota { get; set; }
        }
    }

}

