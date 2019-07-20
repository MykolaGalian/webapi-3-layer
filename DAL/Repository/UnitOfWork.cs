using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contract;
using DAL.EF;

namespace DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ProdDBContext _db;
        private OrderRepository _orderRepository;
        private ProductRepository _productRepository;
        private OrederDetailRepository _orederDetailRepository;

        public UnitOfWork(ProdDBContext db)
        {
            _db = db;
        }

        public IOrderRepository orderRepository
        {
            get
            {
                if (this._orderRepository == null)
                {
                    this._orderRepository = new OrderRepository(_db);
                }
                return _orderRepository;
            }
        }

        public IProductRepository productRepository
        {
            get
            {
                if (this._productRepository == null)
                {
                    this._productRepository = new ProductRepository(_db);
                }
                return _productRepository;
            }
        }

        public IOrederDetailRepository orederDetailRepository
        {
            get
            {
                if (this._orederDetailRepository == null)
                {
                    this._orederDetailRepository = new OrederDetailRepository(_db);
                }
                return _orederDetailRepository;
            }
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync(); // for service
        }


        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
