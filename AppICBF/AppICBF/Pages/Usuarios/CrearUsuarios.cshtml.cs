using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AppICBF.Pages.Usuarios
{
    public class CrearUsuarioModel : PageModel
    {
        public UsuarioInfo usuarioInfo = new UsuarioInfo();
        public List<RolInfo> listRoles = new List<RolInfo>();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            CargarRoles();
        }

        public void OnPost()
        {
            usuarioInfo.Nombre_Usuario = Request.Form["nombreUsuario"];
            usuarioInfo.Correo = Request.Form["correo"];

            usuarioInfo.Rol_ID = Convert.ToInt32(Request.Form["rol"]);

            if (string.IsNullOrEmpty(usuarioInfo.Nombre_Usuario) || string.IsNullOrEmpty(usuarioInfo.Correo))
            {
                errorMessage = "Debe llenar todos los campos";
                CargarRoles();
                return;
            }

            try
            {
                String connectionString = "Data Source = FERNANDA; Initial Catalog = ICBFweb; Integrated Security = True; Encrypt = False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlInsert = "INSERT INTO Usuarios (Nombre_Usuario, Correo, Rol_ID) " +
                                       "VALUES (@nombreUsuario, @correo, @rol)";

                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@nombreUsuario", usuarioInfo.Nombre_Usuario);
                        command.Parameters.AddWithValue("@correo", usuarioInfo.Correo);

                        command.Parameters.AddWithValue("@rol", usuarioInfo.Rol_ID);

                        command.ExecuteNonQuery();
                    }
                }

                usuarioInfo.Nombre_Usuario = "";
                usuarioInfo.Correo = "";

                successMessage = "El usuario fue agregado correctamente";
            }
            catch (Exception e)
            {
                errorMessage = "Error al intentar agregar el usuario: " + e.Message;
            }

            CargarRoles();
        }

        private void CargarRoles()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-64KJT59;Initial Catalog=ICBFweb;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlSelect = "SELECT * FROM Roles";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RolInfo rolInfo = new RolInfo
                                {
                                    Rol_ID = reader.GetInt32(0),
                                    Nombre_Rol = reader.GetString(1)
                                };
                                listRoles.Add(rolInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = "Error al cargar los roles: " + e.Message;
            }
        }

        public class UsuarioInfo
        {
            public string Nombre_Usuario { get; set; }
            public string Correo { get; set; }
            public int Rol_ID { get; set; }
        }

        public class RolInfo
        {
            public int Rol_ID { get; set; }
            public string Nombre_Rol { get; set; }
        }
    }
}
