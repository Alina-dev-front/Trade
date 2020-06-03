using System;
using System.Collections.Generic;
using System.Text;
using Trade.Models;

namespace Trade.Repositories
{
    public interface IShopsRepository
    {
        List<Shop> GetAll();
        void Delete(Shop shop);
        void Insert(Shop shop);
        void Save();
    }
}
