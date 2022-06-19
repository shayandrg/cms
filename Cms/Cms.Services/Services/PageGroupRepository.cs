using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.PageGroup;
using MyCms.Services.Repositories;
using MyCms.ViewModels.Page;

namespace MyCms.Services.Services
{
    public class PageGroupRepository : IPageGroupRepository
    {
        private readonly MyCmsDbContext _db;

        public PageGroupRepository(MyCmsDbContext db)
        {
            _db = db;
        }


        public List<PageGroup> GetAllPageGroups()
        {
            return _db.PageGroups.ToList();
        }

        public PageGroup GetPageGroupById(int groupId)
        {
            return _db.PageGroups.Find(groupId);
        }

        public void InsertPageGroup(PageGroup pageGroup)
        {
            _db.PageGroups.Add(pageGroup);
        }

        public void UpdatePageGroup(PageGroup pageGroup)
        {
            _db.Entry(pageGroup).State = EntityState.Modified;
        }

        public void DeletePageGroup(PageGroup pageGroup)
        {
            _db.Entry(pageGroup).State = EntityState.Deleted;
        }

        public void DeletePageGroup(int groupId)
        {
            var group = GetPageGroupById(groupId);
            DeletePageGroup(group);
        }

        public bool PageGroupExists(int pageGroupId)
        {
            return _db.PageGroups.Any(p => p.Id == pageGroupId);
        }

        public List<ShowGroupsViewModel> GetListGroups()
        {
            return _db.PageGroups.Select(g => new ShowGroupsViewModel()
            {
                GroupID = g.Id,
                GroupTitle = g.GroupTitle,
                PageCount = g.Pages.Count
            }).ToList();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
