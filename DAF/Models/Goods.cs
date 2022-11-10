using System;
using System.Data.SqlClient;
using System.Xml.Linq;
namespace DAF.Models
{
    public class Goods
    {
        public int DonationGoods { get; set; }
        public int AllocatedGoods { get; set; }
        public int GoodsAfterDeductions { get; set; }
        public int PurchasedGoods { get; set; }
        public int CalculateGoods()
        {
            String conStringg = "Server=tcp:luthandokelengeshe.database.windows.net,1433;Initial Catalog=DAF;Persist Security Info=False;User ID=luthandokelengeshe;Password=Kelenge$he8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connect = new(conStringg);
            connect.Open();

            //getting donations total sum
            string selectQuery = "SELECT SUM(amount) FROM donations WHERE category ='GOODS'";

            SqlCommand cmd = new(selectQuery, connect);

            DonationGoods = (int)cmd.ExecuteScalar();

            //getting allocations total sum
            string query = "SELECT SUM(amount) FROM allocations WHERE category ='GOODS'";

            SqlCommand command = new(query, connect);

            AllocatedGoods = (int)command.ExecuteScalar();

            //getting purchases total
            //string pQuery = "SELECT SUM(quantity) FROM purchases";

            //SqlCommand sql = new(pQuery, connect);

            //PurchasedGoods = (int)sql.ExecuteScalar();

            //deducting
            GoodsAfterDeductions = DonationGoods - AllocatedGoods; //- PurchasedGoods;

            return GoodsAfterDeductions;
        }
    }
}
