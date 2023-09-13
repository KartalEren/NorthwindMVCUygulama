using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindMVCUygulama.NorthwindDB;
using NorthwindMVCUygulama.SingletonDb;
using NorthwindMVCUygulama.ViewModels;

namespace NorthwindMVCUygulama.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            List<ProductListView> productLists=SingletonContext.Db.Products.Select(x=> new ProductListView()
            {CategoryId=x.CategoryId,
            ProductId=x.ProductId,
            ProductName=x.ProductName,
            SupplierId=x.SupplierId,

            }).OrderBy(x => x.ProductId).ToList();
            return View(productLists);
        }


        // GET: LoginsController/Edit/5
        public ActionResult Details(int id)
        {
            

            return View(SingletonContext.Db.Products.First(x => x.ProductId == id));
        }




        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(SingletonContext.Db.Products.FirstOrDefault(x=>x.ProductId==id));
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Product product)
        {
            try
            {
                Product silinecek = SingletonContext.Db.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
                SingletonContext.Db.Products.Remove(silinecek);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(product);
            }
        }




        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = SingletonContext.Db.Products.FirstOrDefault(x=>x.ProductId==id);
            return View(product);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            try
            {
                //eskiyi sil yeniyi ekle
                Product silinecek = SingletonContext.Db.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
                SingletonContext.Db.Products.Remove(silinecek);
                SingletonContext.Db.Products.Add(product);
                return RedirectToAction(nameof(Index)); 
              
            }
            catch
            {
                return View(product);
            }
        }










        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                //Kontrolleri yap
                if (product == null)
                {
                    throw new Exception("Boş nesne");
                }
                // yazılan id listede varsa hata ver
                if (SingletonContext.Db.Products.Where(x => x.ProductId == product.ProductId).Count() > 0)
                {
                    throw new Exception("Bu id daha önce kullanılmış");
                }

                //Doğruysa listeye ekle
                SingletonContext.Db.Products.Add(product);

                //Yanlışsa hata fırlat

                //Listeye yönlendir
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(product);      //hatalı veri ile birlikte yine aynı sayfaya yönlendirilir.
            }
        }
    }
}
