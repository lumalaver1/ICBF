using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace AppICBF.Pages.AvanceAcademico
{
    public class EliminarAvanceAModel : PageModel
    {
        [BindProperty]
        public string Identificacion_Nino { get; set; }

        public string errorMessage { get; set; } = "";

        // Ruta de conexión a la base de datos
        String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False";

        public void OnGet(string id)
        {
            Identificacion_Nino = id;
        }

        public IActionResult OnPost()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlDelete = "DELETE FROM AvanceAcademico WHERE Identificacion_Nino = @Identificacion_Nino";
                    using (SqlCommand command = new SqlCommand(sqlDelete, connection))
                    {
                        command.Parameters.AddWithValue("@Identificacion_Nino", Identificacion_Nino);
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
}
