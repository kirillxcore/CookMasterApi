﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CookFormMaster;
using Google.Apis.Auth.OAuth2.Mvc;

namespace CookMasterApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public async Task<ActionResult> IndexAsync(CancellationToken cancellationToken)
        {
            var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata()).
                AuthorizeAsync(cancellationToken);

            if (result.Credential != null)
            {
                CookFormManager cookFormManager = new CookFormManager();
                cookFormManager.SetScriptService(result.Credential);

                cookFormManager.Test();

                return View("Index");
            }
            else
            {
                return new RedirectResult(result.RedirectUri);
            }
        }
    }
}
