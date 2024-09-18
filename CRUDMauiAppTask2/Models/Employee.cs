using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDMauiAppTask2.Models
{
    [Table("employees")]
    public class Employee : BaseEntity
    {
        [MinLength(13), Unique]
        public string?  FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
 