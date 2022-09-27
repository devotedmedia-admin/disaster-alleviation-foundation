using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

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
            SqlConnection connect = new(@"Data Source =.\sqlexpress; Initial Catalog = DAF; Integrated Security = True");

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
