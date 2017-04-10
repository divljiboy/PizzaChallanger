using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

public class Pizza
{
    public string name { get; set; }
    public double price { get; set; }
    public List<string> ingredients { get; set; }


    public Pizza(string _name,double _price,List<string> _ingredients) {

        name = _name;
        price = _price;
        ingredients = _ingredients;
        
    }
    public Pizza() { 

    }
    
}
