using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations; 

namespace AppICBF.Pages.Usuarios
{
    public class IndexUsuarioModel : PageModel
    {
        public List<UsuariosInfo> ListaUsuarios = new List<UsuariosInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source = FERNANDA; Initial Catalog = ICBFweb; Integrated Security = True; Encrypt = False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlSelect = "SELECT u.Usuario_ID, u.Nombre_Usuario, u.Correo, r.Nombre_Rol " +
                                       "FROM Usuarios u " +
                                       "INNER JOIN Roles r ON u.Rol_ID = r.Rol_ID";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UsuariosInfo usuario = new UsuariosInfo
                                {
                                    Usuario_ID = reader.GetInt32(0).ToString(),
                                    Nombre_Usuario = reader.GetString(1),
                                    Correo = reader.GetString(2),
                                    RolNombre = reader.GetString(3)
                                };
                                ListaUsuarios.Add(usuario);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
        }

        public class UsuariosInfo
        {
            public string Usuario_ID { get; set; }
            public string Nombre_Usuario { get; set; }
            public string Correo { get; set; }
            public string RolNombre { get; set; }
            public string Rol_ID { get; internal set; }
        }
    }
}
