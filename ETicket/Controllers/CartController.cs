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
using System.Text;
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
                var tickets = _context.Tickets.Include(e=>e.Event).AsNoTracking();
                                             
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
        public IActionResult Buy(int id, int amount)
        {
            if(amount==0)
            {
                amount = 1;
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
                var tickets = _context.Tickets.AsNoTracking()
                                                .AsQueryable();

                ViewBag.total = cart.Sum(item => item.Ticket.TicketPrice * item.Quantity);
                ViewBag.listOfDeliveries = _context.Deliveries.OrderBy(d => d.DeliveryType).ToList();

                return View(tickets);
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

                mailMessage.Subject = "Your order is ready. Please pay your bill.";

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Your order:");
                foreach (var c in cart)
                {
                    sb.AppendLine(_context.Events.Where(e => e.EventId == c.Ticket.EventId).FirstOrDefault().EventName +" "+c.Ticket.TicketName+" "+c.Ticket.TicketPrice+" "+c.Quantity+" "+c.Ticket.TicketPrice*c.Quantity);
                    Purchases p = new Purchases
                    {
                        PurchaseTicketDate = DateTime.Now,
                        Id = id,
                        DeliveryId = 1,
                        PurchaseAmount = c.Quantity,
                        Ticket = c.Ticket
                    };
                    
                    var ticket = _context.Tickets.Where(t => t.TicketId == c.Ticket.TicketId).ToList();
                    foreach(var tick in ticket)
                    {
                        tick.TicketAvailability -= c.Quantity;
                    }

                    _context.Add(p);
                }

                _context.SaveChanges();

                sb.AppendLine("Thanks for buying at our store");

                mailMessage.Body = sb.ToString();
                client.Send(mailMessage);
            }
            catch
            {
                ViewBag.ErrorMessage = "Your cart can't be empty";
                return RedirectToAction("Index", "Home");
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
