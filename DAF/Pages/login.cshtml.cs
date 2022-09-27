using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Net.NetworkInformation;

namespace DAF.Pages
{
    public class loginModel : PageModel
    {
        public bool dataMatch = false;

        public string email = "";
        public string password = "";

        public void OnGet() { }
        public void OnPost()
        {
            //assign
            email = Request.Form["email"];
            password = Request.Form["password"];

            SqlConnection connect = new(@"Data Source=.\sqlexpress;Initial Catalog=DAF;Integrated Security=True");

            if (email.Length > 0 && password.Length > 0)
            {
                try
                {
                    connect.Open();

                    string selectQuery = "SELECT * FROM users";

                    SqlCommand sql = new(selectQuery, connect);

                    SqlDataReader reader = sql.ExecuteReader();


                    while (reader.Read())
                    {
                        if (email.Equals(reader.GetValue(3)) && password.Equals(reader.GetValue(5)))
                        {
                            Response.Redirect("home");
                        }   
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                dataMatch = true;
            }
        }
    }
}
