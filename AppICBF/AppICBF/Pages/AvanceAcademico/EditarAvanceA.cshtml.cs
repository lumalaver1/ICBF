using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace AppICBF.Pages.AvanceAcademico
{
    public class EditarAvanceAModel : PageModel
    {
        [BindProperty]
        public AvanceAInfo AvanceAInfo { get; set; } = new AvanceAInfo();
        public string ErrorMessage { get; set; } = "";
        public string SuccessMessage { get; set; } = "";

        // Ruta de conexión a la base de datos
        String connectionString = "Data Source=FERNANDA;Initial Catalog=ICBFweb;Integrated Security=True;Encrypt=False";

        public void OnGet(string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM AvanceAcademico WHERE Identificacion_Nino = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                AvanceAInfo.Identificacion_Nino = reader.GetString(0);
                                AvanceAInfo.Ano_Escolar = reader.GetString(1);
                                AvanceAInfo.Nivel = reader.GetString(2);
                                AvanceAInfo.Notas = reader.GetString(3);
                                AvanceAInfo.Descripcion = reader.GetString(4);
                                AvanceAInfo.Fecha_Entrega_Nota = reader.GetDateTime(5).ToString("yyyy-MM-dd");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(AvanceAInfo.Identificacion_Nino) ||
                string.IsNullOrEmpty(AvanceAInfo.Ano_Escolar) ||
                string.IsNullOrEmpty(AvanceAInfo.Nivel) ||
                string.IsNullOrEmpty(AvanceAInfo.Notas) ||
                string.IsNullOrEmpty(AvanceAInfo.Descripcion) ||
                string.IsNullOrEmpty(AvanceAInfo.Fecha_Entrega_Nota))
            {
                ErrorMessage = "Debe llenar todos los campos";
                return Page();
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlUpdate = "UPDATE AvanceAcademico SET Ano_Escolar = @Ano_Escolar, Nivel = @Nivel, Notas = @Notas, Descripcion = @Descripcion, Fecha_Entrega_Nota = @Fecha_Entrega_Nota WHERE Identificacion_Nino = @Identificacion_Nino";
                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@Identificacion_Nino", AvanceAInfo.Identificacion_Nino);
                        SqlParameter sqlParameter = command.Parameters.AddWithValue("@Ano_Escolar", AvanceAInfo.Ano_Escolar);
                        command.Parameters.AddWithValue("@Nivel", AvanceAInfo.Nivel);
                        command.Parameters.AddWithValue("@Notas", AvanceAInfo.Notas);
                        command.Parameters.AddWithValue("@Descripcion", AvanceAInfo.Descripcion);
                        command.Parameters.AddWithValue("@Fecha_Entrega_Nota", DateTime.Parse(AvanceAInfo.Fecha_Entrega_Nota));

                        command.ExecuteNonQuery();
                    }
                }

                SuccessMessage = "El avance académico fue actualizado correctamente";
                return RedirectToPage("/AvanceAcademico/IndexAvanceAcademico");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
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
        public string Fecha_Entrega_Nota { get; set; }
    }
}
