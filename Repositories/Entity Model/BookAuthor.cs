using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrinhHuuTruong.eBookStore.Repositories.Entity_Model
{
    public class BookAuthor
    {
        [Key, Required]
        public int AuthorId { get; set; }
        [Key, Required]
        public int BookId { get; set; }
        [Required]
        public string AuthorOrder { get; set; }
        [Required]
        public double RoyaltyPercentage { get; set; }

        public virtual Book? Book { get; set; }
        public virtual Author? Author { get; set; }
    }
}
