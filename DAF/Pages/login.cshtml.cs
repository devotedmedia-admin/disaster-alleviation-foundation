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
        public string errorMessage = "";

        public string email = "";
        public string password = "";

        public void OnGet() { }
        public void OnPost()
        {
            //assign
            email = Request.Form["email"];
            password = Request.Form["password"];

            String connectString = "Server=tcp:luthandokelengeshe.database.windows.net,1433;Initial Catalog=DAF;Persist Security Info=False;User ID=luthandokelengeshe;Password=Kelenge$he8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connect = new(connectString);

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
                            if(reader.GetValue(3).Equals("admin@daf.com") && reader.GetValue(5).Equals("password1"))
                            {
                                Response.Redirect("adminHome");
                            }
                            else
                            {
                                Response.Redirect("home");
                            }
                        }
                        else if (email != reader.GetValue(3).ToString() || password != reader.GetValue(5).ToString())
                        {
                            errorMessage = "User not authorised!";
                        }
                    }
                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                }
            }
            else
            {
                errorMessage = "All fields are required to login!";
                return;
            }
        }
    }
}
