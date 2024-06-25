using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace AppICBF.Pages.AvanceAcademico
{
    public class CrearAvanceAModel : PageModel
    {
        public AvanceAInfo avanceAInfo { get; set; } = new AvanceAInfo();
        public string errorMessage { get; set; } = "";

        // Ruta de conexión a la base de datos
        String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False";

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            avanceAInfo.Identificacion_Nino = Request.Form["Identificacion_Nino"];
            avanceAInfo.Ano_Escolar = Request.Form["Ano_Escolar"];

            // Verifica que ninguno de los campos sea nulo o vacío
            if (string.IsNullOrEmpty(avanceAInfo.Identificacion_Nino) || string.IsNullOrEmpty(avanceAInfo.Ano_Escolar))
            {
                errorMessage = "Debe llenar todos los campos";
                return Page();
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlInsert = "INSERT INTO AvanceAcademico (Identificacion_Nino, Ano_Escolar) VALUES (@Identificacion_Nino, @Ano_Escolar)";
                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@Identificacion_Nino", avanceAInfo.Identificacion_Nino);
                        command.Parameters.AddWithValue("@Ano_Escolar", avanceAInfo.Ano_Escolar);

                        command.ExecuteNonQuery();
                    }
                }

                return RedirectToPage("/AvanceAcademico/IndexAvanceAcademico");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return Page();
            }
        }
    }

    public class AvanceAInfo
    {
        public string Identificacion_Nino { get; set; }
        public string Ano_Escolar { get; set; }

        public string Nivel { get; set; }

        public string Notas { get; set; }

        public string Descripcion { get; set; }
        public string Ano_Escolar { get; set; }
    }
}
