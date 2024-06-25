using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

using static AppICBF.Pages.Usuarios.IndexUsuarioModel;

namespace AppICBF.Pages.Usuarios
{
    public class EditarUsuarioModel : PageModel
    {
        public UsuariosInfo Usuario { get; set; } = new UsuariosInfo();
        public string ErrorMessage { get; set; } = "";
        public string SuccessMessage { get; set; } = "";

        public void OnGet()
        {
            int id = Convert.ToInt32(Request.Query["id"]);

            try
            {
                String connectionString = "Data Source = FERNANDA; Initial Catalog = ICBFweb; Integrated Security = True; Encrypt = False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Usuarios WHERE Usuario_ID = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Usuario.Usuario_ID = reader.GetInt32(0).ToString();
                                Usuario.Nombre_Usuario = reader.GetString(1);
                                Usuario.Correo = reader.GetString(2);
                                // Corregido: Conversion explícita a int
                                Usuario.Rol_ID = reader.GetInt32(3).ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }

        public void OnPost()
        {
            Usuario.Usuario_ID = Request.Form["id"];
            Usuario.Nombre_Usuario = Request.Form["Nombre_Usuario"];
            Usuario.Correo = Request.Form["Correo"];
            // Corregido: Conversion explícita a int
            Usuario.Rol_ID = Request.Form["Rol_ID"];

            if (string.IsNullOrEmpty(Usuario.Nombre_Usuario) || string.IsNullOrEmpty(Usuario.Correo) || string.IsNullOrEmpty(Usuario.Rol_ID))
            {
                ErrorMessage = "Debe llenar todos los campos";
                return;
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-64KJT59;Initial Catalog=ICBFweb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlUpdate = "UPDATE Usuarios SET " +
                                       "Nombre_Usuario = @Nombre_Usuario, " +
                                       "Correo = @Correo, " +
                                       "Rol_ID = @Rol_ID " +
                                       "WHERE Usuario_ID = @id";

                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@id", Usuario.Usuario_ID);
                        command.Parameters.AddWithValue("@Nombre_Usuario", Usuario.Nombre_Usuario);
                        command.Parameters.AddWithValue("@Correo", Usuario.Correo);
                        command.Parameters.AddWithValue("@Rol_ID", Usuario.Rol_ID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return;
            }

            SuccessMessage = "Usuario actualizado correctamente.";
            Response.Redirect("/Usuarios/IndexUsuario");
        }
    }
}
