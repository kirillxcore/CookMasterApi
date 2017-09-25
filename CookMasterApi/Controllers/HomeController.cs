using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CookFormMaster;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Auth.OAuth2.Web;

namespace CookMasterApi.Controllers
{
	public class MyAuthorizationCodeMvcApp : AuthorizationCodeWebApp
	{
		private readonly Controller controller;
		private readonly FlowMetadata flowData;

		/// <summary>Gets the controller which is the owner of this authorization code MVC app instance.</summary>
		public Controller Controller
		{
			get
			{
				return this.controller;
			}
		}

		/// <summary>Gets the <see cref="T:Google.Apis.Auth.OAuth2.Mvc.FlowMetadata" /> object.</summary>
		public FlowMetadata FlowData
		{
			get
			{
				return this.flowData;
			}
		}

		/// <summary>Constructs a new authorization code MVC app using the given controller and flow data.</summary>
		public MyAuthorizationCodeMvcApp(Controller controller, FlowMetadata flowData)
			: base(flowData.Flow, "http://cookmaster.apphb.com/AuthCallback/IndexAsync", controller.Request.Url.ToString())
		{
			this.controller = controller;
			this.flowData = flowData;
		}

		/// <summary>
		/// Asynchronously authorizes the installed application to access user's protected data. It gets the user
		/// identifier by calling to <see cref="M:Google.Apis.Auth.OAuth2.Mvc.FlowMetadata.GetUserId(System.Web.Mvc.Controller)" /> and then calls to
		/// <see cref="M:Google.Apis.Auth.OAuth2.Web.AuthorizationCodeWebApp.AuthorizeAsync(System.String,System.Threading.CancellationToken)" />.
		/// </summary>
		/// <param name="taskCancellationToken">Cancellation token to cancel an operation</param>
		/// <returns>
		/// Auth result object which contains the user's credential or redirect URI for the authorization server
		/// </returns>
		public Task<AuthorizationCodeWebApp.AuthResult> AuthorizeAsync(CancellationToken taskCancellationToken)
		{
			return this.AuthorizeAsync(this.FlowData.GetUserId(this.Controller), taskCancellationToken);
		}
	}


	public class HomeController : AsyncController
    {
     /*   public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
*/
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await new MyAuthorizationCodeMvcApp(this, new AppFlowMetadata()).
                AuthorizeAsync(cancellationToken);

			if (result.Credential != null)
            {
                CookFormManager cookFormManager = new CookFormManager();
                cookFormManager.SetScriptService(result.Credential);


				ViewBag.Message =  cookFormManager.Test();

	            return View("Index");
			}
            else
            {
                return new RedirectResult(result.RedirectUri);
            }
        }
    }
}
