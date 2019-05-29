using UnityEngine;

public sealed class HungerMeter
{
    private const float maxHunger = 100;

    private float hungerLevel = 0;
    private static HungerMeter instance = null;
    private static readonly object padlock = new object();

    HungerMeter()
    {
    }

    public static HungerMeter Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new HungerMeter();
                }
                return instance;
            }
        }
    }

    public void DecreaseHunger(float amount)
    {
        hungerLevel -= amount;
        if (hungerLevel < 0)
        {
            hungerLevel = 0;
        }
    }

    public float getHunger()
    {
        instance++;
        return hungerLevel;
    }

    public static HungerMeter operator ++(HungerMeter a)
    {
        if (a.hungerLevel < maxHunger)
        {
            a.hungerLevel += Time.deltaTime;
            if (a.hungerLevel >= maxHunger)
            {
                ResourceManager.Instance.KillDragons();
                foreach (Transform UIelement in GameObject.Find("UI").transform)
                {
                    if (UIelement.name != "Lose")
                        UIelement.gameObject.SetActive(false);
                    else
                        UIelement.gameObject.SetActive(true);
                }
            }
        }
        return a;
    }
}