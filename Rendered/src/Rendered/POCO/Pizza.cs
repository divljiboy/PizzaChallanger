using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

public class Pizza
{
    public string name { get; set; }
    public int price { get; set; }
    public List<string> ingredients { get; set; }


    public Pizza(string _name,int _price,List<string> _ingredients) {

        name = _name;
        price = _price;
        ingredients = _ingredients;
        
    }
    public Pizza() { 

    }
    
}
