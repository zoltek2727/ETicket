using ETicket.Data;
using ETicket.Helpers;
using ETicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ETicket.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        private UserManager<IdentityUser> _userManager;

        public CartController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Cart
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;

            if (cart != null)
            {
                var tickets = _context.Tickets.AsNoTracking();
                                             
                ViewBag.total = cart.Sum(item => item.Ticket.TicketPrice * item.Quantity);
                return View(await tickets.ToListAsync());
            }

            return View();
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> GetCartAmount()
        {
            var cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");

            var names = "0";

            try
            {
                if (cart != null)
                {
                    names = cart.Count.ToString();
                }

                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("Buy/{id}/{amount}")]
        public IActionResult Buy(int id, string amount)
        {
            if(string.IsNullOrWhiteSpace(amount))
            {
                amount = "1";
            }

            Tickets ticketModel = new Tickets();
            if (SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart") == null)
            {
                var cart = new List<ShoppingCart>();
                cart.Add(new ShoppingCart() { Ticket = _context.Tickets.Find(id), Quantity = Convert.ToInt32(amount) });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<ShoppingCart> cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new ShoppingCart() { Ticket = _context.Tickets.Find(id), Quantity = Convert.ToInt32(amount) });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Order()
        {
            var cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;

            if (cart != null)
            {
                var products = _context.Tickets.AsNoTracking()
                                                .AsQueryable();

                ViewBag.total = cart.Sum(item => item.Ticket.TicketPrice * item.Quantity);
                return View(products);
            }

            return View();
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<ShoppingCart> cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Purchase()
        {
            List<ShoppingCart> cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");

            var id = _userManager.GetUserId(User);

            try
            {
                Purchases p = new Purchases
                {
                    PurchaseTicketDate = DateTime.Now,
                    Id = id
                };
                _context.Add(p);

                //LeasingDetail ld;

                //foreach (var item in cart)
                //{
                //    ld = new LeasingDetail
                //    {
                //        LeasingId = l.LeasingId,
                //        LeasingDetailAmount = item.Quantity,
                //        LeasingDetailExtend = true,
                //        LeasingDetailEnd = DateTime.Now.AddYears(1),
                //        ProductId = item.Product.ProductId
                //    };
                //    var product = _context.Products.Where(p => p.ProductId == item.Product.ProductId).ToList();
                //    foreach(var prod in product)
                //    {
                //        prod.ProductAvailability -= item.Quantity;
                //    }

                //    _context.Add(ld);
                //}

                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("techleasingshop@gmail.com", "dup@1234");

                var user = await _userManager.GetUserAsync(User);
                var email = user.Email;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("techleasingshop@gmail.com");
                mailMessage.To.Add(email);
                mailMessage.Body = "Order no. "+p.PurchaseId;
                mailMessage.Subject = "Your order is completed. Please pay your bill.";
                mailMessage.Attachments.Add(new Attachment(@"C:\PDF\Leasing.pdf"));
                client.Send(mailMessage);

                _context.SaveChanges();
            }
            catch
            {
                ViewBag.ErrorMessage = "Your cart can't be empty";
                return RedirectToAction("Index", "Purchases", new { id = _context.Purchases.Last().PurchaseId });
            }
                
            cart = null;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return RedirectToAction("Index", "Purchases");
        }

        private int isExist(int id)
        {
            List<ShoppingCart> cart = SessionHelper.GetObjectFromJson<List<ShoppingCart>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Ticket.TicketId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
