using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrinhHuuTruong.eBookStore.Repositories.Entity_Model;
using TrinhHuuTruong.eBookStore.Repositories.Repository.Interface;
using TrinhHuuTruong.eBookStore.Services.Interface;

namespace TrinhHuuTruong.eBookStore.Services
{
    public class BookAuthorService : IBookAuthorService
    {
        private readonly IBookAuthorRepository repository;
        private readonly IAuthorRepository authorRepository;

        public BookAuthorService(IBookAuthorRepository repository, IAuthorRepository authorRepository)
        {
            this.repository = repository;
            this.authorRepository = authorRepository;
        }

        public IEnumerable<BookAuthor> GetAllById(int bookId)
        {
            try
            {
                var list  = repository.GetAllWithCondition(x => x.BookId == bookId);
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<BookAuthor> GetAll()
        {
            try
            {
                return repository.GetAll();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Add(BookAuthor bookAuthor)
        {
            try
            {
                var checkList = repository.GetAllWithCondition(x => x.BookId == bookAuthor.BookId);
                foreach (var item in checkList)
                {
                    if (item.AuthorId == bookAuthor.AuthorId)
                    {
                        throw new Exception("Author was exist");
                    }
                }

                return await repository.Add(bookAuthor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
