using Microsoft.AspNetCore.Mvc;
using NorthwindMVCUygulama.NorthwindDB;
using NorthwindMVCUygulama.SingletonDb;
using NorthwindMVCUygulama.ViewModels;

namespace NorthwindMVCUygulama.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            List<CategoryListView> categoryLists = SingletonContext.Db.Categories.Select(x => new CategoryListView()
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
            }).OrderBy(x => x.CategoryId).ToList();
            return View(categoryLists);
        }

        public IActionResult Details(int id)
        {
            return View(SingletonContext.Db.Categories.First(x => x.CategoryId == id));
        }


        // GET: UsersController/Delete/5
        public IActionResult Delete(int id)
        {
            return View(SingletonContext.Db.Categories.FirstOrDefault(x => x.CategoryId == id));
        }


        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Category category)
        {

            try
            {
                Category silinecek = SingletonContext.Db.Categories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
                SingletonContext.Db.Categories.Remove(silinecek);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return View(category);
            }

        }



        // GET: UsersController/Edit/5
        public IActionResult Edit(int id)
        {
            Category category = SingletonContext.Db.Categories.FirstOrDefault(x => x.CategoryId == id);
            return View(category);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            try
            {
                //eskiyi sil yeniyi ekle
                Category silinecek = SingletonContext.Db.Categories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
                SingletonContext.Db.Categories.Remove(silinecek);
                SingletonContext.Db.Categories.Add(category);
                SingletonContext.Db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View(category);
            }
        }




        // GET: UsersController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            try
            {   //Kontrolleri yap
                if (category == null)
                {
                    throw new Exception("Boş nesne");
                }
                // yazılan id listede varsa hata ver
                if (SingletonContext.Db.Categories.Where(x => x.CategoryId == category.CategoryId).Count() > 0)
                {
                    throw new Exception("Bu id daha önce kullanılmış");
                }
                else
                {   //Doğruysa listeye ekle
                    SingletonContext.Db.Categories.Add(category);
                    SingletonContext.Db.SaveChanges();
                    //Yanlışsa hata fırlat
                    //Listeye yönlendir
                    return RedirectToAction(nameof(Index));
                }


               

            }
            catch (Exception ex) //hatalı veri ile birlikte yine aynı sayfaya yönlendirilir.
            {
                ViewBag.Error = ex.Message;
                return View(category);
            }
        }
    }
}

