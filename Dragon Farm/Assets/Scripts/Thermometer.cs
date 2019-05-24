using UnityEngine;

public sealed class Thermometer
{
    private const float minTemp = 0, maxTemp = 100;

    private float tempurature = maxTemp;
    private static Thermometer instance = null;
    private static readonly object padlock = new object();

    Thermometer()
    {
    }

    public static Thermometer Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Thermometer();
                }
                return instance;
            }
        }
    }

    public void IncreaseTemperature(float amount)
    {
        tempurature += amount;
        if (tempurature > maxTemp)
        {
            tempurature = maxTemp;
        }
    }

    public float getTemp()
    {
        instance--;
        return (tempurature - minTemp) / (maxTemp - minTemp);
    }

    public static Thermometer operator --(Thermometer a)
    {
        a.tempurature -= Time.deltaTime;
        if (a.tempurature > maxTemp)
        {
            //IMPLEMENT LOSE CONDITION
        }
        return a;
    }
}