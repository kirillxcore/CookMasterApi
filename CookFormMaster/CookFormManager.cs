using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using CookFormMaster.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Script.v1;
using Google.Apis.Script.v1.Data;
using Google.Apis.Util.Store;
using Newtonsoft.Json;

namespace CookFormMaster
{
	public class CookFormManager
	{
		static string ApplicationName = "Google Apps Script Execution API";

		public ScriptService ScriptService = null;

		public void SetScriptService(UserCredential credential)
		{
			ScriptService = new ScriptService(new BaseClientService.Initializer
			{
				HttpClientInitializer = credential,
				ApplicationName = "ASP.NET MVC Sample"
			});
		}

		public string Test()
		{
			var result = new StringBuilder("");

			var email = "cookmaster2018@gmail.com";

			var menu = new Menu
			{
				title = "Меню на выбор",
				description = "Повар: Иванова Т.И.",
				days = new List<MenuDay>
				{
					new MenuDay
					{
						title = "Понедельник, 17.09.2017",
						categories = new List<Category>
						{
							new Category
							{
								title = "Супы",
								dishes = new List<Dish>
								{
									new Dish
									{
										title = "Куриный суп с яйцом",
										description = "Быстрый и вкусный куриный суп с яйцом. Прекрасно подойдет к обеденному столу.",
										image = "https://kedem.ru/photo/recipe/rnamebig/kurinyj-sup-s-yajcom.jpg"
									},
									new Dish
									{
										title = "Грибной суп",
										description = "Ароматный, вкусный и легкий грибной суп приятно разнообразит ваше повседневное меню.",
										image = "https://kedem.ru/photo/recipe/rnamebig/gribnoj-sup.jpg"
									}
								}
							},
							new Category
							{
								title = "Второе",
								dishes = new List<Dish>
								{
									new Dish
									{
										title = "Картофель тушеный с белыми грибами",
										description = "Гадаете, что бы такого вкусного приготовить? Тогда картофель тушеный с белыми грибами как раз для вас.",
										image = "https://kedem.ru/photo/recipe/rnamebig/kartofel-tushenyj-s-belymi-gribami.jpg"
									},
									new Dish
									{
										title = "Картофельные драники 'Улётные'",
										description = "Одно из самых популярных блюд, которое любят в каждой семье. Румяные, с золотистой корочкой картофельные драники со сметанкой... Ммм...",
										image = "https://kedem.ru/photo/recipe/rnamebig/kartofelnye-draniki-ulyotnye.jpg"
									}
								}
							},
							new Category
							{
								title = "Салат",
								dishes = new List<Dish>
								{
									new Dish
									{
										title = "Салат с маринованными грибами 'Венгерка'",
										description = "Салат с маринованными грибами «Венгерка», это очаровательный салат с грибами и маслинами, который понравится вашим гостям.",
										image = "https://kedem.ru/photo/recipe/rnamebig/salat-s-marinovannymi-gribami-vengerka.jpg"
									},
									new Dish
									{
										title = "Закуска 'Оливье-рулет'",
										description = "'Оливье - рулет' прекрасная закуска-салат для новогоднего стола, точно не останется до конца праздника.",
										image = "https://kedem.ru/photo/recipe/rnamebig/zakuska-olive-rulet.jpg"
									}
								}
							}
						}
					},
					new MenuDay
					{
						title = "Вторник, 18.09.2017",
						categories = new List<Category>
						{
							new Category
							{
								title = "Супы",
								dishes = new List<Dish>
								{
									new Dish
									{
										title ="Тыквенный суп-пюре",
										description = "Самый быстрый и вкусный тыквенный суп-пюре с овощами на курином бульоне.",
										image = "https://kedem.ru/photo/recipe/rnamebig/tykvennyj-sup-pyure-za-10-minut.jpg"
									},
									new Dish
									{
										title = "Свекольник домашний",
										description = "Свекольник домашний, аппетитный и вкусный постный суп, который можно готовить хоть каждый день.",
										image = "https://kedem.ru/photo/recipe/rnamebig/svekolnik-domashnij.jpg"
									}
								}
							},
							new Category
							{
								title = "Второе",
								dishes = new List<Dish>
								{
									new Dish
									{
										title = "Тушеное мясо в горшочке",
										description = "Нежное тушеное мясо в горшочке, пропитанное ароматами овощей и приправ, порадует своим вкусом вас и ваших близких.",
										image = "https://kedem.ru/photo/recipe/rnamebig/tushenoe-myaso-v-gorshochke.jpg"
									},
									new Dish
									{
										title = "Перец в духовке с сыром и яйцом",
										description = "Необычный рецепт фаршированного перца с сыром и яйцом, запеченного в духовке отлично подойдет как для вкусного завтрака, так и для легкого ужина.Простой в приготовлении, он займет совсем немного времени.",
										image = "https://kedem.ru/photo/recipe/rnamebig/perec-v-duhovke-s-syrom-i-yajcom.jpg"
									}
								}
							},
							new Category
							{
								title = "Выпечка",
								dishes = new List<Dish>
								{
									new Dish
									{
										title = "Шарлотка с яблоками",
										description = "Хотите наполнить дом теплом и уютом? Тогда самое время приготовить Шарлотку! Шарлотка из зеленых сочных яблок не оставит равнодушным никого.",
										image = "https://kedem.ru/photo/recipe/rnamebig/sharlotka-s-yablokami.jpg"
									},
									new Dish
									{
										title = "Мини-пиццы",
										description = "Эти маленькие, аппетитные и очень вкусные мини-пиццы станут хитом в любой компании.Лучше приготовить побольше.",
										image = "https://kedem.ru/photo/recipe/rnamebig/mini-piccy.jpg"
									}
								}
							}
						}
					}
				},
				wishes = new Wishes
				{
					title = "Пожелания",
					values = new List<string>
					{
						"Больше мяса",
						"Больше сладкого"
					}
				}
			};

			var menuJson = JsonConvert.SerializeObject(menu);

			var request = new ExecutionRequest();
			request.Function = "createForm";
			request.Parameters = new List<object> { email, menuJson };
			request.DevMode = false;
			//			request.Function = "responseForm";
			//			request.Parameters = new List<object> { "1JE3w1CZDUfqocGX5k-opTSA07Ykr52o1cI5pFezi-54" };

			string scriptId = "1s65xtr2aqSWhWRWU4xUT3CPX2S5jqvYhoiO5olF2st-9FuhZO8a9rhi6";

			var runReq = ScriptService.Scripts.Run(request, scriptId);

			try
			{
				Operation op = runReq.Execute();

				if (op.Error != null)
				{
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
					var resp = op.Response["result"];

					result.AppendLine("Result: " + resp);
				}
			}
			catch (Google.GoogleApiException e)
			{
				result.AppendLine($"Error calling API:\n{e}");
			}

			return result.ToString();
		}
	}
}
