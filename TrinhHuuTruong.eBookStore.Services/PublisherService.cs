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
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository repository;

        public PublisherService(IPublisherRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Add(Publisher publisher)
        {
            try
            {
                return await repository.Add(publisher);
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
                var check = await repository.GetById(id);
                if(check != null)
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

        public async Task<Publisher> Get(int id)
        {
            try
            {
                var publisher = await repository.GetById(id);
                if(publisher != null)
                {
                    return publisher;
                }
                else
                {
                    throw new Exception("Not Found Publisher");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Publisher> GetAll()
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

        public async Task<bool> Update(int id, Publisher publisher)
        {
            try
            {
                var publisherDB = await repository.GetById(id);
                if(publisherDB != null)
                {
                    publisherDB.PublisherName = publisher.PublisherName;
                    publisherDB.City = publisher.City;
                    publisherDB.State = publisher.State;
                    publisherDB.Country = publisher.Country;

                    return await repository.Update(publisherDB.PublisherId, publisherDB);
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
