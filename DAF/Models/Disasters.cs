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
            SqlConnection connect = new(@"Data Source=.\sqlexpress;Initial Catalog=DAF;Integrated Security=True");
            connect.Open();

            string selectQuery = "SELECT * FROM disasters";

            SqlCommand cmd = new(selectQuery, connect);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Disasters> disasterList = new List<Disasters>();

            while (reader.Read())
            {
                Disasters disasters = new Disasters();

                disasters.ID = "" + reader.GetInt32(0);
                disasters.StartDate = "" + reader.GetDateTime(1);
                disasters.EndDate = "" + reader.GetDateTime(2);
                disasters.Location = reader.GetString(3);
                disasters.Description = reader.GetString(4);
                disasters.RequiredAidType = reader.GetString(5);

                disasterList.Add(disasters);
            }
            connect.Close();

            return disasterList;
        }
    }
}
