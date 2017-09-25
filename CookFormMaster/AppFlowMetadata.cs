using System;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Util.Store;

namespace CookFormMaster
{
    public class AppFlowMetadata : FlowMetadata
    {

        static string[] Scopes = { "https://www.googleapis.com/auth/forms", "https://www.googleapis.com/auth/script.external_request", "https://www.googleapis.com/auth/script.send_mail" };
        static string ApplicationName = "Google Apps Script Execution API";


        private static readonly IAuthorizationCodeFlow flow =
            new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "224579463826-1ggal5bqaiakm04fpdugmh32s0eodvrq.apps.googleusercontent.com",
                    ClientSecret = "D41d0xxdcnFB7kESJQ8685sb"
                },
                Scopes = Scopes,
                DataStore = new FileDataStore("Script.Api.Auth.Store")
            });

        public override string GetUserId(Controller controller)
        {
            // In this sample we use the session to store the user identifiers.
            // That's not the best practice, because you should have a logic to identify
            // a user. You might want to use "OpenID Connect".
            // You can read more about the protocol in the following link:
            // https://developers.google.com/accounts/docs/OAuth2Login.
            var user = controller.Session["user"];
            if (user == null)
            {
                user = Guid.NewGuid();
                controller.Session["user"] = user;
            }
            return user.ToString();

        }

        public override IAuthorizationCodeFlow Flow
        {
            get { return flow; }
        }
    }
}