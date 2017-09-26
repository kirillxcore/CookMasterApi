using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using CookFormMaster;
using Google.Apis.Auth.OAuth2.Mvc;

namespace CookMasterApi.Controllers
{
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
            var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata()).
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
