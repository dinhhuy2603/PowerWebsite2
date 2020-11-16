using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PowerManagement.Models
{
    [Table("xuatbaocao")]
    public class Report
    {
        [Key]
        public int id { get; set; }
        public DateTime thoigian { get; set; }
        public string tencb { get; set; }
        public string giatri { get; set; }
    }
}