using TrinhHuuTruong.eBookStore.Repositories.Entity_Model;

namespace TrinhHuuTruong.eBookStore.WebClient.Models
{
    public class BooksVM
    {
        public string Search { get; set; }
        public List<Book> Books { get; set; }
    }

    public class BooksDetailVM
    {
        public Book Book { get; set; }
        public string AuthorId { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
    }
}

