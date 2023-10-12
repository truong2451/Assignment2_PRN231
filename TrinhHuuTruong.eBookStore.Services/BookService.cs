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
    public class BookService : IBookService
    {
        private readonly IBookRepository repository;
        private readonly IPublisherRepository publisherRepository;

        public BookService(IBookRepository repository, IPublisherRepository publisherRepository)
        {
            this.repository = repository;
            this.publisherRepository = publisherRepository;
        }

        public Book CheckValidation(Book book)
        {
            if(string.IsNullOrEmpty(book.Title))
            {
                throw new Exception("Title cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(book.Type))
            {
                throw new Exception("Type cannot be empty!!!");
            }
            if (book.PublisherId == null)
            {
                throw new Exception("Publisher Invalid!!!");
            }
            if (book.Price == null || book.Price < 0)
            {
                throw new Exception("Price Invalid!!!");
            }
            if (string.IsNullOrEmpty(book.Advance))
            {
                throw new Exception("Advance cannot be empty!!!");
            }
            if (book.Royalty == null || book.Royalty < 0)
            {
                throw new Exception("Royalty Invalid!!!");
            }
            if (string.IsNullOrEmpty(book.YtdSales.ToString()))
            {
                throw new Exception("YtdSales cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(book.Notes))
            {
                throw new Exception("Note cannot be empty!!!");
            }
            if (book.PublishedDate == null)
            {
                throw new Exception("PublishedDate cannot be empty!!!");
            }

            return book;
        }

        public async Task<bool> Add(Book book)
        {
            try
            {
                var check = CheckValidation(book);
                if(check != null)
                {
                    return await repository.Add(book);
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var book = await repository.GetById(id);
                if(book != null)
                {
                    return await repository.DeleteById(id);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Book> Get(int id)
        {
            try
            {
                var bookDB = await repository.GetById(id);
                if( bookDB != null)
                {
                    bookDB.Publisher = await publisherRepository.GetById(bookDB.PublisherId);
                    bookDB.Publisher.Books.Clear();
                    return bookDB;
                }
                else
                {
                    throw new Exception("Not Found Book");
                }              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Book> GetAll()
        {
            try
            {
                var booksDB = repository.GetAll();
                foreach(var item in booksDB)
                {
                    item.Publisher = publisherRepository.GetById(item.PublisherId).Result;
                    item.Publisher.Books.Clear();
                }
                return booksDB;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(int id, Book book)
        {
            try
            {
                var bookDB = await repository.GetById(id);
                var check = CheckValidation(book);

                if (bookDB != null)
                {
                    if(check != null)
                    {
                        bookDB.BookName = check.BookName;
                        bookDB.Title = book.Title;
                        bookDB.Type = book.Type;
                        bookDB.PublisherId = book.PublisherId;
                        bookDB.Price = book.Price;
                        bookDB.Advance = book.Advance;
                        bookDB.Royalty = book.Royalty;
                        bookDB.YtdSales = book.YtdSales;
                        bookDB.Notes = book.Notes;
                        bookDB.PublishedDate = book.PublishedDate;

                        return await repository.Update(bookDB.BookId, bookDB);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Book> Search(string search)
        {
            try
            {
                List<Book> listBooks = new List<Book>();
                if(double.TryParse(search, out double price))
                {
                    var list = repository.GetAllWithCondition(x => x.Price == price);
                    foreach (var book in list)
                    {
                        listBooks.Add(book);
                    }
                }
                else
                {
                    var list = repository.GetAllWithCondition(x => x.BookName.Contains(search));
                    foreach (var book in list)
                    {
                        listBooks.Add(book);
                    }
                }
                return listBooks;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }       
    }
}
