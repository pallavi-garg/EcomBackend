using Microsoft.AspNetCore.Mvc;
using OrderService.BusinessLogic.Interface;
using OrderService.Shared.Model;
using System.Collections.Generic;

namespace OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        IOrderProvider _orderProvider;
        public OrderDetailController(IOrderProvider OrderProvider)
        {
            _orderProvider = OrderProvider;
        }

        /// <summary>
        /// Not MVP
        /// TODO should return list of Order 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Order> GetOrderList()
        {
            return _orderProvider.GetAllOrders();
        }

        /// <summary>
        /// Id is id of custmer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("customer/{id}")]
        public ActionResult<Order> GetOrderByCustomerId(string id)
        {
            return _orderProvider.GetOrderByCustomerId(id);
        }

        /// <summary>
        /// id would be order id
        /// TODO should return Order 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(string id)
        {
            return _orderProvider.GetOrderById(id);
        }

        /// <summary>
        /// Not MVP
        /// </summary>
        /// <param name="inputData"></param>
        [HttpPut("{Id}")]
        public void UpdateOrderDetail([FromBody] Order inputData)
        {
            _orderProvider.UpdateOrderDetail(inputData);
        }

        [HttpPost("add")]
        public Order AddNewOrder([FromBody] Order inputData)
        {
            return _orderProvider.AddNewOrder(inputData);
        }


        /// <summary>
        /// Not MVP
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{Id}")]
        public void DeleteOrderById(string id)
        {
            _orderProvider.DeleteOrderById(id);
        }
    }

    //TODO:
    //1. sync comm to fetch product details to return complete order details
        //1.1. controller - end point
        //1.2. repo
    //2. move setting.cs to lauchsetting.json
    //3. end point to get order based on user - not mvp
}
