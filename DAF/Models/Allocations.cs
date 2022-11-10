using System.Data.SqlClient;

namespace DAF.Models
{
    public class Allocations
    {
        public string ID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string RequiredAidType { get; set; }
        public string Category { get; set; }
        public string Amount { get; set; }
        public string Message { get; set; }

        public List<Allocations> GetAllocations()
        {
            String connectionString = "Server=tcp:luthandokelengeshe.database.windows.net,1433;Initial Catalog=DAF;Persist Security Info=False;User ID=luthandokelengeshe;Password=Kelenge$he8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connect = new(connectionString);
            connect.Open();

            string selectQuery = "SELECT * FROM allocations";

            SqlCommand cmd = new(selectQuery, connect);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Allocations> allocationsList = new();

            while (reader.Read())
            {
                Allocations allocations = new()
                {
                    ID = "" + reader.GetInt32(0),
                    StartDate = "" + reader.GetDateTime(1),
                    EndDate = "" + reader.GetDateTime(2),
                    Location = reader.GetString(3),
                    Description = reader.GetString(4),
                    RequiredAidType = reader.GetString(5),
                    Category = reader.GetString(6),
                    Amount = "" + reader.GetInt32(7),
                    Message = reader.GetString(8)
                };

                allocationsList.Add(allocations);
            }
            connect.Close();

            return allocationsList;
        }
    }
}
