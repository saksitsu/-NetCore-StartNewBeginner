using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SAKSIT.Data;
using SAKSIT.Models;

namespace SAKSIT.Controllers
{
    public class orderController : Controller
    {
        private readonly DataContext _context;

        public orderController(DataContext context)
        {
            _context = context;
        }

        // GET: order
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.order_list.Include(o => o.customer);
            return View(await dataContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> NewOrder()
        {
            var customer = new SelectList(await _context.customer.ToListAsync(), "customer_id", "firstname");
            var product = new SelectList(await _context.product.ToListAsync(), "product_id", "product_name");

            ViewData["customer"] = customer;
            ViewData["product"] = product;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewOrder(order_detail od)
        {
            var customer = new SelectList(await _context.customer.ToListAsync(), "customer_id", "firstname",od.order_List.customer_id);
            var product = new SelectList(await _context.product.ToListAsync(), "product_id", "product_name",od.product_id);

            using(var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    order_list ol = new order_list();
                    ol.order_id = od.order_id;
                    ol.customer_id = od.order_List.customer_id;
                    ol.status = od.order_List.status;
                    ol.order_date = System.DateTime.Now;
                    _context.Add(ol);
                    await _context.SaveChangesAsync();

                    _context.Add(od);
                    await _context.SaveChangesAsync();

                    tran.Commit();
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                }

                tran.Dispose();
            }

            ViewData["customer"] = customer;
            ViewData["product"] = product;


            return View();
        }

        [HttpGet]
        public IActionResult ShowOrder()
        {
            List<order_detail> od = new List<order_detail>();
            string Result = "";
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:49997/API/GET_ORDER");
                httpWebRequest.Timeout = (1000 * 60);//Time out 1 min.
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Accept = "application/json"; // Determines the response type as XML or JSON etc
                //httpWebRequest.Headers.Add("MyHeader", "123");//Header data
                //// Connecting to the server. Sending request and receiving response
                //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                //{
                //    streamWriter.Write(data_json);
                //    streamWriter.Flush();
                //    streamWriter.Close();
                //}
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    Result = streamReader.ReadToEnd();
                }

                od = JsonConvert.DeserializeObject<List<order_detail>>(Result);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse err = ex.Response as HttpWebResponse;
                    if (err != null)
                    {
                        string htmlResponse = new StreamReader(err.GetResponseStream()).ReadToEnd();

                        Result = string.Format("{0} {1}", err.StatusDescription, htmlResponse);
                    }
                }

            }
            catch (Exception ex)
            {
                Result = "Error : " + ex.Message.ToString();
            }

            return View(od);
        }

        // GET: order/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order_list = await _context.order_list
                .Include(o => o.customer)
                .FirstOrDefaultAsync(m => m.order_id == id);
            if (order_list == null)
            {
                return NotFound();
            }

            return View(order_list);
        }

        // GET: order/Create
        public IActionResult Create()
        {
            ViewData["customer_id"] = new SelectList(_context.customer, "customer_id", "customer_id");
            return View();
        }

        // POST: order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("order_id,customer_id,order_date,status")] order_list order_list)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order_list);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["customer_id"] = new SelectList(_context.customer, "customer_id", "customer_id", order_list.customer_id);
            return View(order_list);
        }

        // GET: order/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order_list = await _context.order_list.FindAsync(id);
            if (order_list == null)
            {
                return NotFound();
            }
            ViewData["customer_id"] = new SelectList(_context.customer, "customer_id", "customer_id", order_list.customer_id);
            return View(order_list);
        }

        // POST: order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("order_id,customer_id,order_date,status")] order_list order_list)
        {
            if (id != order_list.order_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order_list);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!order_listExists(order_list.order_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["customer_id"] = new SelectList(_context.customer, "customer_id", "customer_id", order_list.customer_id);
            return View(order_list);
        }

        // GET: order/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order_list = await _context.order_list
                .Include(o => o.customer)
                .FirstOrDefaultAsync(m => m.order_id == id);
            if (order_list == null)
            {
                return NotFound();
            }

            return View(order_list);
        }

        // POST: order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var order_list = await _context.order_list.FindAsync(id);
            _context.order_list.Remove(order_list);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool order_listExists(string id)
        {
            return _context.order_list.Any(e => e.order_id == id);
        }
    }
}
