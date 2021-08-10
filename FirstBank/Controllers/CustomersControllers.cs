using Microsoft.AspNetCore.Mvc;
using FirstBank.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FirstBank.Controllers
{
  public class CustomersController : Controller
  {
    private readonly FirstBankContext _db;
    public CustomersController(FirstBankContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      return View(_db.Customers.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.AccountId = new SelectList(_db.Accounts, "AccountId", "AccountName");
      return View();
    }
    [HttpPost]
    public ActionResult Create(Customer customer, int AccountId)
    {
      _db.Customers.Add(customer);
      _db.SaveChanges();
      if (AccountId != 0)
      {
        _db.CustomerAccount.Add(new CustomerAccount() { AccountId = AccountId, CustomerId = customer.CustomerId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      var thisCustomer = _db.Customers.FirstOrDefault(customer => customer.CustomerId == id);
      return View(thisCustomer);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCustomer = _db.Customers.FirstOrDefault(customer => customer.CustomerId == id);
      _db.Customers.Remove(thisCustomer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisCustomer = _db.Customers
      .Include(customer => customer.JoinEntities)
      .ThenInclude(join => join.Account)
      .FirstOrDefault(customer => customer.CustomerId == id);
      return View(thisCustomer);
    }
    public ActionResult Edit(int id)
    {
      var thisCustomer = _db.Customers.FirstOrDefault(customer => customer.CustomerId == id);
      ViewBag.AccountId = new SelectList(_db.Accounts, "AccountId", "AccountName");
      return View(thisCustomer);
    }
    [HttpPost]
    public ActionResult Edit(Customer customer, int AccountId)
    {
      if (AccountId != 0)
      {
        _db.CustomerAccount.Add(new CustomerAccount() { AccountId = AccountId, CustomerId = customer.CustomerId });
      }
      _db.Entry(customer).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult AddAccount(int id)
    {
      var thisCustomer = _db.Customers.FirstOrDefault(account => account.CustomerId == id);
      ViewBag.AccountId = new SelectList(_db.Accounts, "AccountId", "AccountName");
      return View(thisCustomer);
    }
    [HttpPost]
    public ActionResult AddAccount(Customer account, int AccountId)
    {
      if (AccountId != 0)
      {
        _db.CustomerAccount.Add(new CustomerAccount() { AccountId = AccountId, CustomerId = account.CustomerId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteAccount(int joinId)
    {
      var joinEntry = _db.CustomerAccount.FirstOrDefault(entry => entry.CustomerAccountId == joinId);
      _db.CustomerAccount.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}