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
				Title = "Меню на выбор",
				Description = "Повар: Иванова Т.И.",
				Days = new List<MenuDay>
				{
					new MenuDay
					{
						Title = "Понедельник, 17.09.2017",
						Categories = new List<Category>
						{
							new Category
							{
								Title = "Супы",
								Dishes = new List<Dish>
								{
									new Dish
									{
										Id = 1,
										Title = "Куриный суп с яйцом",
										Description = "Быстрый и вкусный куриный суп с яйцом. Прекрасно подойдет к обеденному столу.",
										Image = "https://kedem.ru/photo/recipe/rnamebig/kurinyj-sup-s-yajcom.jpg"
									},
									new Dish
									{
										Id = 4,
										Title = "Грибной суп",
										Description = "Ароматный, вкусный и легкий грибной суп приятно разнообразит ваше повседневное меню.",
										Image = "https://kedem.ru/photo/recipe/rnamebig/gribnoj-sup.jpg"
									}
								}
							},
							new Category
							{
								Title = "Второе",
								Dishes = new List<Dish>
								{
									new Dish
									{
										Id = 7,
										Title = "Картофель тушеный с белыми грибами",
										Description = "Гадаете, что бы такого вкусного приготовить? Тогда картофель тушеный с белыми грибами как раз для вас.",
										Image = "https://kedem.ru/photo/recipe/rnamebig/kartofel-tushenyj-s-belymi-gribami.jpg"
									},
									new Dish
									{
										Id = 8,
										Title = "Картофельные драники 'Улётные'",
										Description = "Одно из самых популярных блюд, которое любят в каждой семье. Румяные, с золотистой корочкой картофельные драники со сметанкой... Ммм...",
										Image = "https://kedem.ru/photo/recipe/rnamebig/kartofelnye-draniki-ulyotnye.jpg"
									}
								}
							},
							new Category
							{
								Title = "Салат",
								Dishes = new List<Dish>
								{
									new Dish
									{
										Id = 15,
										Title = "Салат с маринованными грибами 'Венгерка'",
										Description = "Салат с маринованными грибами «Венгерка», это очаровательный салат с грибами и маслинами, который понравится вашим гостям.",
										Image = "https://kedem.ru/photo/recipe/rnamebig/salat-s-marinovannymi-gribami-vengerka.jpg"
									},
									new Dish
									{
										Id = 16,
										Title = "Закуска 'Оливье-рулет'",
										Description = "'Оливье - рулет' прекрасная закуска-салат для новогоднего стола, точно не останется до конца праздника.",
										Image = "https://kedem.ru/photo/recipe/rnamebig/zakuska-olive-rulet.jpg"
									}
								}
							}
						}
					},
					new MenuDay
					{
						Title = "Вторник, 18.09.2017",
						Categories = new List<Category>
						{
							new Category
							{
								Title = "Супы",
								Dishes = new List<Dish>
								{
									new Dish
									{
										Id = 5,
										Title ="Тыквенный суп-пюре",
										Description = "Самый быстрый и вкусный тыквенный суп-пюре с овощами на курином бульоне.",
										Image = "https://kedem.ru/photo/recipe/rnamebig/tykvennyj-sup-pyure-za-10-minut.jpg"
									},
									new Dish
									{
										Id = 6,
										Title = "Свекольник домашний",
										Description = "Свекольник домашний, аппетитный и вкусный постный суп, который можно готовить хоть каждый день.",
										Image = "https://kedem.ru/photo/recipe/rnamebig/svekolnik-domashnij.jpg"
									}
								}
							},
							new Category
							{
								Title = "Второе",
								Dishes = new List<Dish>
								{
									new Dish
									{
										Id = 9,
										Title = "Тушеное мясо в горшочке",
										Description = "Нежное тушеное мясо в горшочке, пропитанное ароматами овощей и приправ, порадует своим вкусом вас и ваших близких.",
										Image = "https://kedem.ru/photo/recipe/rnamebig/tushenoe-myaso-v-gorshochke.jpg"
									},
									new Dish
									{
										Id = 10,
										Title = "Перец в духовке с сыром и яйцом",
										Description = "Необычный рецепт фаршированного перца с сыром и яйцом, запеченного в духовке отлично подойдет как для вкусного завтрака, так и для легкого ужина.Простой в приготовлении, он займет совсем немного времени.",
										Image = "https://kedem.ru/photo/recipe/rnamebig/perec-v-duhovke-s-syrom-i-yajcom.jpg"
									}
								}
							},
							new Category
							{
								Title = "Выпечка",
								Dishes = new List<Dish>
								{
									new Dish
									{
										Id = 11,
										Title = "Шарлотка с яблоками",
										Description = "Хотите наполнить дом теплом и уютом? Тогда самое время приготовить Шарлотку! Шарлотка из зеленых сочных яблок не оставит равнодушным никого.",
										Image = "https://kedem.ru/photo/recipe/rnamebig/sharlotka-s-yablokami.jpg"
									},
									new Dish
									{
										Id = 13,
										Title = "Мини-пиццы",
										Description = "Эти маленькие, аппетитные и очень вкусные мини-пиццы станут хитом в любой компании.Лучше приготовить побольше.",
										Image = "https://kedem.ru/photo/recipe/rnamebig/mini-piccy.jpg"
									}
								}
							}
						}
					}
				},
				Wishes = new Wishes
				{
					Title = "Пожелания",
					Values = new List<string>
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
//			request.Parameters = new List<object> { "1orZ4eKcRjikCGDl5Ms0FwiUSA8WqOVqTNaBrYPA58ec" };

			var scriptId = "1s65xtr2aqSWhWRWU4xUT3CPX2S5jqvYhoiO5olF2st-9FuhZO8a9rhi6";

			var runReq = ScriptService.Scripts.Run(request, scriptId);

			try
			{
				var op = runReq.Execute();

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
					var resultJson = op.Response["result"].ToString();

					var formCreationResponse = JsonConvert.DeserializeObject<FormCreationResponse>(resultJson);
//					var formAnswers = JsonConvert.DeserializeObject<IList<FormAnswer>>(resultJson);
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
