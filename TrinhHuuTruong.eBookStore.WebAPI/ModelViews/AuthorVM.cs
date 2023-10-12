using System.ComponentModel.DataAnnotations;

namespace TrinhHuuTruong.eBookStore.WebAPI.ModelViews
{
    public class AuthorVM
    {
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public string EmailAddress { get; set; }
    }
}
