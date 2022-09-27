using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Numerics;
using System.Xml.Linq;

namespace DAF.Pages
{
    public class donateModel : PageModel
    {
        public bool notEnoughData = false;
        public bool donationSent = false;

        public string name;
        public string email;
        public string category;
        public string customCategory;
        public string date;
        public string amount;
        public string description;
        public void OnGet()
        {
        }

        public void OnPost()
        {
            name = Request.Form["name"];
            email = Request.Form["email"];
            category = Request.Form["category"];
            customCategory = Request.Form["customCategory"];
            date = Request.Form["date"];
            amount = Request.Form["amount"];
            description = Request.Form["description"];

            SqlConnection connect = new(@"Data Source=.\sqlexpress;Initial Catalog=DAF;Integrated Security=True");

            
            if (category.Length > 0 && date.Length > 0 && amount.Length > 0 && description.Length > 0)
            {
                if (category.Equals("OTHER") && customCategory.Length > 0)
                {
                    category = "GOODS";

                    try
                    {
                        //connect to database
                        connect.Open();

                        //database writter
                        string userQuery = "INSERT INTO donations (name, email, category, customCategory, date, amount, description) " +
                            "VALUES('" + name + "','" + email + "','" + category + "','" + customCategory + "','" + date + "'" +
                            ",'" + amount + "','" + description + "')";

                        SqlCommand sql = new(userQuery, connect);

                        //database reader
                        SqlDataReader reader = sql.ExecuteReader();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    //success
                    donationSent = true;
                }
                else if (category.Equals("MONEY"))
                {
                    try
                    {
                        //connect to database
                        connect.Open();

                        //database writter
                        string userQuery = "INSERT INTO donations (name, email, category, date, amount, description) " +
                            "VALUES('" + name + "','" + email + "','" + category + "','" + date + "'" +
                            ",'" + amount + "','" + description + "')";

                        SqlCommand sql = new(userQuery, connect);

                        //database reader
                        SqlDataReader reader = sql.ExecuteReader();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    //success
                    donationSent = true;
                }
                else
                {
                    customCategory = category;
                    category = "GOODS";

                    try
                    {
                        //connect to database
                        connect.Open();

                        //database writter
                        string userQuery = "INSERT INTO donations (name, email, category, customCategory, date, amount, description) " +
                            "VALUES('" + name + "','" + email + "','" + category + "','" + customCategory + "','" + date + "'" +
                            ",'" + amount + "','" + description + "')";

                        SqlCommand sql = new(userQuery, connect);

                        //database reader
                        SqlDataReader reader = sql.ExecuteReader();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    //success
                    donationSent = true;
                }
            }
            else
            {
                notEnoughData = true;
            }
        }
    }
}
