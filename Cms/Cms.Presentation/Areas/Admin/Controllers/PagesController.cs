using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.Page;
using MyCms.Services.Repositories;

namespace MyCms.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private IPageRepoitory _pageRepoitory;
        private IPageGroupRepository _pageGroupRepository;

        public PagesController(IPageRepoitory pageRepoitory, IPageGroupRepository pageGroupRepository)
        {
            _pageRepoitory = pageRepoitory;
            _pageGroupRepository = pageGroupRepository;
        }
        

        // GET: Admin/Pages
        public async Task<IActionResult> Index()
        {
            var myCmsDbContext = _pageRepoitory.GetAllPage();
            return View(myCmsDbContext);
        }

        // GET: Admin/Pages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _pageRepoitory.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: Admin/Pages/Create
        public IActionResult Create()
        {
            var list = _pageGroupRepository.GetAllPageGroups();
            ViewData["GroupID"] = new SelectList(list, "Id", "GroupTitle");
            return View();
        }

        // POST: Admin/Pages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroupId,Title,ShortDescription,Content,Visit,ImageName,PageTags,ShowInSlider,CreateDate")] Page page,IFormFile imgup)
        {
            page.Visit = 0;
                page.CreateDate=DateTime.Now;

                if (imgup != null)
                {
                    page.ImageName = Guid.NewGuid().ToString()+Path.GetExtension(imgup.FileName);
                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(),"wwwroot/PageImages",page.ImageName
                    );

                    using (var stream =new FileStream(savePath,FileMode.Create))
                    {
                       await imgup.CopyToAsync(stream);
                    }

                }

                _pageRepoitory.InsertPage(page);
                _pageRepoitory.Save();
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Pages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _pageRepoitory.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }
            ViewData["GroupID"] = new SelectList(_pageGroupRepository.GetAllPageGroups(), "Id", "GroupTitle", page.GroupId);
            return View(page);
        }

        // POST: Admin/Pages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroupId,Title,ShortDescription,Content,Visit,ImageName,PageTags,ShowInSlider,CreateDate")] Page page, IFormFile imgup)
        {
            if (id != page.Id)
            {
                return NotFound();
            }

           
                try
                {
                    if (imgup != null)
                    {

                        if (page.ImageName == null)
                        {
                            page.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgup.FileName);
                        }
                       
                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/PageImages", page.ImageName
                        );

                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                           await imgup.CopyToAsync(stream);
                        }

                    }
                   _pageRepoitory.UpdatePage(page);
                    _pageRepoitory.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Pages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _pageRepoitory.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }

            if (page != null)
            {
                _pageRepoitory.DeletePage(page);

                if (page.ImageName != null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PageImages", page.ImageName);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _pageRepoitory.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(page);
        }

        // POST: Admin/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var page = _pageRepoitory.GetPageById(id);
            _pageRepoitory.DeletePage(page);

            if (page.ImageName != null)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PageImages", page.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _pageRepoitory.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PageExists(int id)
        {
            return _pageRepoitory.PageExists(id);
        }
    }
}
