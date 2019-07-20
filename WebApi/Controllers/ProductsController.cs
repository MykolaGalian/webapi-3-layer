using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using BLL.Contracts;
using BLL.DTO;

namespace WebApi.Controllers
{
    public class ProductsController : ApiController
    {
        private IDTOUnitOfWork _unitOfWork;
        
        public ProductsController(IDTOUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        // GET: api/Products - просмотр всех товаров - getProductsAll
        [HttpGet]
        public HttpResponseMessage GetProducts_() 
        {
            var prod = _unitOfWork.productServices.GetProducts();
            
            if (prod == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, prod);
        }

        // GET: api/Products/5  - просмотр выбранного товара  - getProductById
        [HttpGet]
        public async Task<HttpResponseMessage> GetProducts_(int id)
        {
            var prod = await _unitOfWork.productServices.GetProduct(id);
           
            if (prod == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, prod);
        }


        // POST: api/Products -добавление товара в систему - AddProduct
        [HttpPost]
        public async Task<HttpResponseMessage> PostProduct_([FromBody]ProductDTO product)
        {
            if (ModelState.IsValid)
            {
               await _unitOfWork.productServices.PostProduct(product);

               return this. Request.CreateResponse(HttpStatusCode.Created, product);
            }
            else
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
       
    }
}