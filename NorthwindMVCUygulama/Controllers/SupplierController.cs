using Microsoft.AspNetCore.Mvc;
using NorthwindMVCUygulama.NorthwindDB;
using NorthwindMVCUygulama.SingletonDb;
using NorthwindMVCUygulama.ViewModels;
using System.Linq.Expressions;

namespace NorthwindMVCUygulama.Controllers
{
    public class SupplierController : Controller
    {
        private NorthwindContext _db;
        public SupplierController()
        {
            _db = SingletonContext.Db;
        }



        public IActionResult Index()
        {
            List<SupplierListView> supplierListViews = _db.Suppliers.Select(x => new SupplierListView()
            {
                CompanyName = x.CompanyName,
                SupplierId = x.SupplierId,
            }).OrderBy(x => x.SupplierId).ToList();
            return View(supplierListViews);
        }



        public IActionResult Details(int id)
        {
            return View(_db.Suppliers.First(x => x.SupplierId == id));
        }



        // GET: UsersController/Delete/5
        public IActionResult Delete(int id)
        {
            return View(_db.Suppliers.FirstOrDefault(x => x.SupplierId == id));
        }


        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Supplier supplier)
        {

            try
            {
                Supplier silinecek = _db.Suppliers.FirstOrDefault(x => x.SupplierId == supplier.SupplierId);
                _db.Suppliers.Remove(silinecek);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return View(supplier);
            }

        }


        // GET: UsersController/Edit/5
        public IActionResult Edit(int id)
        {
            Supplier supplier = _db.Suppliers.FirstOrDefault(x => x.SupplierId == id);
            return View(supplier);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Supplier supplier)
        {
            try
            {    //eskiyi sil yeniyi ekle
                Supplier silinecek = _db.Suppliers.FirstOrDefault(x => x.SupplierId == supplier.SupplierId);
                _db.Suppliers.Remove(silinecek);
                _db.Suppliers.Add(supplier);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View(supplier);
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
        public IActionResult Create(Supplier supplier)
        {
            try
            {   //Kontrolleri yap
                if (supplier == null)
                {
                    throw new Exception("Boş nesne");
                }
                // yazılan id listede varsa hata ver
                if (_db.Suppliers.Where(x => x.SupplierId == supplier.SupplierId).Count() > 0)
                {
                    throw new Exception("Bu id daha önce kullanılmış");
                }
                else
                {   //Doğruysa listeye ekle
                    _db.Suppliers.Add(supplier);
                    _db.SaveChanges();
                    //Yanlışsa hata fırlat
                    //Listeye yönlendir
                    return RedirectToAction(nameof(Index));
                }


              

            }
            catch (Exception ex) //hatalı veri ile birlikte yine aynı sayfaya yönlendirilir.
            {
                ViewBag.Error = ex.Message;
                return View(supplier);
            }






        }
    }
}

