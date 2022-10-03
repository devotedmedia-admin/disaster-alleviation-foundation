using DAF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DAF.Pages
{
    public class notLoggedHomeModel : PageModel
    {
        public int AvailableFunds = 0;
        public List<Funds> receivedFunds = new();
        public void OnGet()
        {
            Funds fund = new();
            receivedFunds = fund.CalculateMoney();
        }
    }
}
