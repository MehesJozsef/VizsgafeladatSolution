using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.MODEL
{
    public class LoginLog
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoginTime { get; set; } = DateTime.Now;
        public DateTime? LogoutTime { get; set; }
        public string? IpAddress { get; set; }
        public string? ClientApp { get; set; }
    }
}
