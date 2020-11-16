using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PowerManagement.Models
{
    [Table("alarm")]
    public class Alarm
    {
        [Key]
        public DateTime thoigian { get; set; }
        public string tencb { get; set; }
        public string giatrithap { get; set; }
        public string giatrihientai { get; set; }
        public string giatricao { get; set; }
        public string canhbao { get; set; }
    }
}