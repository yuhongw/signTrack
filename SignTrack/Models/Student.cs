using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignTrack.Models
{
    public class Student
    {
        [Key]
        
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        [Display(Name = "姓名")]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(30)]
        public string PhoneId { get; set; }
        [MaxLength(20)]
        [Display(Name="学号")]
        public string StuNo { get; set; }
       
        public virtual ICollection<SignIn> SignIns { get; set; }
    }
}
