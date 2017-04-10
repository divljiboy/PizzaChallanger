using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Rendered.Controllers
{
    [Produces("application/json")]
    [Route("api/Pizza")]
    public class PizzaController : Controller
    {
        /**
        Link to git repository: https://github.com/divljiboy/PizzaChallanger.git
        **/
        List<string> meat = new List<string> { "ham", "salami", "tuna", "calamari", "crab_meat", "shrimps", "minced_meat", "sausage", "kebab", "minced_beef", "cocktail_sausages" };
        List<string> cheese = new List<string> { "mozzarella_cheese", "parmesan_cheese", "blue_cheese", "goat_cheese", "mozzarella" };
        List<string> olives = new List<string> { "green_olives", "black_olives", "olives" };
        List<Pizza> group_1 = new List<Pizza>();
        List<Pizza> group_2 = new List<Pizza>();
        List<Pizza> group_3 = new List<Pizza>();
        List<Pizza> group_4 = new List<Pizza>();

        /**
         Class: Pizza
         Attributes:
            name : string,
            ingredients: List<string>,
            price : double

         */
        Pizza p;
        List<Dictionary<string, JToken>> dict = new List<Dictionary<string, JToken>>();

        double size;
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] JObject stuff)
        {
            size = 0;
            dict = stuff["pizzas"].ToObject<List<Dictionary<string, JToken>>>();
            foreach (Dictionary<string, JToken> item in dict)
            {
                if (item.Count == 2)
                {
                    try
                    {
                        //p = new Pizza(item.Keys.First(), (double)item.Values.Last(),item.Values.First()["ingredients"].ToObject<List<string>>());
                        p = new Pizza();
                        p.ingredients = item.Values.First()["ingredients"].ToObject<List<string>>();
                        p.name = item.Keys.First();
                        p.price = (double)item.Values.Last();
                        GroupPizza(p);
                   
                    }
                    catch
                    {
                        return Json("Failed");
                    }
                }

            }
            dynamic d = new JObject();
            d.personal_info = new JObject(new JProperty("full_name", "Ivan Divljak"),new JProperty("email","ivan93.ns@hotmail.com"),new JProperty("code_link","blabla"));
             dynamic g1 = new JProperty("group_1",new JObject(new JProperty("percentage", (group_1.Count / size*100).ToString() + "%"), new JProperty("cheapest", JToken.FromObject(group_1.OrderBy(x=>x.price).FirstOrDefault()))));
             dynamic g2 = new JProperty("group_2", new JObject(new JProperty("percentage", (group_2.Count / size*100).ToString() + "%"), new JProperty("cheapest", JToken.FromObject(group_2.OrderBy(x => x.price).FirstOrDefault()))));
             dynamic g3 = new JProperty("group_3", new JObject(new JProperty("percentage", (group_3.Count / size * 100).ToString() + "%"), new JProperty("cheapest", JToken.FromObject(group_3.OrderBy(x => x.price).FirstOrDefault()))));
             dynamic g4 = new JProperty("group_4", new JObject(new JProperty("percentage", (group_4.Count / size * 100).ToString() + "%"), new JProperty("cheapest", JToken.FromObject(group_4.OrderBy(x => x.price).FirstOrDefault()))));
            d.answers = new JArray(new JObject(g1, g2, g3, g4));
           return Json(d);
        }

        private void GroupPizza(Pizza p)
        {

            if (meat.Any(m => p.ingredients.Contains(m)))
            {
                group_1.Add(p);
            }
            if (cheese.Intersect(p.ingredients).Count() >= 2)
            {
                group_2.Add(p);
            }
            if (meat.Any(m => p.ingredients.Contains(m)) && olives.Any(x => p.ingredients.Contains(x)))
            {
                group_3.Add(p);
            }
            if (p.ingredients.Contains("mushrooms") && p.ingredients.Contains("mozzarella_cheese"))
            {
                group_4.Add(p);
            }
            size++;

        }


    }
}