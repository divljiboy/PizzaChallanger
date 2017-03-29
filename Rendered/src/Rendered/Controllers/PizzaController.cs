using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Rendered.Controllers
{
    [Produces("application/json")]
    [Route("api/Pizza")]
    public class PizzaController : Controller
    {

        List<string> meat = new List<string> { "ham", "salami", "tuna", "calamari", "crab_meat", "shrimps", "minced_meat", "sausage", "kebab", "minced_beef", "cocktail_sausages" };
        List<string> cheese = new List<string> { "mozzarella_cheese", "parmesan_cheese", "blue_cheese", "goat_cheese", "mozzarella" };
        List<string> olives = new List<string> { "green_olives", "black_olives", "olives" };
        List<Pizza> group_1 = new List<Pizza>();
        List<Pizza> group_2 = new List<Pizza>();
        List<Pizza> group_3 = new List<Pizza>();
        List<Pizza> group_4 = new List<Pizza>();
        Pizza p = new Pizza();
        string key;
        JToken value;
        List<Dictionary<string, JToken>> dict = new List<Dictionary<string, JToken>>();
        
        double size = 0;
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] JObject stuff)
        {
            dict = stuff["pizzas"].ToObject<List<Dictionary<string, JToken>>>();

            foreach (var item in dict) {
                if (item.Count == 2)
                {
                    p = new Pizza();
                    foreach (KeyValuePair<string, JToken> pair in item)
                    {
                        if (!pair.Key.Equals("nil"))
                        {
                            key = pair.Key;
                            value = pair.Value;
                            if (key.Equals("price"))
                            {
                                p.price = (int)value;
                                size++;
                                GroupPizza(p);
                            }
                            else
                            {
                                p.ingredients = value["ingredients"].ToObject<List<string>>();
                                p.name = key;
                            }

                        }
                    }
                }
            }

            group_1= group_1.OrderBy(o => o.price).ToList();
            group_2=group_2.OrderBy(o => o.price).ToList();
            group_3=group_3.OrderBy(o => o.price).ToList();
            group_4=group_4.OrderBy(o => o.price).ToList();


            dynamic d = new JObject();
            d.personal_info = new JObject(new JProperty("full_name", "Ivan Divljak"),new JProperty("email","ivan93.ns@hotmail.com"),new JProperty("code_link","blabla"));
             dynamic g1 = new JProperty("group_1",new JObject(new JProperty("percentage", (group_1.Count / size*100).ToString() + "%"), new JProperty("cheapest", JToken.FromObject(group_1[0]))));
             dynamic g2 = new JProperty("group_2", new JObject(new JProperty("percentage", (group_2.Count / size*100).ToString() + "%"), new JProperty("cheapest", JToken.FromObject(group_2[0]))));
             dynamic g3 = new JProperty("group_3", new JObject(new JProperty("percentage", (group_3.Count / size * 100).ToString() + "%"), new JProperty("cheapest", JToken.FromObject(group_3[0]))));
             dynamic g4 = new JProperty("group_4", new JObject(new JProperty("percentage", (group_4.Count / size * 100).ToString() + "%"), new JProperty("cheapest", JToken.FromObject(group_4[0]))));
            d.answers = new JArray(new JObject(g1, g2, g3, g4));
           
    

            return Json(d);
        }

        private void GroupPizza(Pizza p)
        {

            if (meat.Any(m=>p.ingredients.Contains(m))) {
                group_1.Add(p);
            }
            if (cheese.Intersect(p.ingredients).Count() >=2) {
                group_2.Add(p);
            }
            if (meat.Any(m=>p.ingredients.Contains(m)) && olives.Any(x=>p.ingredients.Contains(x)))
            {
                group_3.Add(p);
            }
            if (p.ingredients.Contains("mushrooms")&& p.ingredients.Contains("mozzarella_cheese")) {
                group_4.Add(p);
            }
        }

       
    }
}
