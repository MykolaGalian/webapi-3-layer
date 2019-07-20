using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contract
{
    public interface IOrederDetailRepository
    {
        Task AddOrderDetail (OrderDetail orederDetail);
        Task Save();
    }
}
