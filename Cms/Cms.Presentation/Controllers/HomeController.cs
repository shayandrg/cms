using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.Services.Repositories;

namespace MyCms.Web.Controllers
{
    public class HomeController : Controller
    {
        private IPageRepoitory _pageRepoitory;

        public HomeController(IPageRepoitory pageRepoitory)
        {
            _pageRepoitory = pageRepoitory;
        }

        public IActionResult Index()
        {
            ViewData["Slider"] = _pageRepoitory.GetPagesinSlider();
            return View(_pageRepoitory.GetLatesPage());
        }
    }
}