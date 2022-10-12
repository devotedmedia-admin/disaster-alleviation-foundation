using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Xml.Linq;
using System;

namespace DAF.Pages
{
    public class contactModel : PageModel
    {
        public bool hasData = false;
        public string contactName = "";
        public string contactEmail = "";
        public string contactSubject = "";
        public string contactMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            contactName = Request.Form["contactName"];
            contactEmail = Request.Form["contactEmail"];
            contactSubject = Request.Form["contactSubject"];
            contactMessage = Request.Form["contactMessage"];

            String conString = "Server=tcp:luthandokelengeshe.database.windows.net,1433;Initial Catalog=DAF;Persist Security Info=False;User ID=luthandokelengeshe;Password=Kelenge$he8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connect = new(conString);

            if (contactName.Length > 0 && contactName.Length > 0 && contactSubject.Length > 0 && contactMessage.Length > 0)
            {
                try
                {
                    //connect to database
                    connect.Open();

                    //database writter
                    string userQuery = "INSERT INTO contacts (name, email, subject, message)" +
                        "VALUES('" + contactName + "','" + contactEmail + "','" + contactSubject + "','" + contactMessage + "')";

                    SqlCommand sql = new(userQuery, connect);

                    //database reader
                    SqlDataReader reader = sql.ExecuteReader();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                hasData = true;
            } 
        }
    }
}
