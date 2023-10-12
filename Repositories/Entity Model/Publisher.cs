using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrinhHuuTruong.eBookStore.Repositories.Entity_Model
{
    public class Publisher
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublisherId { get; set; }
        [Required]
        public string PublisherName { get; set;}
        [Required]
        public string City { get; set;}
        [Required]
        public string State { get; set;}
        [Required]
        public string Country { get; set;}

        public virtual ICollection<User>? Users { get; set;}
        public virtual ICollection<Book>? Books { get; set;}

    }
}
