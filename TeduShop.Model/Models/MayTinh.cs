using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduShop.Model.Models
{
    [Table("BangMayTinh")]
    public class MayTinh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        public string Name { set; get; }
        public bool? CategoryPC { set; get; }
        public string Desciption { set; get; }
        public DateTime? CreatedDate { set; get; }
        public bool Status { set; get; }
        public string CreatedBy { set; get; }
        public virtual IEnumerable<ChiTietSuaChua> BangChiTietSuaChua { set; get; }
    }
}
