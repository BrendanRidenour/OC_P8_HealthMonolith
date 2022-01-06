using CalifornianHealth.Data;
using CalifornianHealth.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CalifornianHealth.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index([FromServices] IFetchConsultantsOperation operation)
        {
            var consultants = await operation.FetchConsultants();

            return View(model: consultants);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(viewName: "Error",
                model: new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult StatusCodePage(int statusCode)
        {
            if (statusCode == 404)
                return View("NotFound");

            return Error();
        }
    }
}