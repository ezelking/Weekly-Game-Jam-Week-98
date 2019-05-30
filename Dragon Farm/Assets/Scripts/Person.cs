using System.Collections.Generic;
using UnityEngine;

public abstract class Person
{
    public string personName;
    public Stats originalStats;

    public GameObject character;

    public Stats GetStats()
    {
        Stats gearTotal = new Stats();

        foreach (Gear gear in gears)
        {
            gearTotal += gear.stats;
        }
        return originalStats + gearTotal;
    }
    public List<Gear> gears;

    public float recharge;

    public abstract void Spawn();

    public bool actionAvailable()
    {
        return recharge < 0;
    }

    public void Recharge()
    {
        recharge -= Time.deltaTime * GetStats().chargeSpeed;
    }

    public void AddGear(Gear gear)
    {
        gears.Add(gear);
    }
}

public class Warrior: Person
{
    public Warrior(string _name)
    {
        personName = _name;
        originalStats = new Stats(true);
        recharge = 0;
        gears = new List<Gear>();
    }

    public override void Spawn()
    {
        character = GameObject.Instantiate((GameObject)Resources.Load("ToonyTinyPeople/TT_demo/prefabs/TT_demo_male_A", typeof(GameObject)));
        character.transform.position = new Vector3(1, -3, -45);
        ResourceManager.Instance.AddPerson(this);
    }
}

public class Carpenter : Person
{
    public Carpenter(string _name)
    {
        personName = _name;
        originalStats = new Stats(true);
        recharge = 0;
        gears = new List<Gear>();
    }
    public override void Spawn()
    {
        character = GameObject.Instantiate((GameObject)Resources.Load("ToonyTinyPeople/TT_demo/prefabs/TT_demo_male_A", typeof(GameObject)));
        character.transform.position = new Vector3(1, -3, -45);
        ResourceManager.Instance.AddPerson(this);
    }
}
public class Cook : Person
{
    public Cook(string _name)
    {
        personName = _name;
        originalStats = new Stats(true);
        recharge = 0;
        gears = new List<Gear>();
    }
    public override void Spawn()
    {
        character = GameObject.Instantiate((GameObject)Resources.Load("ToonyTinyPeople/TT_demo/prefabs/TT_demo_female", typeof(GameObject)));
        character.transform.position = new Vector3(1, -3, -45);
        ResourceManager.Instance.AddPerson(this);
    }
}

public class Smith : Person
{
    public Smith(string _name)
    {
        personName = _name;
        originalStats = new Stats(true);
        recharge = 0;
        gears = new List<Gear>();
    }
    public override void Spawn()
    {
        character = GameObject.Instantiate((GameObject)Resources.Load("ToonyTinyPeople/TT_demo/prefabs/TT_demo_male_B", typeof(GameObject)));
        character.transform.position = new Vector3(1, -3, -45);
        ResourceManager.Instance.AddPerson(this);
    }
}

/*public abstract class Decorator : Person
{
    protected Person person;

    public Decorator(Person _person)
    {
        person = _person;
    }

    public override Stats GetStats()
    {
        return person.GetStats();
    }

    public override List<Gear> GetGears()
    {
        return person.GetGears();
    }

    public override void Spawn()
    {
        person.Spawn();
    }
}

class PersonwithGear : Decorator
{
    Gear gear;

    public PersonwithGear(Person _person, Gear _gear): base(_person)
    {
        gear = _gear;
    }

    public override Stats GetStats()
    {
        return base.GetStats() + gear.stats;
    }
    public override List<Gear> GetGears()
    {
        List<Gear> list = base.GetGears();
        list.Add(gear);
        return list;
    }
}*/

public struct Stats
{
    public int strength { get; set; }
    public int chargeSpeed { get; set; }

    public Stats(bool newStats)
    {
        chargeSpeed = Random.Range(1,3);
        strength = Random.Range(1, 3);
    }
    public static Stats operator +(Stats a, Stats b)
    {
        a.strength += b.strength;
        a.chargeSpeed += b.chargeSpeed;
        return a;
    }

    public override string ToString()
    {
        return strength + " | " + chargeSpeed; 
    }
}

public struct Gear
{
    public string name;
    public Stats stats;

    public Gear(bool newGear)
    {
        name = "Helmet";
        stats = new Stats(true);
    }
}