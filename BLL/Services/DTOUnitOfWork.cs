﻿using BLL.Contracts;
using DAL.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;


namespace BLL.Services
{
    public class DTOUnitOfWork : IDTOUnitOfWork
    {
        private readonly IUnitOfWork _uow;
        private OrderServices _orderServices;
        private ProductServices _productServices;


        public DTOUnitOfWork(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IProductService productServices
        {
            get
            {
                if (this._productServices == null)
                {
                    this._productServices = new ProductServices(_uow);
                }
                return _productServices;
            }
        }

        public IOrderService orderServices
        {
            get
            {
                if (this._orderServices == null)
                {
                    this._orderServices = new OrderServices(_uow);
                }
                return _orderServices;
            }
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _uow?.Dispose();

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
