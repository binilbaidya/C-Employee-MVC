using System;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDEmployeeMVC.Model
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        public string ProfilePic { get; set; }
        [NotMapped]
        public HttpPostedFileBase ProfilePicFile { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public long Phone { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public string EmpDoc { get; set; }
        [NotMapped]
        public HttpPostedFileBase[] EmpDocFile { get; set; }
    }
    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3
    }
}
