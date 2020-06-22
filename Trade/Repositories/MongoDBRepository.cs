using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Trade.Models;
using System.IO;
using System.Linq;

namespace Trade.Repositories
{
    public class MongoDBRepository : IProductRepository
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<Product> _collection;

        public void MongoDbProductRepository(IMongoCollection<Product> collection)
        {
            _client = new MongoClient("213.67.205.10/32");
            _database = _client.GetDatabase("Trade");
            _collection = _database.GetCollection<Product>("Products");
        }

        public List<Product> GetAll()
        {
            var all = _collection.Find(Builders<Product>.Filter.Empty);
            return all.ToList();
        }

        public Product GetById(string id)
        {
            return _collection.Find(Builders<Product>.Filter.Eq(x => x.Id, id)).FirstOrDefault();
        }

        public void Delete(Product product)
        {
            _collection.DeleteOne(s => s.Id == product.Id);
        }

        public void Insert(Product product)
        {
            _collection.InsertOne(product);
        }

        //Update method, needed for MongoDB implementation
        //Not part of IRepository at the moment.
        //This code will actually replace an existing document with the new data.
        //All fields will be overwritten. Use UpdateOne to change just part of object.
        public void Update(Product product)
        {
            _collection.ReplaceOne(p => p.Id == product.Id, product);
        }


        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
