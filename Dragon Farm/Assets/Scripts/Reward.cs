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
    public Metal()
    {
        name = "Metal";
    }
}

public class Wood : Resource
{
    public Wood()
    {
        name = "Wood";
    }
}

public class Food : Resource
{
    public Food()
    {
        name = "Food";
    }
}