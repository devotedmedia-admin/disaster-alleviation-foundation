using DAF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DAF.Pages
{
    public class notLoggedHomeModel : PageModel
    {
        public int AvailableFunds { get; set; }
        public int AvailableGoods { get; set; }
        public void OnGet()
        {
            Funds fund = new();
            AvailableFunds = fund.CalculateMoney();

            Goods goods = new();
            AvailableGoods = goods.CalculateGoods();
        }
    }
}
