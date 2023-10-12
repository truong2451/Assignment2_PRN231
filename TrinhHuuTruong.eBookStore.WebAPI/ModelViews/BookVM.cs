using System.ComponentModel.DataAnnotations;

namespace TrinhHuuTruong.eBookStore.WebAPI.ModelViews
{
    public class BookVM
    {
        [Required]
        public string BookName { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int PublisherId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Advance { get; set; }
        [Required]
        public double Royalty { get; set; }
        [Required]
        public int YtdSales { get; set; }
        [Required]
        public string Notes { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
    }
}
