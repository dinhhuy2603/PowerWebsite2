using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PowerManagement.Models
{
    [Table("taikhoan")]
    public class Taikhoan
    {
        [Key]
        public string user { get; set; }
        public string pass { get; set; }
    }

    public class UserAccLogin
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
        [MinLength(3, ErrorMessage = "Tên đăng nhập ít nhất 3 kí tự.")]
        public string user { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [MinLength(3, ErrorMessage = "Mật khẩu ít nhất 3 kí tự.")]
        [DataType(DataType.Password)]
        public string pass { get; set; }
    }
}