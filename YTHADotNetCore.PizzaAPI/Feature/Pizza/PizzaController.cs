﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YTHADotNetCore.PizzaAPI.Database;

namespace YTHADotNetCore.PizzaAPI.Feature.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PizzaController()
        {
            _context = new AppDbContext();
        }

        [HttpGet("Pizza")]
        public async Task<IActionResult>GetPizza()
        {
            var lst = await _context.Pizzas.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("Extras")]
        public async Task<IActionResult> GetPizzaExtra()
        {
            var lst = await _context.PizzaExtras.ToListAsync();
            return Ok(lst);
        }

        [HttpPost("OrderPizza")]
        public async Task<IActionResult> PizzaOrder(OrderRequest orderRequest)
        {
            var pizza = _context.Pizzas.FirstOrDefault(x => x.Id == orderRequest.PizzaId);
            var total = pizza.Price;

            if(orderRequest.Extra.Length > 0)
            {
                var extraLst = await _context.PizzaExtras.Where(x => orderRequest.Extra.Contains(x.Id)).ToListAsync();
                total += extraLst.Sum(x => x.Price);
            }

            string invoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");
            PizzaOrderModel pizzaOrderModel = new PizzaOrderModel()
            {
                InvoiceNo = invoiceNo,
                PizzaId = orderRequest.PizzaId,
                TotalPrice = total,
            };

            List<PizzaOrderDetailModel> pizzaOrderDetailModels = orderRequest.Extra.Select(extraId => new PizzaOrderDetailModel()
            {
                InvoiceNo = invoiceNo,
                PizzaExtraId = extraId,
            }).ToList();

            MessageResponse messageResponse = new MessageResponse()
            {
                InvoiceNo = invoiceNo,
                TotalAmount = total,
                Message = "Thank you for your order! Enjoy with your pizza."
            };

            await _context.PizzaOrders.AddAsync(pizzaOrderModel);
            await _context.PizzaOrderDetails.AddRangeAsync(pizzaOrderDetailModels);
            await _context.SaveChangesAsync();

            return Ok(messageResponse);
        }

        [HttpGet("Order/{invoiceNo}")]
        public async Task<IActionResult> GetOrder(string invoiceNo)
        {
            var item = await _context.PizzaOrders.FirstOrDefaultAsync(x => x.InvoiceNo == invoiceNo);
            var orderDetailList = await _context.PizzaOrderDetails.Where(x => x.InvoiceNo == invoiceNo).ToListAsync();
            return Ok( new { Order = item, OrderDetail = orderDetailList } );
        }
    }
}
