using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSQ.GiftEntertainmentTracker.Controllers
{
    public class SuperAdminController : Controller
    {
        //
        // GET: /SuperAdmin/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /SuperAdmin/Details/5

        public ActionResult Details()
        {
            return View();
        }

        //
        // GET: /SuperAdmin/Create

        public ActionResult Create(int companyId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = Common.ListItem.division, Value = Common.ObjectTypeCode.Divison });
            items.Add(new SelectListItem { Text = Common.ListItem.department, Value = Common.ObjectTypeCode.Department });
            ViewBag.companyItems = items;
            Session["companyId"] = companyId;
            return View();
        } 

        //
        // POST: /SuperAdmin/Create

        [HttpPost]
        public ActionResult Create(string companyItems)
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            string action = null, route = null;
            if (companyItems == Common.ObjectTypeCode.Divison)
            {
                action = "CreateDivision";
                route = "Division";
                goto send;
            }
            else if (companyItems == Common.ObjectTypeCode.Department)
            {
                action = "CreateDepartment";
                route = "Department";
                goto send;
            }
        send:
            return RedirectToAction(action, route, new { objectId = companyId, objectTypeCode = Common.ObjectTypeCode.Company });
        }
        
        //
        // GET: /SuperAdmin/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SuperAdmin/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SuperAdmin/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SuperAdmin/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
