using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.Services.Repositories;

namespace MyCms.Web.Controllers
{
    public class NewsController : Controller
    {
        private IPageRepoitory pageRepoitory;

        public NewsController(IPageRepoitory pageRepoitory)
        {
            this.pageRepoitory = pageRepoitory;
        }

        

        [Route("News/{newsId}")]
        public IActionResult ShowNews(int newsId)
        {
            var page = pageRepoitory.GetPageById(newsId);

            if (page != null)
            {
                page.Visit += 1;
                pageRepoitory.UpdatePage(page);
                pageRepoitory.Save();
            }

            return View(page);
        }

        [Route("Group/{groupId}/{title}")]
        public IActionResult ShowNewsByGroupId(int groupId,string title)
        {
            ViewData["GroupTitle"] = title;
            return View(pageRepoitory.GetPagesByGroupId(groupId));
        }

        [Route("Search")]
        public IActionResult Search(string q)
        {
            return View(pageRepoitory.Search(q));
        }
    }
}