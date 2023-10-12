using System.Text.Json.Serialization;

namespace TrinhHuuTruong.eBookStore.WebAPI.ModelViews
{
    public class BookAuthorVM
    {

        public int AuthorId { get; set; }

        public int BookId { get; set; }
        [JsonIgnore]
        public string AuthorOther { get; set; }
        [JsonIgnore]
        public double RoyaltyPercentage { get; set; }
    }
}
