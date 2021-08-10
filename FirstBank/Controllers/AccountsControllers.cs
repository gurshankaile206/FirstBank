using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FirstBank.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirstBank.Controllers
{
  public class AccountsController : Controller
  {
    private readonly FirstBankContext _db;

    public AccountsController(FirstBankContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Account> model = _db.Accounts.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.CustomerId = new SelectList(_db.Customers, "CustomerId", "CustomerName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Account account)
    {
      _db.Accounts.Add(account);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisAccount = _db.Accounts.FirstOrDefault(account => account.AccountId == id);
      return View(thisAccount);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisAccount = _db.Accounts.FirstOrDefault(account => account.AccountId == id);
      _db.Accounts.Remove(thisAccount);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      var thisAccount = _db.Accounts
      .Include(account => account.JoinEntities)
      .ThenInclude(join => join.Customer)
      .FirstOrDefault(account => account.AccountId == id);
      return View(thisAccount);
    }

    public ActionResult Edit(int id)
    {
      var thisAccount = _db.Accounts.FirstOrDefault(account => account.AccountId == id);
      return View(thisAccount);
    }
    [HttpPost]
    public ActionResult Edit(Account account)
    {
      _db.Entry(account).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}