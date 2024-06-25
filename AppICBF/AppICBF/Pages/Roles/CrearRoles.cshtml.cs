using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations; //

namespace AppICBF.Pages.Roles
{
    public class CrearRolModel : PageModel
    {
        public RolInfo rolInfo = new RolInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            rolInfo.Nombre_Rol = Request.Form["nombreRol"];

            if (string.IsNullOrEmpty(rolInfo.Nombre_Rol))
            {
                errorMessage = "Debe llenar el campo Nombre Rol";
                return;
            }

            try
            {
                String connectionString = "Data Source = FERNANDA; Initial Catalog = ICBFweb; Integrated Security = True; Encrypt = False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlInsert = "INSERT INTO Roles (Nombre_Rol) VALUES (@nombreRol)";

                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@nombreRol", rolInfo.Nombre_Rol);

                        command.ExecuteNonQuery();
                    }
                }

                successMessage = "El rol fue agregado correctamente";
                // Limpia el campo después de agregar el rol
                rolInfo.Nombre_Rol = "";

                // Redirige a la página de índice de roles
                Response.Redirect("/Roles/IndexRoles");
            }
            catch (Exception e)
            {
                errorMessage = "Error al intentar agregar el rol: " + e.Message;
            }
        }

        public class RolInfo
        {
            public string Nombre_Rol { get; set; } = "";
        }
    }
}