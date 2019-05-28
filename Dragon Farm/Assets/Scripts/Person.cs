﻿using System.Collections.Generic;
using UnityEngine;

public abstract class Person
{
    public string name;
    public Stats originalStats;

    public GameObject character;

    public abstract Stats GetStats();
    public abstract List<Gear> GetGears();

    public abstract void Spawn();
}

public class Warrior: Person
{
    public Warrior(string _name, Stats _stats)
    {
        name = _name;
        originalStats = _stats;
    }
    public override Stats GetStats() {
        return originalStats;
    }
    public override List<Gear> GetGears()
    {
        return new List<Gear>();
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
    public Carpenter(string _name, Stats _stats)
    {
        name = _name;
        originalStats = _stats;
    }
    public override Stats GetStats()
    {
        return originalStats;
    }
    public override List<Gear> GetGears()
    {
        return new List<Gear>();
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
    public Cook(string _name, Stats _stats)
    {
        name = _name;
        originalStats = _stats;
    }
    public override Stats GetStats()
    {
        return originalStats;
    }
    public override List<Gear> GetGears()
    {
        return new List<Gear>();
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
    public Smith(string _name, Stats _stats)
    {
        name = _name;
        originalStats = _stats;
    }
    public override Stats GetStats()
    {
        return originalStats;
    }
    public override List<Gear> GetGears()
    {
        return new List<Gear>();
    }
    public override void Spawn()
    {
        character = GameObject.Instantiate((GameObject)Resources.Load("ToonyTinyPeople/TT_demo/prefabs/TT_demo_male_B", typeof(GameObject)));
        character.transform.position = new Vector3(1, -3, -45);
        ResourceManager.Instance.AddPerson(this);
    }
}

public abstract class Decorator : Person
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
    public PersonwithGear(Person _person, string _name, Stats _stats): base(_person)
    {
        gear.name = _name;
        gear.stats = _stats;
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
}

public struct Stats
{
    public int health { get; set; }
    int strength { get; set; }
    int intelligence { get; set; }
    public Stats(int _health, int _strength, int _intelligence)
    {
        health = _health;
        strength = _strength;
        intelligence = _intelligence;
    }
    public static Stats operator +(Stats a, Stats b)
    {
        a.health += b.health;
        a.strength += b.strength;
        a.intelligence += b.intelligence;
        return a;
    }

    public override string ToString()
    {
        return health + " | " + strength + " | " + intelligence; 
    }
}

public struct Gear
{
    public string name;
    public Stats stats;
}