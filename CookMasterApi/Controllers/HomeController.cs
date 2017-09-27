using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using CookFormMaster;
using Google.Apis.Auth.OAuth2.Mvc;

namespace CookMasterApi.Controllers
{
	public class HomeController : AsyncController
    {
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata()).
                AuthorizeAsync(cancellationToken);

			if (result.Credential != null)
            {
                CookFormManager.Instance.SetScriptService(result.Credential);

	            return View("Index");
			}
            else
            {
                return new RedirectResult(result.RedirectUri);
            }
        }


    }
}
