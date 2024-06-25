using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace AppICBF.Pages.Nino
{
    public class EliminarNinoModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Registro_NIUP { get; set; }

        public IActionResult OnGet()
        {
            // Puedes realizar alguna l�gica adicional aqu� si es necesario
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Registro_NIUP <= 0)
            {
                return NotFound(); // Devolver un error 404 si el id no es v�lido
            }

            try
            {
                String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlDelete = "DELETE FROM Datos_Ninos WHERE Registro_NIUP = @Registro_NIUP";
                    using (SqlCommand command = new SqlCommand(sqlDelete, connection))
                    {
                        command.Parameters.AddWithValue("@Registro_NIUP", Registro_NIUP);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar el error de alguna manera apropiada, por ejemplo, registrar el error
                Console.WriteLine("Error al eliminar el ni�o: " + ex.Message);
                return RedirectToPage("/Nino/IndexNinos"); // Otra opci�n podr�a ser redirigir a una p�gina de error
            }

            return RedirectToPage("/Nino/IndexNinos");
        }
    }
}
