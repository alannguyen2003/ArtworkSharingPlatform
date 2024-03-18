using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Error;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;
using Session = Stripe.Checkout.Session;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;
using SessionService = Stripe.Checkout.SessionService;

namespace ArtworkSharingHost.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StripePaymentController : ControllerBase
{
    private static string ClientDomain = "http://localhost:4200/";
    private readonly IPackageService _packageService;

    public StripePaymentController(IPackageService packageService)
    {
        _packageService = packageService;
    }

    [HttpGet("checkout")]
    public async Task<IActionResult> CreateSessionForPayment(int packageId)
    {
        var package = _packageService.GetPackageById(packageId).Result;
        var options = new SessionCreateOptions
        {
            SuccessUrl = ClientDomain + $"checkout",
            CancelUrl = ClientDomain + "checkout-fail",
            LineItems = new List<SessionLineItemOptions>(),
            Mode = "payment"
        };
        var sessionLineItem = new SessionLineItemOptions()
        {
            PriceData = new SessionLineItemPriceDataOptions()
            {
                UnitAmount = (long)package.Price,
                Currency = "vnd",
                ProductData = new SessionLineItemPriceDataProductDataOptions()
                {
                    Name = package.Name,
                    Description = "Package for credit in Artwork Sharing Platform"
                }
            },
            Quantity = 1
        };
        options.LineItems.Add(sessionLineItem);
        var service = new SessionService();
        try
        {
            Session session = service.Create(options);
            return Ok(new CreateCheckoutSessionResponsePayment()
            {
                SessionId = session.Id,
            });
        }
        catch (StripeException ex)
        {
            return BadRequest(new ErrorResponse
            {
                ErrorMessage = new ErrorMessage
                {
                    Message = ex.StripeError.Message,
                }
            });
        }
    }
}