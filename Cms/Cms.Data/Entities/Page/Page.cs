using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCms.DomainClasses.Page
{
    public class Page
    {
        public int Id { get; set; }

        [Display(Name = "گروه خبر")]
        public int GroupId { get; set; }

        [Display(Name = "عنوان صفحه")]
        [MaxLength(400)]
        public string Title { get; set; }

        [Display(Name = "توضیح مختصر")]
        public string ShortDescription { get; set; }

        [Display(Name = "متن کامل")]
        public string Content { get; set; }

        [Display(Name = "بازدید")]
        public int Visit { get; set; }

        [Display(Name = "تصویر")]
        public string ImageName { get; set; }

        [Display(Name = "کلمات کلیدی")]
        public string PageTags { get; set; }

        [Display(Name = "نمایش در اسلایدر")]
        public bool ShowInSlider { get; set; }

        [Display(Name = "تاریخ")]
        public DateTime CreateDate { get; set; }=DateTime.UtcNow;


        #region Navigation Property
        
        public virtual PageGroup.PageGroup Group { get; set; }

        #endregion

    }
}
