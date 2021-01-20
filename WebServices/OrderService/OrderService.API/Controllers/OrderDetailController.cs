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
        [HttpGet]
        public IEnumerable<OrderDetails> GetOrderList()
        {
            return _orderProvider.GetAllOrders();
        }

        [HttpGet("{id}")]
        public ActionResult<OrderDetails> GetOrderById(int id)
        {
            return _orderProvider.GetOrderById(id);
        }

        //[HttpGet("OrderName/{OrderName}")]
        //public ActionResult<OrderDetails> GetOrderByProductName(string productName)
        //{
        //    return _orderProvider.GetOrderByProductName(productName);
        //}

        [HttpPut("{Id}")]
        public void UpdateOrderDetail([FromBody] OrderDetails inputData)
        {
            _orderProvider.UpdateOrderDetail(inputData);
        }

        [HttpPost("AddNew")]
        public void AddNewOrder([FromBody] OrderDetails inputData)
        {
            _orderProvider.AddNewOrder(inputData);
        }

        [HttpDelete("{Id}")]
        public void DeleteOrderById(int id)
        {
            _orderProvider.DeleteOrderById(id);
        }
    }
}
