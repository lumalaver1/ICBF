using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace AppICBF.Pages.Asistencia
{
    public class EliminarModel : PageModel
    {
            [BindProperty(SupportsGet = true)]
            public int Identificacion_Nino { get; set; }

            public IActionResult OnGet()
            {
                // Puedes realizar alguna lógica adicional aquí si es necesario
                return Page();
            }

            public IActionResult OnPost()
            {
                if (Identificacion_Nino <= 0)
                {
                    return NotFound(); // Devolver un error 404 si el id no es válido
                }

                try
                {
                    String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sqlDelete = "DELETE FROM Regiistro_Asistencia WHERE Identificacion_Nino = @Identificacion_Nino";
                        using (SqlCommand command = new SqlCommand(sqlDelete, connection))
                        {
                            command.Parameters.AddWithValue("@Identificacion_Nino", Identificacion_Nino);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar el error de alguna manera apropiada, por ejemplo, registrar el error
                    Console.WriteLine("Error al eliminar la asistencia: " + ex.Message);
                    return RedirectToPage("/Asistencia/IndexAsistencia"); // Otra opción podría ser redirigir a una página de error
                }

                return RedirectToPage("/Asistencia/IndexAsistencia");
            }
        }
    }
