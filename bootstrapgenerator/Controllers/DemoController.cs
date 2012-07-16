﻿using System.Web.Mvc;

namespace bootstrapgenerator.Controllers
{
	public class DemoController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "Welcome to ASP.NET MVC!";

			return View();
		}

		public ActionResult Example(string id)
		{
			return View(id);
		}
	}
}
