using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using inventario.Models;
using inventario.Models.Database;
using inventario.Context;

namespace inventario.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {

        private readonly ILogger<ProductoController> _logger;
        private readonly AppDbContext dbContext;
        public List<Producto> Producto { get; set; }
        public ProductoController(ILogger<ProductoController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                return Ok(dbContext.Productos.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}", Name = "GetById")]
        public ActionResult GetById(int id)
        {
            try
            {
                var producto = dbContext.Productos.FirstOrDefault(Productos => Productos.Id == id);
                return Ok(producto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Producto Producto)
        {
            try
            {
                if (Producto.Id == id)
                {
                    dbContext.Entry(Producto).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    return CreatedAtRoute("GetById", new { id = Producto.Id }, Producto);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var Producto = dbContext.Productos.FirstOrDefault(c => c.Id == id);
                if (Producto != null)
                {
                    dbContext.Productos.Remove(Producto);
                    dbContext.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult FormsTestPost([FromBody] Producto Producto)
        {
            try
            {
                dbContext.Productos.Add(Producto);
                dbContext.SaveChanges();
                return CreatedAtRoute("GetById", new { Producto.Id }, Producto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
