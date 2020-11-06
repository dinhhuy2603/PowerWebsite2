using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PowerManagement.Models
{
    [Table("trangthai")]
    public class Trangthai
    {
        [Key]
        public string status_cb1 { get; set; }
        public string status_cb2 { get; set; }
        public string status_cb3 { get; set; }
        public string status_cb4 { get; set; }
        public string status_modbus { get; set; }
        public string status_ketnoiDB { get; set; }

        public static implicit operator string(Trangthai v)
        {
            throw new NotImplementedException();
        }
    }
}