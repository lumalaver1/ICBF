using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace AppICBF.Pages.Jardin
{
    public class EliminarJardinModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Identificador_Jardin { get; set; }

        public IActionResult OnGet()
        {
            // Puedes realizar alguna l�gica adicional aqu� si es necesario
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Identificador_Jardin <= 0)
            {
                return NotFound(); // Devolver un error 404 si el id no es v�lido
            }

            try
            {
                String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlDelete = "DELETE FROM Registro_Jardin WHERE Identificador_Jardin = @Identificador_Jardin";
                    using (SqlCommand command = new SqlCommand(sqlDelete, connection))
                    {
                        command.Parameters.AddWithValue("@Identificador_Jardin", Identificador_Jardin);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar el error de alguna manera apropiada, por ejemplo, registrar el error
                Console.WriteLine("Error al eliminar el jard�n: " + ex.Message);
                return RedirectToPage("/Jardin/Index"); // Otra opci�n podr�a ser redirigir a una p�gina de error
            }

            return RedirectToPage("/Jardin/Index");
        }
    }
}
