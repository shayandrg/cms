using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.Page;
using MyCms.Services.Repositories;

namespace MyCms.Services.Services
{
    public class PageRepoitory:IPageRepoitory
    {
        private readonly MyCmsDbContext _db;

        public PageRepoitory(MyCmsDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Page> GetAllPage()
        {
            return _db.Pages.ToList();
        }

        public IEnumerable<Page> GetTopPage(int take = 4)
        {
            return _db.Pages.OrderByDescending(p => p.Visit).Take(take).ToList();
        }

        public IEnumerable<Page> GetPagesinSlider()
        {
            return _db.Pages.Where(p => p.ShowInSlider).ToList();

        }

        public IEnumerable<Page> GetLatesPage()
        {
            return _db.Pages.OrderByDescending(p => p.CreateDate).Take(4).ToList();
        }

        public IEnumerable<Page> GetPagesByGroupId(int groupId)
        {
            return _db.Pages.Where(p => p.GroupId == groupId).ToList();
        }

        public IEnumerable<Page> Search(string q)
        {
            var list = _db.Pages.Where(p =>
                p.Title.Contains(q) || p.ShortDescription.Contains(q) || p.Content.Contains(q) ||
                p.PageTags.Contains(q)).ToList();

            return list.Distinct().ToList();
        }

        public Page GetPageById(int pageId)
        {
            return _db.Pages.Find(pageId);
        }

        public void InsertPage(Page page)
        {
            _db.Pages.Add(page);
        }

        public void UpdatePage(Page page)
        {
            _db.Entry(page).State = EntityState.Modified;
        }

        public void DeletePage(Page page)
        {
            _db.Entry(page).State = EntityState.Deleted;
        }

        public void DeletePage(int pageId)
        {
            var page = GetPageById(pageId);
            DeletePage(page);
        }

        public bool PageExists(int pageId)
        {
            return _db.Pages.Any(p => p.Id == pageId);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
