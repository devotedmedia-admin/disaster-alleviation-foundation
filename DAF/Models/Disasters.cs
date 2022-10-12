using System.Data.SqlClient;
using System.Xml.Linq;

namespace DAF.Models
{
    public class Disasters
    {
        public string ID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string RequiredAidType { get; set; }

        public List<Disasters> GetDonations()
        {
            String connectionString = "Server=tcp:luthandokelengeshe.database.windows.net,1433;Initial Catalog=DAF;Persist Security Info=False;User ID=luthandokelengeshe;Password=Kelenge$he8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connect = new(connectionString);
            connect.Open();

            string selectQuery = "SELECT * FROM disasters";

            SqlCommand cmd = new(selectQuery, connect);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Disasters> disasterList = new();

            while (reader.Read())
            {
                Disasters disasters = new()
                {
                    ID = "" + reader.GetInt32(0),
                    StartDate = "" + reader.GetDateTime(1),
                    EndDate = "" + reader.GetDateTime(2),
                    Location = reader.GetString(3),
                    Description = reader.GetString(4),
                    RequiredAidType = reader.GetString(5)
                };

                disasterList.Add(disasters);
            }
            connect.Close();

            return disasterList;
        }
    }
}
