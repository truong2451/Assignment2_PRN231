using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrinhHuuTruong.eBookStore.Repositories.Entity_Model
{
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int PublisherId { get; set; }
        [Required]
        public double Price { get; set;}
        [Required]
        public string Advance { get; set;}
        [Required]
        public double Royalty { get; set;}
        [Required]
        public int YtdSales { get; set;}
        [Required]
        public string Notes { get; set;}
        [Required]
        public DateTime PublishedDate { get; set;}

        public virtual Publisher? Publisher { get; set;}
        public virtual ICollection<BookAuthor>? BookAuthors { get; set;}

    }
}
