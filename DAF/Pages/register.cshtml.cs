using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DAF.Pages
{
    public class registerModel : PageModel
    {
        public string errorMessage = "";

        //register
        public string name = "";
        public string surname = "";
        public string email = "";
        public string phone = "";
        public string password = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            //assign
            name = Request.Form["name"];
            surname = Request.Form["surname"];
            email = Request.Form["email"];
            phone = Request.Form["phone"];
            password = Request.Form["regPassword"];

            //connection string
            String connectionString = "Server=tcp:luthandokelengeshe.database.windows.net,1433;Initial Catalog=DAF;Persist Security Info=False;User ID=luthandokelengeshe;Password=Kelenge$he8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connect = new(connectionString); 

            if (name.Length > 0 && surname.Length > 0 && email.Length > 0 && phone.Length > 0 && password.Length > 0)
            {
                try
                {
                    //connect to database
                    connect.Open();

                    //database writter
                    string userQuery = "INSERT INTO users (name, surname, email, phone, password) " +
                        "VALUES('" + name + "','" + surname + "','" + email + "','" + phone + "','" + password + "')";
                    SqlCommand sql = new(userQuery, connect);

                    //database reader
                    SqlDataReader reader = sql.ExecuteReader();

                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                }

                Response.Redirect("home");
            }
            else
            {
                errorMessage = "All fields are required to register!";
                return;
            }
        }
    }
}
