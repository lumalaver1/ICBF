using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations; 

namespace AppICBF.Pages.Roles
{
    public class EditarRolModel : PageModel
    {
        public RolInfo rolInfo = new RolInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet(int id)
        {
            try
            {
                String connectionString = "Data Source = FERNANDA; Initial Catalog = ICBFweb; Integrated Security = True; Encrypt = False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Roles WHERE Rol_ID = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                rolInfo.Rol_ID = reader.GetInt32(0);
                                rolInfo.Nombre_Rol = reader.GetString(1);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
        }

        public IActionResult OnPost()
        {
            rolInfo.Rol_ID = int.Parse(Request.Form["rolID"]);
            rolInfo.Nombre_Rol = Request.Form["nombreRol"];

            if (string.IsNullOrEmpty(rolInfo.Nombre_Rol))
            {
                errorMessage = "Debe ingresar un nombre para el rol";
                return Page();
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-64KJT59;Initial Catalog=ICBFweb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlUpdate = "UPDATE Roles SET Nombre_Rol = @nombreRol WHERE Rol_ID = @id";
                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@id", rolInfo.Rol_ID);
                        command.Parameters.AddWithValue("@nombreRol", rolInfo.Nombre_Rol);

                        command.ExecuteNonQuery();
                    }
                }

                successMessage = "El rol fue actualizado correctamente";
                return RedirectToPage("/Roles/IndexRoles");
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return Page();
            }
        }

        public class RolInfo
        {
            public int Rol_ID { get; set; }
            public string Nombre_Rol { get; set; }
        }
    }
}
