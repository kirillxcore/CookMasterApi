using System;
using System.Collections.Generic;
using System.Text;
using CookFormMaster.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Script.v1;
using Google.Apis.Script.v1.Data;
using Newtonsoft.Json;

namespace CookFormMaster
{
	public class CookFormManager
	{
	    private static volatile CookFormManager _instance;
	    private static readonly object SyncRoot = new Object();

	    private CookFormManager() { }

	    public static CookFormManager Instance
	    {
	        get
	        {
	            if (_instance == null)
	            {
	                lock (SyncRoot)
	                {
	                    if (_instance == null)
	                        _instance = new CookFormManager();
	                }
	            }

	            return _instance;
	        }
	    }

	    public bool IsAuthorized => ScriptService != null;

	    public ScriptService ScriptService;

		public void SetScriptService(UserCredential credential)
		{
			ScriptService = new ScriptService(new BaseClientService.Initializer
			{
				HttpClientInitializer = credential,
				ApplicationName = "ASP.NET MVC Sample"
			});
		}

	    public FormCreationResponse CreateForm(Menu menu, string emails)
	    {
	        var menuJson = JsonConvert.SerializeObject(menu);

	        var request = new ExecutionRequest();
	        request.Function = "createForm";
	        request.Parameters = new List<object> {emails, menuJson};
	        request.DevMode = false;
	        var scriptId = "1s65xtr2aqSWhWRWU4xUT3CPX2S5jqvYhoiO5olF2st-9FuhZO8a9rhi6";

	        var runReq = ScriptService.Scripts.Run(request, scriptId);

	        return JsonConvert.DeserializeObject<FormCreationResponse>(ProcessResult(runReq));
	    }

	    public IList<FormAnswer> GetFormResult(string menuFormId)
	    {
	        var request = new ExecutionRequest();
	        request.Function = "responseForm";
	        request.Parameters = new List<object> {menuFormId};
	        var scriptId = "1s65xtr2aqSWhWRWU4xUT3CPX2S5jqvYhoiO5olF2st-9FuhZO8a9rhi6";

	        var runReq = ScriptService.Scripts.Run(request, scriptId);

	        return JsonConvert.DeserializeObject<IList<FormAnswer>>(ProcessResult(runReq));

	    }

	    private string ProcessResult(ScriptsResource.RunRequest runReq)
	    {
	        var errorMessages = new StringBuilder("");
	        try
	        {
	            var op = runReq.Execute();

	            if (op.Error != null)
	            {
	                IDictionary<string, object> error = op.Error.Details[0];
	                errorMessages.AppendLine($"Script error message: {error["errorMessage"]}");
	                if (error["scriptStackTraceElements"] != null)
	                {
	                    // There may not be a stacktrace if the script didn't
	                    // start executing.
	                    errorMessages.AppendLine("Script error stacktrace:");
	                    Newtonsoft.Json.Linq.JArray st =
	                        (Newtonsoft.Json.Linq.JArray)error["scriptStackTraceElements"];
	                    foreach (var trace in st)
	                    {
	                        errorMessages.AppendLine($"\t{trace["function"]}: {trace["lineNumber"]}");
	                    }
	                }
	            }
	            else
	            {
	               return op.Response["result"].ToString();
	            }
	        }
	        catch (Google.GoogleApiException e)
	        {
	            errorMessages.AppendLine($"Error calling API:\n{e}");
	        }

	        throw new Exception(errorMessages.ToString());
        }
	}
}
