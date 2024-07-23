using System;
using System.Collections.Generic;

public interface IDrink
{
    double GetCost();
    string GetDescription();
}

public class BasicDrink : IDrink
{
    private string _description;
    private double _cost;

    public BasicDrink(string description, double cost)
    {
        _description = description;
        _cost = cost;
    }

    public double GetCost()
    {
        return _cost;
    }

    public string GetDescription()
    {
        return _description;
    }
}

public abstract class DrinkDecorator : IDrink
{
    protected IDrink _decoratedDrink;

    public DrinkDecorator(IDrink drink)
    {
        _decoratedDrink = drink;
    }

    public virtual double GetCost()
    {
        return _decoratedDrink.GetCost();
    }

    public virtual string GetDescription()
    {
        return _decoratedDrink.GetDescription();
    }
}

public class MilkDecorator : DrinkDecorator
{
    private string _milkType;
    private double _cost;

    public MilkDecorator(IDrink drink, string milkType, double cost) : base(drink)
    {
        _milkType = milkType;
        _cost = cost;
    }

    public override double GetCost()
    {
        return base.GetCost() + _cost;
    }

    public override string GetDescription()
    {
        return base.GetDescription() + $", {_milkType} Milk";
    }
}

public class ToppingDecorator : DrinkDecorator
{
    private string _topping;
    private double _cost;

    public ToppingDecorator(IDrink drink, string topping, double cost) : base(drink)
    {
        _topping = topping;
        _cost = cost;
    }

    public override double GetCost()
    {
        return base.GetCost() + _cost;
    }

    public override string GetDescription()
    {
        return base.GetDescription() + $", {_topping}";
    }
}

public class WhippedCreamDecorator : DrinkDecorator
{
    private double _cost;

    public WhippedCreamDecorator(IDrink drink, double cost) : base(drink)
    {
        _cost = cost;
    }

    public override double GetCost()
    {
        return base.GetCost() + _cost;
    }

    public override string GetDescription()
    {
        return base.GetDescription() + ", Whipped Cream";
    }
}

public class SizeDecorator : DrinkDecorator
{
    private string _size;
    private double _cost;

    public SizeDecorator(IDrink drink, string size, double cost) : base(drink)
    {
        _size = size;
        _cost = cost;
    }

    public override double GetCost()
    {
        return base.GetCost() + _cost;
    }

    public override string GetDescription()
    {
        return _size + " " + base.GetDescription();
    }
}

public class Program
{
    
    static void Main(string[] args)
    {
        Dictionary<string, double> sizePrices = new Dictionary<string, double>
        {
            { "Small", 5.0 },
            { "Medium", 6.5 },
            { "Large", 8.0 }
        };

        Dictionary<string, double> drinkPrices = new Dictionary<string, double>
        {
            { "Coffee", 2.0 },
            { "Tea", 1.5 }
        };

        Dictionary<string, double> milkPrices = new Dictionary<string, double>
        {
            { "Whole", 1.5 },
            { "Skim", 0.5 },
            { "Almond", 0.5 },
            { "Soy", 0.75 }
        };

        Dictionary<string, double> toppingPrices = new Dictionary<string, double>
        {
            { "Chocolate", 0.75 },
            { "Caramel", 0.50 },
            { "Vanilla", 0.25 }
        };

        double whippedCreamPrice = 0.25;

        // Display Menu
        Console.WriteLine("Menu:");
        Console.WriteLine("Sizes:");
        foreach (var size in sizePrices)
        {
            Console.WriteLine($"{size.Key} - ${size.Value}");
        }

        Console.WriteLine("\nBasic Drinks:");
        foreach (var drink in drinkPrices)
        {
            Console.WriteLine($"{drink.Key} - ${drink.Value}");
        }

        Console.WriteLine("\nMilk Types:");
        foreach (var milk in milkPrices)
        {
            Console.WriteLine($"{milk.Key} Milk - ${milk.Value}");
        }

        Console.WriteLine("\nToppings:");
        foreach (var topping in toppingPrices)
        {
            Console.WriteLine($"{topping.Key} - ${topping.Value}");
        }

        Console.WriteLine($"\nWhipped Cream - ${whippedCreamPrice}");

        // Customer interaction
        Console.WriteLine("\nLet's start your order:");

        Console.Write("Choose size (Small, Medium, Large): ");
        string sizeChoice = Console.ReadLine();

        Console.Write("Choose drink (Coffee, Tea): ");
        string drinkTypeChoice = Console.ReadLine();

        IDrink order = new BasicDrink(drinkTypeChoice, drinkPrices[drinkTypeChoice]);
        order = new SizeDecorator(order, sizeChoice, sizePrices[sizeChoice]);

        Console.Write("Add milk (Whole, Skim, Almond, Soy)? Leave blank for none: ");
        string milkChoice = Console.ReadLine();
        if (!string.IsNullOrEmpty(milkChoice))
        {
            order = new MilkDecorator(order, milkChoice, milkPrices[milkChoice]);
        }

        Console.Write("Add toppings (Chocolate, Caramel, Vanilla)? List separated by commas, or leave blank for none: ");
        string toppingsChoice = Console.ReadLine();
        if (!string.IsNullOrEmpty(toppingsChoice))
        {
            string[] toppings = toppingsChoice.Split(',');
            foreach (string topping in toppings)
            {
                order = new ToppingDecorator(order, topping.Trim(), toppingPrices[topping.Trim()]);
            }
        }

        Console.Write("Add whipped cream? (yes/no): ");
        string whippedCreamChoice = Console.ReadLine();
        if (whippedCreamChoice.ToLower() == "yes")
        {
            order = new WhippedCreamDecorator(order, whippedCreamPrice);
        }

        Console.WriteLine($"\nDescription: {order.GetDescription()}");
        Console.WriteLine($"Cost: ${order.GetCost():0.00}");

        Console.ReadLine();
    }
    
}
