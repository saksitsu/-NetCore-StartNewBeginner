using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAKSIT.Data;

namespace SAKSIT.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class APIController : Controller
    {
        private readonly DataContext _context;
        public APIController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ActionName("GET_ORDER")]
        public JsonResult GET_ORDER()
        {
            var response = _context.order_detail.Where(s => s.order_List.status == true)
                .Include(p => p.product)
                .Include(o => o.order_List)
                .ThenInclude(x => x.customer).ToList();

            return Json(response);
        }
    }
}