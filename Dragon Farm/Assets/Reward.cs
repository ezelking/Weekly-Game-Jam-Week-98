public abstract class Reward 
{
    public string name;
}

public class Resource : Reward
{
    int amount;
}

public class Iron : Resource
{
    public Iron()
    {
        name = "iron";
    }
}

public class Wood : Resource
{
    public Wood()
    {
        name = "wood";
    }
}