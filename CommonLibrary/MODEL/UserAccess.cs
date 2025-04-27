using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.MODEL
{
    public class UserAccess
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PageId { get; set; }
    }
}
