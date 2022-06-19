using System;
using System.Collections.Generic;
using System.Text;
using MyCms.DomainClasses.PageGroup;
using MyCms.ViewModels.Page;

namespace MyCms.Services.Repositories
{
   public interface IPageGroupRepository
   {
       List<PageGroup> GetAllPageGroups();
       PageGroup GetPageGroupById(int groupId);
       void InsertPageGroup(PageGroup pageGroup);
       void UpdatePageGroup(PageGroup pageGroup);
       void DeletePageGroup(PageGroup pageGroup);
       void DeletePageGroup(int groupId);
       bool PageGroupExists(int pageGroupId);
       List<ShowGroupsViewModel> GetListGroups();
       void Save();
   }
}
