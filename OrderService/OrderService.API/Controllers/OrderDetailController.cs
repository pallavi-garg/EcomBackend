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
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(string id)
        {
            return _orderProvider.GetOrderById(id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="inputData"></param>
        [HttpPut("update")]
        public void UpdateOrderDetail([FromBody] Order inputData)
        {
            _orderProvider.UpdateOrderDetail(inputData);
        }

        [HttpPost("add")]
        public ActionResult<string> AddNewOrder([FromBody] Order inputData)
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

}
