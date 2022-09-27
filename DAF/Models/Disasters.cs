using System.Data.SqlClient;
using System.Xml.Linq;

namespace DAF.Models
{
    public class Disasters
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string RequiredAidType { get; set; }

        public List<Disasters> GetDonations()
        {
            SqlConnection connect = new(@"Data Source=.\sqlexpress;Initial Catalog=DAF;Integrated Security=True");
            connect.Open();

            string selectQuery = "SELECT startDate, endDate, location, description, requiredAidType FROM captures";

            SqlCommand cmd = new(selectQuery, connect);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Disasters> disasterList = new List<Disasters>();

            while (reader.Read())
            {
                Disasters disasters = new Disasters();

                disasters.StartDate = reader["startDate"].ToString();
                disasters.EndDate = reader["endDate"].ToString();
                disasters.Location = reader["location"].ToString();
                disasters.Description = reader["description"].ToString();
                disasters.RequiredAidType = reader["requiredAidType"].ToString();

                disasterList.Add(disasters);
            }
            connect.Close();

            return disasterList;
        }
    }
}
