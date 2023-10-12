using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrinhHuuTruong.eBookStore.Repositories.Entity_Model
{
    public class Author
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }
        [Required] 
        public string LastName { get; set;}
        [Required]
        public string FirstName { get; set;}
        [Required]
        public string Phone { get; set;}
        [Required]
        public string Address { get; set;}
        [Required]
        public string City { get; set;}
        [Required]
        public string State { get; set;}
        [Required]  
        public string Zip { get; set;}
        [Required]
        public string EmailAddress { get; set;}

        //[JsonIgnore]
        public virtual ICollection<BookAuthor>? BookAuthors { get; set; }
    }
}
