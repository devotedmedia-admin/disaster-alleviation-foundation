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
        public string Amount { get; set; }
        public string Description { get; set; }

        public List<Donations> GetDonations(string category)
        {
            SqlConnection connect = new(@"Data Source=.\sqlexpress;Initial Catalog=DAF;Integrated Security=True");
            connect.Open();

            string selectQuery = "SELECT name, email, category, date, amount, description FROM donations WHERE category ='" + category + "'";

            SqlCommand cmd = new(selectQuery, connect);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Donations> donationList = new List<Donations>();

            while (reader.Read())
            {
                Donations donations = new Donations();

                donations.Name = reader["name"].ToString();
                donations.Email = reader["email"].ToString();
                donations.Category = reader["category"].ToString();
                donations.Date = reader["date"].ToString();
                donations.Amount = reader["amount"].ToString();
                donations.Description = reader["description"].ToString();

                donationList.Add(donations);
            }
            connect.Close();

            return donationList;
        }
    }
}
