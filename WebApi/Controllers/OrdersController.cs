using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
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
    public class OrdersController : ApiController
    {
        private IDTOUnitOfWork _unitOfWork;

        public OrdersController(IDTOUnitOfWork uow)
        {
            _unitOfWork = uow;
        }


        // GET: api/Orders - просмотр всех товаров - getOrdersAll
        [HttpGet]
        public HttpResponseMessage GetOrders_()
        {

            var orders = this._unitOfWork.orderServices.GetOrders();

            if (orders == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, orders);
        }

        // GET: api/Orders/5 -  просмотр выбранного заказа - getOrderById
        [HttpGet]
        public async Task<HttpResponseMessage> GetOrder_(int id)
        {
            var order = await _unitOfWork.orderServices.GetOrder(id);
            if (order == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, order);
        }

        // POST: api/Orders  -создание заказа  - AddOrder
        [HttpPost]
        public async Task<HttpResponseMessage> PostOrder_([FromBody] OrderDTO orderDto)
        {

            if (ModelState.IsValid)
            {
                await _unitOfWork.orderServices.PostOrder(orderDto);

                return this.Request.CreateResponse(HttpStatusCode.Created, orderDto);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        //GET /api/orders/{id}/products - просмотр списка товаров выбранного заказа - GetProductsByOrderId
        [HttpGet]
        [Route("api/orders/{id}/products")]
        public async Task<HttpResponseMessage> GetProductsByOrderId_(int id)
        {
            var order = await _unitOfWork.orderServices.GetProductsByOrderId(id);
            if (order == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, order);
        }


        //PUT /api/orders/{Id}/products - добавление товара к выбранному заказу - AddProductsToOrderById
        [HttpPut]
        [Route("api/orders/{id}/products")]
        public async Task<HttpResponseMessage> PutProductForOrder_(int id, [FromBody] OrderDetailDTO orderDetailDto)
        {
            if (!ModelState.IsValid)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            if (id != orderDetailDto.OrderId)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            await _unitOfWork.orderServices.PutProductForOrder(id, orderDetailDto);
            return this.Request.CreateResponse(HttpStatusCode.OK, orderDetailDto);
        }
    }

}