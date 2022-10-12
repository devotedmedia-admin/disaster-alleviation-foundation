using System.Data.SqlClient;
using System.Xml.Linq;

namespace DAF.Models
{
    public class Donations
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }

        public List<Donations> GetDonations(string category)
        {
            String coString = "Server=tcp:luthandokelengeshe.database.windows.net,1433;Initial Catalog=DAF;Persist Security Info=False;User ID=luthandokelengeshe;Password=Kelenge$he8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connect = new(coString);
            connect.Open();

            string selectQuery = "SELECT name, email, category, date, amount, description FROM donations WHERE category ='" + category + "'";

            SqlCommand cmd = new(selectQuery, connect);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Donations> donationList = new();

            while (reader.Read())
            {
                Donations donations = new()
                {
                    Name = reader["name"].ToString(),
                    Email = reader["email"].ToString(),
                    Category = reader["category"].ToString(),
                    Date = reader["date"].ToString(),
                    Amount = Convert.ToInt32(reader["amount"].ToString()),
                    Description = reader["description"].ToString()
                };

                donationList.Add(donations);
            }
            connect.Close();

            return donationList;
        }
    }
}
