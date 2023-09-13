using Microsoft.AspNetCore.Mvc;
using NorthwindMVCUygulama.NorthwindDB;
using NorthwindMVCUygulama.SingletonDb;
using NorthwindMVCUygulama.ViewModels;
using System.Linq;

namespace NorthwindMVCUygulama.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {

            List<CustomerListView> customerLists = SingletonContext.Db.Customers.Select(x => new CustomerListView()
            {
                CustomerId = x.CustomerId,
                CompanyName = x.CompanyName,
                Phone = x.Phone,
                City = x.City,

            }).OrderBy(x => x.CustomerId).ToList();
            return View(customerLists);

        }

        public IActionResult Details(string id)
        {
            return View(SingletonContext.Db.Customers.First(x => x.CustomerId==id));  
        }




        // GET: UsersController/Delete/5
        public IActionResult Delete(string id)
        {
            return View(SingletonContext.Db.Customers.FirstOrDefault(x => x.CustomerId == id));
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Customer customer)
        {

            try
            {
                Customer silinecek = SingletonContext.Db.Customers.FirstOrDefault(x => x.CustomerId == customer.CustomerId);
                SingletonContext.Db.Customers.Remove(silinecek);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(customer);
            }
        }




        // GET: UsersController/Edit/5
        public IActionResult Edit(string id)
        {
            Customer customer = SingletonContext.Db.Customers.FirstOrDefault(x => x.CustomerId == id);
            return View(customer);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer customer)
        {
            try
            {
                //eskiyi sil yeniyi ekle
                Customer silinecek = SingletonContext.Db.Customers.FirstOrDefault(x => x.CustomerId == customer.CustomerId);
                SingletonContext.Db.Customers.Remove(silinecek);
                SingletonContext.Db.Customers.Add(customer);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View(customer);
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
        public IActionResult Create(Customer customer)
        {
            try
            {
                //Kontrolleri yap
                if (customer == null)
                {
                    throw new Exception("Boş nesne");
                }
                // yazılan id listede varsa hata ver
                if (SingletonContext.Db.Customers.Where(x => x.CustomerId == customer.CustomerId).Count() > 0)
                {
                    throw new Exception("Bu id daha önce kullanılmış");
                }
                else
                {
                    //Doğruysa listeye ekle
                    SingletonContext.Db.Customers.Add(customer);
                    SingletonContext.Db.SaveChanges();
                    //Yanlışsa hata fırlat
                    //Listeye yönlendir
                    return RedirectToAction(nameof(Index));
                }
             
              

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(customer);      //hatalı veri ile birlikte yine aynı sayfaya yönlendirilir.
            }
        }

    }
        
}





