using System;
using System.Data.SqlClient;
using System.Xml.Linq;
namespace DAF.Models
{
    public class Funds
    {
        public int DonationMoney { get; set; }
        public int AllocatedFunds { get; set; }
        public int TotalDonatedMoney { get; set; }
        public int FundsAfterDeductions { get; set; }
        public int PurchasesMoney { get; set; }
        public int CalculateMoney()
        {
            String conStringg = "Server=tcp:luthandokelengeshe.database.windows.net,1433;Initial Catalog=DAF;Persist Security Info=False;User ID=luthandokelengeshe;Password=Kelenge$he8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connect = new(conStringg);
            connect.Open();

            //getting donations total sum
            string selectQuery = "SELECT SUM(amount) FROM donations WHERE category ='MONEY'";

            SqlCommand cmd = new(selectQuery, connect);

            DonationMoney = (int)cmd.ExecuteScalar();

            //getting allocations total sum
            string query = "SELECT SUM(amount) FROM allocations WHERE category ='MONEY'";

            SqlCommand command = new(query, connect);

            AllocatedFunds = (int)command.ExecuteScalar();

            //getting purchases total
            string pQuery = "SELECT SUM(price) FROM purchases";

            SqlCommand sql = new(pQuery, connect);

            PurchasesMoney = (int)sql.ExecuteScalar();

            //deducting
            FundsAfterDeductions = DonationMoney - AllocatedFunds - PurchasesMoney;

            return FundsAfterDeductions;
        }
    }
}
