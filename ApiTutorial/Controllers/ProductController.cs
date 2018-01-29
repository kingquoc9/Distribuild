using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiTutorial;
using System.Data.Entity;

namespace ApiTutorial.Controllers
{
    public class ProductController : ApiController
    {
        private Model1 db = new Model1();
        [HttpGet]
        public IHttpActionResult Get()
        {
            IHttpActionResult ret = null;
            List<Product> list = new List<Product>();
            list = db.Products.ToList();
            ret = Ok(list);
            return ret;
        }

        private List<Product> CreateMockData()
        {
            List<Product> ret = new List<Product>();
            ret.Add(new Product()
            {
                ProductId = 1,
                ProductName = "Extending Bootstrap with CSS, JavaScript and jQuery",
                IntroductionDate = Convert.ToDateTime("6/11/2015"),
                Url = "http://bit.ly/1SNzc0i"
            });

            ret.Add(new Product()
            {
                ProductId = 2,
                ProductName = "Build your own Bootstrap Business Application Template in MVC",
                IntroductionDate = Convert.ToDateTime("1/29/2015"),
                Url = "http://bit.ly/1I8ZqZg"
            });

            ret.Add(new Product()
            {
                ProductId = 3,
                ProductName = "Building Mobile Web Sites Using Web Forms, Bootstrap, and HTML5",
                IntroductionDate = Convert.ToDateTime("8/28/2014"),
                Url = "http://bit.ly/1J2dcrj"
            });

            return ret;
        }

        [HttpGet()]
        public IHttpActionResult Get(int id)
        {
            IHttpActionResult ret;
            List<Product> list = new List<Product>();
            Product prod = new Product();

            prod = db.Products.Where(p => p.ProductId == id).First();
            if (prod == null)
            {
                ret = NotFound();
            }
            else
            {
                ret = Ok(prod);
            }

            return ret;
        }

        [HttpPost()]
        public IHttpActionResult Post(Product product)
        {
            IHttpActionResult ret = null;
            product = db.Products.Add(product);
            db.SaveChanges();
            if (product != null)
            {
                ret = Created<Product>(Request.RequestUri +
                     product.ProductId.ToString(), product);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        [HttpPut()]
        public IHttpActionResult Put(int id, Product product)
        {
            IHttpActionResult ret = null;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            if (true)
            {
                ret = Ok(product);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            IHttpActionResult ret = null;
            var product = db.Products.Remove(db.Products.Where(item => item.ProductId == id).First());
            db.SaveChanges();
            if (true)
            {
                ret = Ok(true);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}