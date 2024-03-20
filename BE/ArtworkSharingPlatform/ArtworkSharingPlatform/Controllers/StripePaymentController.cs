using ArtworkSharingHost.Extensions;
using ArtworkSharingHost.StripePaymentService.Settings;
using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.DataTransferLayer;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response;
using ArtworkSharingPlatform.DataTransferLayer.Payload.Response.Error;
using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
	private readonly IOptions<StripeSettings> _stripeSettings;
	private readonly IAuthService _authService;
	private readonly ITransactionService _transactionService;
	private readonly ILogger<StripePaymentController> _logger;

	public StripePaymentController(
        IPackageService packageService,
        IOptions<StripeSettings> stripeSettings,
        IAuthService authService,
        ITransactionService transactionService,
        ILogger<StripePaymentController> logger
        )
    {
        _packageService = packageService;
		_stripeSettings = stripeSettings;
		_authService = authService;
		_transactionService = transactionService;
		_logger = logger;
	}

    [HttpGet("checkout")]
    public async Task<IActionResult> CreateSessionForPayment(int packageId)
    {
        var package = _packageService.GetPackageById(packageId).Result;
        var sessionMetadata = new Dictionary<string, string>();
        sessionMetadata.Add("package_id", package.Id.ToString());
        var options = new SessionCreateOptions
        {
            SuccessUrl = ClientDomain + $"checkout",
            CancelUrl = ClientDomain + "checkout-fail",
            LineItems = new List<SessionLineItemOptions>(),
            Mode = "payment",
            PaymentIntentData = new SessionPaymentIntentDataOptions(),
            CustomerEmail = User.GetEmail(),
            Metadata = sessionMetadata
        };
        var sessionLineItem = new SessionLineItemOptions()
        {
            PriceData = new SessionLineItemPriceDataOptions()
            {
                UnitAmount = (long)package.Price,
                Currency = "vnd",
                ProductData = new SessionLineItemPriceDataProductDataOptions()
                {
                    Name = "Package: " + package.Name,
                    Description = "Package for credit in Artwork Sharing Platform",
                }
            },
            Quantity = 1
        };
        options.LineItems.Add(sessionLineItem);
        var service = new SessionService();
        try
        {
            Session session = service.Create(options);
            //await _packageService.AddPackagePaymentIntent(User.GetUserId(), package.Id, session.PaymentIntentId);
            return Ok(new CreateCheckoutSessionResponsePayment()
            {
                SessionId = session.Id,
            });
        }
        catch (StripeException ex)
        {
            return BadRequest(ex.StripeError.Message);
        }
    }
	[HttpPost("webhook")]
	public async Task<IActionResult> WebHook()
	{
		var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
		try
		{
			var stripeEvent = EventUtility.ConstructEvent(
			 json,
			 Request.Headers["Stripe-Signature"],
			 _stripeSettings.Value.WHKey
		   );

			// Handle the event
			if (stripeEvent.Type == Events.PaymentIntentSucceeded)
			{
				var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
				//Do stuff
				if (paymentIntent != null)
                {
                    _logger.LogInformation(paymentIntent.Id);
                }
			}
            else if(stripeEvent.Type == Events.CheckoutSessionAsyncPaymentFailed)
            {
				var checkoutSession = stripeEvent.Data.Object as Session;
                if (checkoutSession != null && !string.IsNullOrEmpty(checkoutSession.CustomerEmail))
                {
                    var user = await _authService.GetUserByEmail(checkoutSession.CustomerEmail);
                    if (user != null)
                    {
                        await _packageService.UserBuyPackage(user.Id, Int32.Parse(checkoutSession.Metadata.GetValueOrDefault("package_id")));
                        var package = await _packageService.GetPackageById(Int32.Parse(checkoutSession.Metadata.GetValueOrDefault("package_id")));
                        var transaction = new TransactionDTO
                        {
                            SenderId = user.Id,
                            ReportName = user.Email + " buy package: " + package.Name + "FAILED",
                            CreateDate = DateTime.UtcNow,
                            TotalPrice = package.Price
                        };
                        await _transactionService.AddTransaction(transaction);
                        _logger.LogInformation("Add credit failed");
                    }
                }
			}
            else if(stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var checkoutSession = stripeEvent.Data.Object as Session;
                var user = await _authService.GetUserByEmail(checkoutSession.CustomerEmail);
                if (user != null)
                {
                    await _packageService.UserBuyPackage(user.Id, Int32.Parse(checkoutSession.Metadata.GetValueOrDefault("package_id")));
                    var package = await _packageService.GetPackageById(Int32.Parse(checkoutSession.Metadata.GetValueOrDefault("package_id")));
                    var transaction = new TransactionDTO
                    {
                        SenderId = user.Id,
                        ReportName = user.Email + " buy package: " + package.Name,
                        CreateDate = DateTime.UtcNow,
                        TotalPrice = package.Price
                    };
                    await _transactionService.AddTransaction(transaction);
                    _logger.LogInformation("Add credit success");
                }
            }
			else
			{
                _logger.LogInformation($"Unhandled Events: {stripeEvent.Type}");
			}
			return Ok();
		}
		catch (StripeException e)
		{
			return BadRequest(e.StripeError.Message);
		}
	}
}