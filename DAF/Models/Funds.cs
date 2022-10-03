using System;
using System.Data.SqlClient;
using System.Xml.Linq;
namespace DAF.Models
{
    public class Funds
    {
        public int DonationMoney { get; set; }
        public int AllocationsMoney { get; set; }

        public List<Funds> CalculateMoney()
        {
            SqlConnection connect = new(@"Data Source=.\sqlexpress;Initial Catalog=DAF;Integrated Security=True");
            connect.Open();

            //getting donations total sum
            string selectQuery = "SELECT SUM(amount) FROM donations WHERE category ='MONEY'";

            SqlCommand cmd = new(selectQuery, connect);

            List<Funds> fundsList = new();

        
            
                Funds funds = new()
                {
                    DonationMoney = (int)cmd.ExecuteScalar()
                };
                fundsList.Add(funds);
            
            return fundsList;
        }
        
    }
}
