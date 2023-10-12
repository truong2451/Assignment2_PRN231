using System.ComponentModel.DataAnnotations;

namespace TrinhHuuTruong.eBookStore.WebAPI.ModelViews
{
    public class PublisherVM
    {
        [Required]
        public string PublisherName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
