using DAF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;

namespace DAF.Pages
{
    public class allocateModel : PageModel
    {
        public string Category { get; set; }
        public int Amount { get; set; }
        public string Message { get; set; }
        public int AvailableFunds { get; set; }

        public string errorMessage = "";

        public string successMessage = "";

        public Disasters disasters = new();
        public void OnGet()
        {
            String ID = Request.Query["ID"];

            try
            {
                String connString = "Server=tcp:luthandokelengeshe.database.windows.net,1433;Initial Catalog=DAF;Persist Security Info=False;User ID=luthandokelengeshe;Password=Kelenge$he8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                SqlConnection connect = new(connString);
                connect.Open();

                string selectQuery = "SELECT * FROM disasters WHERE id=@ID";

                using (SqlCommand cmd = new(selectQuery, connect))
                {
                    cmd.Parameters.AddWithValue("@ID", ID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            disasters.ID = "" + reader.GetInt32(0);
                            disasters.StartDate = "" + reader.GetDateTime(1);
                            disasters.EndDate = "" + reader.GetDateTime(2);
                            disasters.Location = reader.GetString(3);
                            disasters.Description = reader.GetString(4);
                            disasters.RequiredAidType = reader.GetString(5);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void OnPost()
        {
            //get total donations
            Funds fund = new();
            AvailableFunds = fund.CalculateMoney();

            disasters.StartDate = Request.Form["start"];
            disasters.EndDate = Request.Form["end"];
            disasters.Location = Request.Form["place"];
            disasters.Description = Request.Form["description"];
            disasters.RequiredAidType = Request.Form["aid"];
            Category = Request.Form["category"];
            Amount = Convert.ToInt32(Request.Form["amount"]);
            Message = Request.Form["message"];


            if (disasters.StartDate.Length == 0 || disasters.EndDate.Length == 0
                || disasters.Location.Length == 0 || disasters.Description.Length == 0 ||
                disasters.RequiredAidType.Length == 0 || Category.Length == 0 || Amount == 0)
            {
                errorMessage = "All fields are required!";
                return;
            }

            //connection string
            String connectString = "Server=tcp:luthandokelengeshe.database.windows.net,1433;Initial Catalog=DAF;Persist Security Info=False;User ID=luthandokelengeshe;Password=Kelenge$he8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connect = new(connectString);

            try
            {
                connect.Open();

                if(Category == "MONEY" && Amount > AvailableFunds)
                {
                    errorMessage = "Insufficient funds to allocate to disaster! Check available balance.";
                    return;
                }
                else
                {
                    //database writter
                    string userQuery = "INSERT INTO allocations (startDate, endDate, location, description, requiredAidType, " +
                        "category, amount, message) " +
                        "VALUES('" + disasters.StartDate + "','" + disasters.EndDate + "','" + disasters.Location + "','" +
                        disasters.Description + "','" + disasters.RequiredAidType + "','" + Category + "','" + Amount + "','" +
                        Message + "')";

                    SqlCommand sql = new(userQuery, connect);

                    sql.ExecuteNonQuery();
                    
                }

            }
            catch(Exception e)
            {
                errorMessage = e.Message;
                return;
            }

            //confirmation
            successMessage = "Thank you! We've allocated your donation to this disaster.";
            return;
        }
    }
}
