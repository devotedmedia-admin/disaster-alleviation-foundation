using DAF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DAF.Pages
{
    public class purchaseModel : PageModel
    {
        public string Category { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int AvailableFunds { get; set; }

        public string errorMessage = "";

        public string successMessage = "";

        public Disasters disasters = new();
        public void OnGet()
        {
            String ID = Request.Query["ID"];

            try
            {
                String stringCon = "Server=tcp:luthandokelengeshe.database.windows.net,1433;Initial Catalog=DAF;Persist Security Info=False;User ID=luthandokelengeshe;Password=Kelenge$he8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                SqlConnection connect = new(stringCon);
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
            Item = Request.Form["item"];
            Quantity = Convert.ToInt32(Request.Form["quantity"]);
            Price = Convert.ToInt32(Request.Form["price"]);

            if (disasters.StartDate.Length == 0 || disasters.EndDate.Length == 0
                || disasters.Location.Length == 0 || disasters.Description.Length == 0 ||
                disasters.RequiredAidType.Length == 0 || Category.Length == 0 || Item.Length == 0 ||
                Quantity == 0 || Price == 0)
            {
                errorMessage = "All fields are required!";
                return;
            }

            //connection string
            String connn = "Server=tcp:luthandokelengeshe.database.windows.net,1433;Initial Catalog=DAF;Persist Security Info=False;User ID=luthandokelengeshe;Password=Kelenge$he8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connect = new(connn);

            try
            {
                //connect to database
                connect.Open();

                if (Price > AvailableFunds)
                {
                    errorMessage = "Insufficient funds to purchase goods for this disaster! Check available balance.";
                    return;
                }
                else
                {
                    //database writter
                    string userQuery = "INSERT INTO purchases (startDate, endDate, location, description, requiredAidType, " +
                        "category, item, quantity, price) " +
                        "VALUES('" + disasters.StartDate + "','" + disasters.EndDate + "','" + disasters.Location + "','" +
                        disasters.Description + "','" + disasters.RequiredAidType + "','" + Category + "','" + Item + "','" +
                        Quantity + "','" + Price + "')";

                    SqlCommand sql = new(userQuery, connect);

                    sql.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }

            //confirmation
            successMessage = "Goods successfully purchased for this disaster!";
            return;
        }
    }
}
