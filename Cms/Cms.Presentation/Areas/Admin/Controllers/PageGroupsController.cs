using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.PageGroup;
using MyCms.Services.Repositories;

namespace MyCms.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PageGroupsController : Controller
    {
        private IPageGroupRepository _pageGroupRepository;

        public PageGroupsController(IPageGroupRepository pageGroupRepository)
        {
            _pageGroupRepository = pageGroupRepository;
        }

        // GET: Admin/PageGroups
        public async Task<IActionResult> Index()
        {
            return View(_pageGroupRepository.GetAllPageGroups());
        }

        // GET: Admin/PageGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageGroup = _pageGroupRepository.GetPageGroupById(id.Value);
            if (pageGroup == null)
            {
                return NotFound();
            }

            return View(pageGroup);
        }

        // GET: Admin/PageGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PageGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroupTitle")] PageGroup pageGroup)
        { 
            _pageGroupRepository.InsertPageGroup(pageGroup);
            _pageGroupRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/PageGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageGroup = _pageGroupRepository.GetPageGroupById(id.Value);
            if (pageGroup == null)
            {
                return NotFound();
            }
            return View(pageGroup);
        }

        // POST: Admin/PageGroups/Edit/5
        [HttpPost]
       
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroupTitle")] PageGroup pageGroup)
        {
            if (id != pageGroup.Id)
            {
                return NotFound();
            }

            try
            {
                _pageGroupRepository.UpdatePageGroup(pageGroup);
                _pageGroupRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageGroupExists(pageGroup.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/PageGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageGroup = _pageGroupRepository.GetPageGroupById(id.Value);
            if (pageGroup == null)
            {
                return NotFound();
            }

            return View(pageGroup);
        }

        // POST: Admin/PageGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          _pageGroupRepository.DeletePageGroup(id);
            _pageGroupRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PageGroupExists(int id)
        {
            return _pageGroupRepository.PageGroupExists(id);
        }
    }
}
