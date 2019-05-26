using System.Collections.Generic;
using UnityEngine;

public sealed class ResourceManager
{
    private static ResourceManager instance = null;
    private static readonly object padlock = new object();

    Resource wood;
    Resource metal;
    Resource food;
    public List<Dragon> dragons;
    public List<Person> people;

    ResourceManager()
    {
        wood = new Wood();
        metal = new Metal();
        food = new Food();

        food.amount = 0;

        dragons = new List<Dragon>();
        people = new List<Person>();

    }

    public static ResourceManager Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ResourceManager();
                }
                return instance;
            }
        }
    }

    public void AddResource(Food f)
    {
        food += f;
    }

    public void AddResource(Metal m)
    {
        metal += m;
    }

    public void AddResource(Wood w)
    {
        wood += w;
    }

    public int[] GetResourceAmounts()
    {
        return new int[5]{ dragons.Count,people.Count, food.amount, wood.amount, metal.amount };
    }
}
