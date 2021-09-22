using System.IO;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using Order = Core.Entities.OrderAggregate.Order;

namespace API.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        //Secret key for Stripe web hook, this will only work locally, and is unique
        private const string WhSecret = "whsec_dtmtyr3dlv2eONfSl5CP9wvdnveMWppg";
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentService paymentService, IMapper mapper, ILogger<PaymentsController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _paymentService = paymentService;
        }


        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var customerBasketToReturn = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (customerBasketToReturn is null)
            {
                return BadRequest(new ApiResponse(400, "Problem with your basket"));
            }
            return Ok(_mapper.Map<CustomerBasket, CustomerBasketDto>(customerBasketToReturn));
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret);


            PaymentIntent intent;
            Order order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeeded:", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);

                    if (order is null)
                    {
                        _logger.LogError("Order with intent id not found", intent.Id);
                    }
                    else
                    {
                        _logger.LogInformation("Order updated to payment received", order.Id);
                    }

                    break;

                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Failed", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);

                    if (order is null)
                    {
                        _logger.LogError("Order with intent id not found", intent.Id);
                    }
                    else
                    {
                        _logger.LogInformation("Order updated to payment received", order.Id);
                    }

                    break;
            }

            return new EmptyResult();
        }
    }
}