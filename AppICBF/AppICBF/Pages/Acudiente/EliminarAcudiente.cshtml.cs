using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace AppICBF.Pages.Acudiente
{
    public class EliminarAcudienteModel : PageModel
    {

            [BindProperty(SupportsGet = true)]
            public int Cedula { get; set; }

            public IActionResult OnGet()
            {
                // Puedes realizar alguna lógica adicional aquí si es necesario
                return Page();
            }

            public IActionResult OnPost()
            {
                if (Cedula <= 0)
                {
                    return NotFound(); // Devolver un error 404 si el id no es válido
                }

                try
                {
                    String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sqlDelete = "DELETE FROM Registro_Acudientes WHERE Cedula = @Cedula";
                        using (SqlCommand command = new SqlCommand(sqlDelete, connection))
                        {
                            command.Parameters.AddWithValue("@Cedula", Cedula);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar el error de alguna manera apropiada, por ejemplo, registrar el error
                    Console.WriteLine("Error al eliminar el acudiente: " + ex.Message);
                    return RedirectToPage("/Acudiente/IndexAcudiente"); // Otra opción podría ser redirigir a una página de error
                }

                return RedirectToPage("/Acudiente/IndexAcudiente");
            }
        }
    }
