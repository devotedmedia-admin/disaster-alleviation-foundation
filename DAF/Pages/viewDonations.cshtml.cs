using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DAF.Models;

namespace DAF.Pages
{
    public class viewDonationsModel : PageModel
    {
        public List<Donations> donationsMade = new List<Donations>();
        public bool donationShow = false;
        public string category;
        public void OnGet()
        {
        }
        public void OnPost()
        {
            category = Request.Form["category"];
            Donations donation = new Donations();
            donationsMade = donation.GetDonations(category);
        }
    }
}
