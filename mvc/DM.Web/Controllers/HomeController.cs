using DM.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DM.Web.Controllers
{
    public class HomeController : BaseController
    {
        public string Test;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult List()
        {
            ViewBag.Title = "数据列表展示";

            return View();
        }

        public ActionResult ListResult()
        {
            var list = new ArrayList() {
                new {id = 1,name = "方便面",price = 4.5 },
                new {id = 2,name = "烤肠",price = 1.5 },
                new {id = 3,name = "鸡蛋",price = 1.5 }
            };
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            var model = new Test()
            {
                Id = 1,
                Name = "方便面",
                Price = 4.5M
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Test model)
        {
            if (ModelState.IsValid)
            {
                model.Name = "测试修改";
            }
            return View(model);
        }
    }
}