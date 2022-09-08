using CosmosStorage.Dao;
using CosmosStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CosmosStorage.Controllers
{
    public class ItemsController : Controller
    {
        private static readonly ICosmosDbService service = CosmosDbServiceProvider.CosmosDbService;
        // GET: Items
        public async Task<ActionResult> Index()
        {
            return View(await service.GetItemsAsync("SELECT * FROM Item"));
        }

        // GET: Items/Details/5
        public async Task<ActionResult> Details(string id)
        {
            return await ShowItem(id);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id, Name, Description, Completed")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid().ToString();
                await service.AddItemAsync(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            return await ShowItem(id);
        }

        private async Task<ActionResult> ShowItem(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var item = await service.GetItemAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Name, Description, Completed")] Item item)
        {
            if (ModelState.IsValid)
            {
                await service.UpdateItemAsync(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            return await ShowItem(id);
        }

        // POST: Items/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete([Bind(Include = "Id, Name, Description, Completed")] Item item)
        {
            await service.DeleteItemAsync(item);
            return RedirectToAction("Index");
        }
    }
}
