using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace AppICBF.Pages.Roles
{
    public class EliminarRolesModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Usuario_ID { get; set; }

        public IActionResult OnGet()
        {
            // Puedes realizar alguna lógica adicional aquí si es necesario
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Usuario_ID <= 0)
            {
                return NotFound(); // Devolver un error 404 si el id no es válido
            }

            try
            {
                String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlDelete = "DELETE FROM Usuarios WHERE Usuario_ID = @Registro_NIUP";
                    using (SqlCommand command = new SqlCommand(sqlDelete, connection))
                    {
                        command.Parameters.AddWithValue("@Registro_NIUP", Usuario_ID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar el error de alguna manera apropiada, por ejemplo, registrar el error
                Console.WriteLine("Error al eliminar el niño: " + ex.Message);
                return RedirectToPage("/Nino/IndexNinos"); // Otra opción podría ser redirigir a una página de error
            }

            return RedirectToPage("/Nino/IndexNinos");
        }
    }
}
