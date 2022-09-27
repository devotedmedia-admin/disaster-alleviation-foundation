using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DAF.Pages
{
    public class loginRegisterModel : PageModel
    {
        public String errorMessage;
        public String successMessage;

        public bool dataMatch = false;

        //register
        public string name = "";
        public string surname = "";
        public string email = "";
        public string phone = "";
        public string password = "";

        //login
        public string logMail = "";
        public string logPassword = "";

        public void OnGet() { }
        public void OnPostLogin()
        {
            //assign
            //logMail = Request.Form["emailCheck"];
            //logPassword = Request.Form["passwordCheck"];

            //connection string
            SqlConnection connect = new(@"Data Source=.\\sqlexpress;Initial Catalog=DAF;Integrated Security=True");

            if(logMail.Length > 0 && logPassword.Length > 0)
            {
                try
                {
                    //open connection
                    connect.Open();

                    //getting data from the database
                    string selectQuery = "SELECT * FROM users";

                    SqlCommand cmd = new(selectQuery, connect);

                    SqlDataReader reader = cmd.ExecuteReader();

                    //reading through the database
                    while(reader.Read())
                    {
                        if (logMail.Equals(reader.GetValue(3)) && logPassword.Equals(reader.GetValue(5)))
                        {
                            //session here
                            //Session["userID"] = reader.GetValue(0).ToString();
                            //Session["Username"] = reader.GetValue(1).ToString();

                            //Thread
                            //encryptions.Threading();
                            
                            Response.Redirect("home");
                        }
                        else
                        {
                            dataMatch = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
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

            if(name.Length > 0 && surname.Length > 0 && email.Length > 0 && phone.Length > 0 && password.Length > 0)
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
                    Console.WriteLine(e.Message);
                }

                Response.Redirect("home");
            }
        }
    }          
}
