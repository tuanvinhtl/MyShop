using System;
using System.ComponentModel.DataAnnotations;

namespace TeduShop.Model.Abstract
{
    public class Auditable
    {
        public DateTime? CreatedDate { set; get; }

        [StringLength(256)]
        public string CreatedBy { set; get; }

        public DateTime? UpdateDate { set; get; }

        [StringLength(256)]
        public string UpdateBy { set; get; }

        public string MetaKeywork { set; get; }

        public string Descryption { set; get; }

        public int? DisplayOrder { set; get; }

        [Required]
        public bool Status { set; get; }
    }
}
