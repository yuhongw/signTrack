using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignTrack.Models
{
    public class SignIn
    {
        [Key]
        public int Id { get; set; }
        public DateTime Sign { get; set; }
        public virtual Student Student { get; set; }

    }
}
