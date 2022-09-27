using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Numerics;
using System.Xml.Linq;

namespace DAF.Pages
{
    public class captureModel : PageModel
    {
        public bool notEnoughData = false;
        public bool captureSent = false;
        public string startDate = "";
        public string endDate = "";
        public string location = "";
        public string description = "";
        public string aid = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            //assign
            startDate = Request.Form["start"];
            endDate = Request.Form["end"];
            location = Request.Form["place"];
            description = Request.Form["description"];
            aid = Request.Form["aid"];

            //connection string
            SqlConnection connect = new(@"Data Source =.\sqlexpress; Initial Catalog = DAF; Integrated Security = True");

            if(startDate.Length > 0 && endDate.Length > 0 && location.Length > 0 && description.Length > 0 && aid.Length > 0)
            {
                try
                {
                    connect.Open();

                    //database writter
                    string userQuery = "INSERT INTO captures (startDate, endDate, location, description, requiredAidType) " +
                        "VALUES('" + startDate + "','" + endDate + "','" + location + "','" + description + "','" + aid + "')";
                    
                    SqlCommand sql = new(userQuery, connect);

                    //database reader
                    SqlDataReader reader = sql.ExecuteReader();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                //confirmation
                captureSent = true;

            }
            else
            {
                notEnoughData = true;
            }
        }
    }
}
