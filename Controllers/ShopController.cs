using Churn.Data;
using Churn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Stripe;
using Stripe.Checkout;
using System.Diagnostics;
using System.Security.Claims;

namespace Churn.Controllers
{

    public class ShopController : Controller
    {

        //property for database connection 
        private ApplicationDbContext _context;
        private IConfiguration _configuration;

        //constructor 
        public ShopController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Categories
                .OrderBy(category => category.Name)
                .ToListAsync();

            return View(products);
        }


        public async Task<IActionResult> Details(int? id)
        {
            var categoryWithProducts = await _context.Categories
                .Include(category => category.Products)
                .FirstOrDefaultAsync(category => category.Id == id);

            return View(categoryWithProducts);
        }


        public async Task<IActionResult> ProductDetails(int? id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(product => product.Id == id);


            return View(product);
        }



        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            //get the logged in user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //get a cart
            var cart = await _context.Carts
                .FirstOrDefaultAsync(cart => cart.UserId == userId && cart.Active == true);

            //check that there is an active cart
            if (cart == null)
            {
                cart = new Models.Cart { UserId = userId };
                await _context.AddAsync(cart);
                await _context.SaveChangesAsync();
            }

            //find product

            var product = await _context.Products
                .FirstOrDefaultAsync(product => product.Id == productId);


            //get out of method if not product found 

            if (product == null)
            {
                return NotFound();
            }

            //create a new cart item
            var cartItem = new CartItem
            {
                Cart = cart,
                Product = product,
                Quantity = quantity,
                Price = (decimal)product.AnnualFee
            };


            //if model state valid do whwat we wnat
            if (ModelState.IsValid)
            {
                await _context.AddAsync(cartItem);
                await _context.SaveChangesAsync();

                return RedirectToAction("ViewMyCart");
            }

            //otherwise leave method
            return NotFound();

        }



        [Authorize]
        public async Task<IActionResult> ViewMyCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = await _context.Carts
                .Include(cart => cart.User)
                .Include(cart => cart.CartItems)
                .ThenInclude(cartItem => cartItem.Product)
                .FirstOrDefaultAsync(cart => cart.UserId == userId && cart.Active == true);

            return View(cart);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteCartItem(int cartItemId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = await _context.Carts
                .FirstOrDefaultAsync(cart => cart.UserId == userId && cart.Active == true);


            if (cart == null) return NotFound();

            var cartItem = await _context.CartItems
                .Include(cartItem => cartItem.Product)
                .FirstOrDefaultAsync(cartItem => cartItem.Cart == cart && cartItem.Id == cartItemId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();

                return RedirectToAction("ViewMyCart");
            }

            return NotFound();
        }




        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = await _context.Carts
                .Include(cart => cart.User)
                .Include(cart => cart.CartItems)
                .ThenInclude(cartItem => cartItem.Product)
                .FirstOrDefaultAsync(cart => cart.UserId == userId && cart.Active == true);

            var order = new Order
            {
                UserId = userId,
                Cart = cart,
                Total = ((decimal)(cart.CartItems.Sum(cartItem => (cartItem.Price * cartItem.Quantity)))),
                ShippingAddress = "",
                PaymentMethod = PaymentMethods.VISA
            };

            ViewData["PaymentMethods"] = new SelectList(Enum.GetValues(typeof(PaymentMethods)));

            return View( order);
        }




        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Payment(string shippingAddress, PaymentMethods paymentMethod)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = await _context.Carts
                .Include(cart => cart.CartItems)
                .FirstOrDefaultAsync(cart => cart.UserId == userId && cart.Active == true);

           

            if (cart == null)
            {
                return NotFound();
            }


            //add order data to the session
            HttpContext.Session.SetString("Shipping Address", shippingAddress);
            HttpContext.Session.SetString("Payment Method", paymentMethod.ToString());

            //SET THE STRIPE API KEY 
            StripeConfiguration.ApiKey = _configuration.GetSection("Stripe")["SecretKey"];

            //create our Stripe options
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(cart.CartItems.Sum(cartItem => cartItem.Quantity * cartItem.Price) * 100),
                            Currency = "cad",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Churn Purchase"
                            },
                        },
                        Quantity = 1,
                    },
                },
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                Mode = "payment",
                SuccessUrl = "https://" + Request.Host + "/Shop/SaveOrder",
                CancelUrl = "https://" + Request.Host + "/Shop/ViewMyCart",
            };

            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);

        }




        public async Task<IActionResult> SaveOrder()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = await _context.Carts
                .Include(cart => cart.CartItems)
                .FirstOrDefaultAsync(cart => cart.UserId == userId && cart.Active == true);

            var paymentMethod = HttpContext.Session.GetString("Payment Method");
            var shippingAddress = HttpContext.Session.GetString("Shipping Address");

            var order = new Order
            {
                UserId = userId,
                Cart = cart,
                Total = cart.CartItems.Sum(CartItem => CartItem.Quantity * CartItem.Price),
                ShippingAddress = shippingAddress,
                PaymentMethod = PaymentMethods.Stripe,
                PaymentReceived = true
            };

            await _context.AddAsync(order);
            await _context.SaveChangesAsync();

            cart.Active = false;
            _context.Update(cart);
            await _context.SaveChangesAsync();

            return RedirectToAction("OrderDetails", new {id = order.Id});
        }





        [Authorize]
        public async Task<IActionResult> OrderDetails(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var order = await _context.Orders
                .Include(order => order.User)
                .Include(order => order.Cart)
                .ThenInclude(cart => cart.CartItems)
                .ThenInclude(cartItem => cartItem.Product)
                .FirstOrDefaultAsync(order => order.UserId == userId && order.Id == id);


            if (order == null)
            {
                return NotFound();
            }

            return View(order); 
        }

        [Authorize]
        public async Task<IActionResult> Orders()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var orders = await _context.Orders
                .OrderByDescending(order => order.Id)
                .Where(order => order.UserId == userId)
                .ToListAsync();


            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

    }



}
