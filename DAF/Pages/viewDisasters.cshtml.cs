using DAF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DAF.Pages
{
    public class viewDisastersModel : PageModel
    {
        
        public List<Disasters> disastersReceived = new List<Disasters>();
        public bool donationShow = false;
        public void OnGet()
        {
            Disasters disaster = new();
            disastersReceived = disaster.GetDonations();
        }

    }
}
