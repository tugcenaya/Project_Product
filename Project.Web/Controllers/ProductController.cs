using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Project.Web.Models;
using Project.Web.Hubs;

namespace Project.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHubContext<SignalServer> _hubContext;
        public ProductController(IHubContext<SignalServer> hubContext)
    {
        _hubContext = hubContext;
    }

        public IActionResult Index()
        {
            
            return View();
        }


    [HttpPost]
        public async Task<IActionResult> Save(Product product)
        {
            await _hubContext.Clients.All.SendAsync("ItemAdded", product);
            return View();

        }

        public IActionResult Update()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
