using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Web;
using Google.Apis.Services;
using Google.Apis.Script.v1;
using Google.Apis.Script.v1.Data;
using Google.Apis.Util.Store;

namespace CookFormMaster
{
    public class CookFormManager
    {
        static string[] Scopes = { "https://www.googleapis.com/auth/forms", "https://www.googleapis.com/auth/script.external_request", "https://www.googleapis.com/auth/script.send_mail" };
        static string ApplicationName = "Google Apps Script Execution API";

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public string Test()
        {
            StringBuilder result = new StringBuilder("");

            String serviceAccountEmail = "cookmaster@project-id-3437069574526953875.iam.gserviceaccount.com";

            var certificate = new X509Certificate2(@"C:\projects\my\CookMasterApi\cook.p12", "notasecret", X509KeyStorageFlags.Exportable);

            ServiceAccountCredential credential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(serviceAccountEmail)
                {
                    Scopes = Scopes
                }.FromCertificate(certificate));


            var service = new ScriptService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

            var email = "dmitry.aliskerov@gmail.com, kirillxcore@mail.ru";
            var configuration = @"
{
  ""title"": ""Меню на выбор"",
  ""discription"": ""Повар: Иванова Т.И."",
  ""days"": [
    {
      ""title"": ""Понедельник, 17.09.2017"",
      ""catigories"": [
        {
          ""title"": ""Супы"",
          ""dishes"": [
            {
              ""title"": ""Куриный суп с яйцом"",
              ""discription"": ""Быстрый и вкусный куриный суп с яйцом. Прекрасно подойдет к обеденному столу."",
              ""image"": ""https://kedem.ru/photo/recipe/rnamebig/kurinyj-sup-s-yajcom.jpg""
			},
            {
              ""title"": ""Грибной суп"",
              ""discription"": ""Ароматный, вкусный и легкий грибной суп приятно разнообразит ваше повседневное меню."",
              ""image"": ""https://kedem.ru/photo/recipe/rnamebig/gribnoj-sup.jpg""
			}
          ]
        },
        {
          ""title"": ""Второе"",
          ""dishes"": [
            {
              ""title"": ""Картофель тушеный с белыми грибами"",
              ""discription"": ""Гадаете, что бы такого вкусного приготовить? Тогда картофель тушеный с белыми грибами как раз для вас."",
              ""image"": ""https://kedem.ru/photo/recipe/rnamebig/kartofel-tushenyj-s-belymi-gribami.jpg""
            },
            {
              ""title"": ""Картофельные драники 'Улётные'"",
              ""discription"": ""Одно из самых популярных блюд, которое любят в каждой семье. Румяные, с золотистой корочкой картофельные драники со сметанкой... Ммм..."",
              ""image"": ""https://kedem.ru/photo/recipe/rnamebig/kartofelnye-draniki-ulyotnye.jpg""
            }
          ]
        },
        {
          ""title"": ""Салат"",
          ""dishes"": [
            {
              ""title"": ""Салат с маринованными грибами 'Венгерка'"",
              ""discription"": ""Салат с маринованными грибами «Венгерка», это очаровательный салат с грибами и маслинами, который понравится вашим гостям."",
              ""image"": ""https://kedem.ru/photo/recipe/rnamebig/salat-s-marinovannymi-gribami-vengerka.jpg""
            },
            {
              ""title"": ""Закуска 'Оливье-рулет'"",
              ""discription"": ""'Оливье - рулет' прекрасная закуска-салат для новогоднего стола, точно не останется до конца праздника."",
              ""image"": ""https://kedem.ru/photo/recipe/rnamebig/zakuska-olive-rulet.jpg""
            }
          ]
        }
      ]
    },
    {
      ""title"": ""Вторник, 18.09.2017"",
      ""catigories"": [
        {
          ""title"": ""Супы"",
          ""dishes"": [
            {
              ""title"": ""Тыквенный суп-пюре"",
              ""discription"": ""Самый быстрый и вкусный тыквенный суп-пюре с овощами на курином бульоне."",
              ""image"": ""https://kedem.ru/photo/recipe/rnamebig/tykvennyj-sup-pyure-za-10-minut.jpg""
            },
            {
              ""title"": ""Свекольник домашний"",
              ""discription"": ""Свекольник домашний, аппетитный и вкусный постный суп, который можно готовить хоть каждый день."",
              ""image"": ""https://kedem.ru/photo/recipe/rnamebig/svekolnik-domashnij.jpg""
            }
          ]
        },
        {
          ""title"": ""Второе"",
          ""dishes"": [
            {
              ""title"": ""Тушеное мясо в горшочке"",
              ""discription"": ""Нежное тушеное мясо в горшочке, пропитанное ароматами овощей и приправ, порадует своим вкусом вас и ваших близких."",
              ""image"": ""https://kedem.ru/photo/recipe/rnamebig/tushenoe-myaso-v-gorshochke.jpg""
            },
            {
              ""title"": ""Перец в духовке с сыром и яйцом"",
              ""discription"": ""Необычный рецепт фаршированного перца с сыром и яйцом, запеченного в духовке отлично подойдет как для вкусного завтрака, так и для легкого ужина. Простой в приготовлении, он займет совсем немного времени."",
              ""image"": ""https://kedem.ru/photo/recipe/rnamebig/perec-v-duhovke-s-syrom-i-yajcom.jpg""
            }
          ]
        },
        {
          ""title"": ""Выпечка"",
          ""dishes"": [
            {
              ""title"": ""Шарлотка с яблоками"",
              ""discription"": ""Хотите наполнить дом теплом и уютом? Тогда самое время приготовить Шарлотку! Шарлотка из зеленых сочных яблок не оставит равнодушным никого."",
              ""image"": ""https://kedem.ru/photo/recipe/rnamebig/sharlotka-s-yablokami.jpg""
            },
            {
              ""title"": ""Мини-пиццы"",
              ""discription"": ""Эти маленькие, аппетитные и очень вкусные мини-пиццы станут хитом в любой компании. Лучше приготовить побольше."",
              ""image"": ""https://kedem.ru/photo/recipe/rnamebig/mini-piccy.jpg""
            }
          ]
        }
      ]
    }
  ],
  ""wishes"": [
    ""Больше мяса"", ""Больше сладкого""
  ]
}";

            // Create an execution request object.
            ExecutionRequest request = new ExecutionRequest();
            request.Function = "createForm";
            request.Parameters = new List<object> { email, configuration };
            request.DevMode = false;
            //			request.Function = "responseForm";
            //			request.Parameters = new List<object> { "1JE3w1CZDUfqocGX5k-opTSA07Ykr52o1cI5pFezi-54" };

            string scriptId = "1Z5fyqL4fmoPiCVHUB08Y1ZF39yTdgFMahiBagmraiT7QjZILcUUwo3t-";

            ScriptsResource.RunRequest runReq =
                    service.Scripts.Run(request, scriptId);

            try
            {
                // Make the API request.
                Operation op = runReq.Execute();

                if (op.Error != null)
                {
                    // The API executed, but the script returned an error.

                    // Extract the first (and only) set of error details
                    // as a IDictionary. The values of this dictionary are
                    // the script's 'errorMessage' and 'errorType', and an
                    // array of stack trace elements. Casting the array as
                    // a JSON JArray allows the trace elements to be accessed
                    // directly.
                    IDictionary<string, object> error = op.Error.Details[0];
                   result.AppendLine($"Script error message: {error["errorMessage"]}");
                    if (error["scriptStackTraceElements"] != null)
                    {
                        // There may not be a stacktrace if the script didn't
                        // start executing.
                        result.AppendLine("Script error stacktrace:");
                        Newtonsoft.Json.Linq.JArray st =
                            (Newtonsoft.Json.Linq.JArray)error["scriptStackTraceElements"];
                        foreach (var trace in st)
                        {
                            result.AppendLine($"\t{trace["function"]}: {trace["lineNumber"]}");
                        }
                    }
                }
                else
                {
                    // The result provided by the API needs to be cast into
                    // the correct type, based upon what types the Apps
                    // Script function returns. Here, the function returns
                    // an Apps Script Object with String keys and values.
                    // It is most convenient to cast the return value as a JSON
                    // JObject (folderSet).
                    var resp = op.Response["result"];

                   result.AppendLine("Result: " + resp);
                }
            }
            catch (Google.GoogleApiException e)
            {
                // The API encountered a problem before the script
                // started executing.
                result.AppendLine($"Error calling API:\n{e}");
            }
            //Console.Read();

            return result.ToString();
        }
    }
}
