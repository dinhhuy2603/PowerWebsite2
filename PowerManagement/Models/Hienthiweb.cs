using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PowerManagement.Models
{
    [Table("hienthiweb")]
    public class Hienthiweb
    {
        [Key]
        public string cb1 { get; set; }
        public string cb2 { get; set; }
        public string cb3 { get; set; }
        public string cb4 { get; set; }

        public static implicit operator string(Hienthiweb v)
        {
            throw new NotImplementedException();
        }
    }
}