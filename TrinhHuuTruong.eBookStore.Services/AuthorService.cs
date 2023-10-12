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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository repository;

        public AuthorService(IAuthorRepository repository)
        {
            this.repository = repository;
        }

        public Author CheckValidation(Author author)
        {
            if (string.IsNullOrEmpty(author.LastName))
            {
                throw new Exception("Last Name cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(author.FirstName))
            {
                throw new Exception("First Name cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(author.Phone))
            {
                throw new Exception("Phone cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(author.Address))
            {
                throw new Exception("Address cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(author.City))
            {
                throw new Exception("City cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(author.State))
            {
                throw new Exception("State cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(author.Zip))
            {
                throw new Exception("Zip cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(author.EmailAddress))
            {
                throw new Exception("EmailAddress cannot be empty!!!");
            }

            return author;
        }
        public async Task<bool> Add(Author author)
        {
            try
            {
                var check = CheckValidation(author);
                if(check != null)
                {
                    return await repository.Add(author);
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
                var authorDB = await repository.GetById(id);
                if(authorDB != null)
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

        public async Task<Author> Get(int id)
        {
            try
            {   
                var author = await repository.GetById(id);
                if (author != null)
                {
                    return author;
                }
                else
                {
                    throw new Exception("Not Found Author");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Author> GetAll()
        {
            try
            {
                return repository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(int id, Author author)
        {
            try
            {
                var authorDB = await repository.GetById(id);
                var check = CheckValidation(author);
                
                if(authorDB != null)
                {
                    if (check != null)
                    {
                        authorDB.LastName = author.LastName;
                        authorDB.FirstName = author.FirstName;
                        authorDB.Phone = author.Phone;
                        authorDB.Address = author.Address;
                        authorDB.City = author.City;
                        authorDB.State = author.State;
                        authorDB.Zip = author.Zip;
                        authorDB.EmailAddress = author.EmailAddress;
                        return await repository.Update(authorDB.AuthorId, authorDB);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
