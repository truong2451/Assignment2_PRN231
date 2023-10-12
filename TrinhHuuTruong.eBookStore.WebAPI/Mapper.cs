using AutoMapper;
using TrinhHuuTruong.eBookStore.Repositories.Entity_Model;
using TrinhHuuTruong.eBookStore.WebAPI.ModelViews;

namespace TrinhHuuTruong.eBookStore.WebAPI
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Author, AuthorVM>();
            CreateMap<AuthorVM, Author>();

            CreateMap<Book, BookVM>();
            CreateMap<BookVM, Book>();

            CreateMap<Publisher, PublisherVM>();
            CreateMap<PublisherVM, Publisher>();

            CreateMap<BookAuthor, BookAuthorVM>();
            CreateMap<BookAuthorVM, BookAuthor>();

        }
    }
}
