using System;
using System.Collections.Generic;
using System.Text;
using MyCms.DomainClasses.Page;

namespace MyCms.Services.Repositories
{
    public interface IPageRepoitory
    {
        IEnumerable<Page> GetAllPage();
        IEnumerable<Page> GetTopPage(int take = 4);
        IEnumerable<Page> GetPagesinSlider();
        IEnumerable<Page> GetLatesPage();
        IEnumerable<Page> GetPagesByGroupId(int groupId);
        IEnumerable<Page> Search(string q);
        Page GetPageById(int pageId);
        void InsertPage(Page page);
        void UpdatePage(Page page);
        void DeletePage(Page page);
        void DeletePage(int pageId);
        bool PageExists(int pageId);
        void Save();
    }
}
