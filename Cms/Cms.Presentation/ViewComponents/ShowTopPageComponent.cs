using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.Services.Repositories;

namespace MyCms.Web.ViewComponents
{
    public class ShowTopPageComponent:ViewComponent
    {
        private IPageRepoitory _pageRepoitory;

        public ShowTopPageComponent(IPageRepoitory pageRepoitory)
        {
            _pageRepoitory = pageRepoitory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("ShowTopPageComponent",
                _pageRepoitory.GetTopPage()));
        }
    }
}
