using DAF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DAF.Pages
{
    public class homeModel : PageModel
    {
        public int AvailableFunds { get; set; }
        public void OnGet()
        {
            Funds fund = new();
            AvailableFunds = fund.CalculateMoney();
        }
    }
}
