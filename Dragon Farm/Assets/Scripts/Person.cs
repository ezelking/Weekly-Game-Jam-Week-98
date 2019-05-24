public abstract class Person
{
    public string name;
    public Stats originalStats;

    public abstract Stats Stats();
}

public class Warrior: Person
{
    public Warrior(string _name, Stats _stats)
    {
        name = _name;
        originalStats = _stats;
    }
    public override Stats Stats() {
        return originalStats;
    }
}

public abstract class Decorator : Person
{
    protected Person person;

    public Decorator(Person _person)
    {
        person = _person;
    }

    public override Stats Stats()
    {
        return person.Stats();
    }
}

class PersonwithGear : Decorator
{
    string gearName;
    public Stats gearStats;

    public PersonwithGear(Person _person, string _name, Stats _stats): base(_person)
    {
        gearName = _name;
        gearStats = _stats;
    }
    public override Stats Stats()
    {
        return base.Stats() + gearStats;
    }
}

public enum CharacterClasses
{
    Explorer,
    Hunter,
    Wood_Chopper,
    Smith,
    Cook
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
}
