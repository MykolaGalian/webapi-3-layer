using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contract;
using DAL.EF;
using DAL.Model;

namespace DAL.Repository
{
    public class OrederDetailRepository : IOrederDetailRepository
    {
        private readonly ProdDBContext _db;

        public OrederDetailRepository(ProdDBContext db)
        {
            _db = db;
        }
        public async Task AddOrderDetail(OrderDetail orederDetail)
        {
            _db.OrderDetails_.Add(orederDetail);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
