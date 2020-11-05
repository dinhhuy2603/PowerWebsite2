using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PowerManagement.Models
{
    public class DBModel : DbContext
    {
        public DBModel() : base("name=PowerEntities")
        {

        }

        public DbSet<Taikhoan> taikhoan { get; set; }
        public DbSet<Hienthiweb> hienthi { get; set; }
        public DbSet<Trangthai> trangthai { get; set; }
    }
}