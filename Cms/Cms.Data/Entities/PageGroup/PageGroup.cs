using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.DomainClasses.PageGroup
{
    public class PageGroup
    {
        public int Id { get; set; }
        
        [MaxLength(200)]
        public string GroupTitle { get; set; }

        
        #region Navigation Property

        public virtual List<Page.Page> Pages { get; set; }

        #endregion
    }
}
