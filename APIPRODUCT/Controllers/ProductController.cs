using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using APIPRODUCT.Models;

namespace APIPRODUCT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DbproductContext _dbcontext;

        public ProductController(DbproductContext dbcontext)
        {
            _dbcontext= dbcontext;
        }

        [HttpGet]
        [Route("List")]
        public IActionResult Lista()
        {
            List<ProductsInf> list = new List<ProductsInf>();
            try
            {

                list = _dbcontext.ProductsInfs.Include(c => c.oCategory).ToList();
                return StatusCode(StatusCodes.Status200OK, new { menssage = "ok", response = list });
            }
            catch (Exception e) { return StatusCode(StatusCodes.Status200OK, new { menssage = e, response = list }); }
        }



        [HttpGet]
        [Route("Obtain/{id:int}")]
        public IActionResult Obtain(int id)
        {
            ProductsInf oProduct = _dbcontext.ProductsInfs.Find(id);
            if (oProduct == null) { return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Product not found"}); }
            try
            {
                oProduct = _dbcontext.ProductsInfs.Include(c => c.oCategory).Where(p => p.Id == id).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { menssage = "ok", reponse = oProduct });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { menssage = e, response = oProduct });
            }


        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Save([FromBody] ProductsInf obj)
        {
            try
            {
                _dbcontext.ProductsInfs.Add(obj);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status200OK, new { menssage = ex.Message }); }
        }


        [HttpPut]
        [Route("Modify")]
        public IActionResult Modify([FromBody] ProductsInf obj)
        {
            ProductsInf oProduct = _dbcontext.ProductsInfs.Find(obj.Id);
            if (oProduct == null) { return BadRequest("Product not found"); }
            try
            {
                oProduct.Name = obj.Name is null ? oProduct.Name : obj.Name;
                oProduct.Price = obj.Price is null ? oProduct.Price : obj.Price;
                oProduct.IdCategory = obj.IdCategory is null ? oProduct.IdCategory : obj.IdCategory;


                _dbcontext.ProductsInfs.Update(oProduct);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { menssage = "ok" });
            }
            catch (Exception e) { return StatusCode(StatusCodes.Status200OK, new { menssage = e.Message }); }

        }

        [HttpDelete]
        [Route("Delete/{idProduct:int}")]
        public IActionResult Delete(int idProduct)
        {
            ProductsInf oProduct = _dbcontext.ProductsInfs.Find(idProduct);
            if (oProduct == null) { return BadRequest("Producto No encontrado"); }
            try
            {
                _dbcontext.ProductsInfs.Remove(oProduct);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { menssage = "ok" });
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status200OK, new { menssage = ex.Message }); }

        }


    }
}
