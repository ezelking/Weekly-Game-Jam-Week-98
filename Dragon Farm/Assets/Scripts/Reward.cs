public abstract class Reward 
{
    public string name;
}

public class Resource : Reward
{
    public int amount;

    public static Resource operator +(Resource a, Resource b)
    {
        a.amount += b.amount;
        return a;
    }
}
public class Metal : Resource
{
    public Metal(int _amount)
    {
        name = "Metal";
        amount = _amount;
    }
}

public class Wood : Resource
{
    public Wood(int _amount)
    {
        name = "Wood";
        amount = _amount;
    }
}

public class Food : Resource
{
    public Food(int _amount)
    {
        name = "Food";
        amount = _amount;
    }
}