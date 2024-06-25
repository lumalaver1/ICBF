using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations; //

namespace AppICBF.Pages.Rol
{
    public class IndexModel : PageModel
    {
        public List<RolInfo> listRoles = new List<RolInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source = FERNANDA; Initial Catalog = ICBFweb; Integrated Security = True; Encrypt = False";
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
                Console.WriteLine("Exception: " + e.ToString());
            }
        }

        public class RolInfo
        {
            public int Rol_ID { get; set; }
            public string Nombre_Rol { get; set; }
        }

        public IActionResult OnGetCrearRol()
        {
            return RedirectToPage("/Roles/CrearRol");
        }

        public IActionResult OnGetEditarRol(int id)
        {
            return RedirectToPage("/Roles/EditarRol", new { id });
        }

        public IActionResult OnGetEliminarRol(int id)
        {
            return RedirectToPage("/Roles/EliminarRol", new { id });
        }
    }
}
