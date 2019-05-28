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
        wood = new Wood(0);
        metal = new Metal(0);
        food = new Food(0);

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

    public void AddPerson(Person p)
    {
        people.Add(p);
    }

    public void AddReward(Reward r)
    {
        if (r.GetType() == typeof(Dragon))
        {
            AddDragon((Dragon)r);
            ((Dragon)r).Spawn();
        } /*else if (r.GetType() == typeof(Person))
        {
            AddPerson((Person)r);
        }*/
        else if(r.GetType() == typeof(Metal))
        {
            AddResource((Metal)r);
        } else if (r.GetType() == typeof(Food))
        {
            AddResource((Food)r);
        }
        else if(r.GetType() == typeof(Wood))
        {
            AddResource((Wood)r);
        } 
    }

    public void AddDragon(Dragon d)
    {
        dragons.Add(d);
    }

    public void KillDragons()
    {
        foreach(Dragon dragon in dragons)
        {
            dragon.dragon.GetComponent<DragonScript>().Kill();
        }
    }

    public int[] GetResourceAmounts()
    {
        return new int[5]{ dragons.Count,people.Count, food.amount, wood.amount, metal.amount };
    }
}
