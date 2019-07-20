using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contract
{
    public interface IUnitOfWork : IDisposable
    {

        IOrderRepository orderRepository { get; }
        IProductRepository productRepository { get; }
        IOrederDetailRepository orederDetailRepository { get; } 
        Task Save();
    }
}
