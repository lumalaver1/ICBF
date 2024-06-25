using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System;

namespace AppICBF.Pages.MadresComunitarias
{
    public class EliminarMadreModel : PageModel
    {


        [BindProperty]
        public string Identificacion_Madre_Comunitaria { get; set; }

        public IActionResult OnGet(string Identificacion_Madre_Comunitaria)
        {
            Identificacion_Madre_Comunitaria = Identificacion_Madre_Comunitaria;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Identificacion_Madre_Comunitaria))
            {
                return RedirectToPage("/MadresComunitarias/IndexMadre");
            }

            try
            {
                String connectionString = "Data Source = FERNANDA; Initial Catalog = ICBFweb; Integrated Security = True; Encrypt = False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlDelete = "DELETE FROM Registro_Jardin WHERE  Identificacion_Madre_Comunitaria = @ Identificacion_Madre_Comunitaria";
                    using (SqlCommand command = new SqlCommand(sqlDelete, connection))
                    {
                        command.Parameters.AddWithValue("@ Identificacion_Madre_Comunitaria", Identificacion_Madre_Comunitaria);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the error
                Console.WriteLine("Error: " + ex.Message);
            }

            return RedirectToPage("/MadresComunitarias/IndexMadre");
        }
    }
}
