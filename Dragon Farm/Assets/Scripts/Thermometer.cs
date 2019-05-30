using UnityEngine;

public sealed class Thermometer
{
    private const float minTemp = 0, maxTemp = 100;

    private float tempurature = maxTemp;
    private static Thermometer instance = null;
    private static readonly object padlock = new object();

    float passedTime;

    Thermometer()
    {
        passedTime = 0;
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
        if (a.tempurature > minTemp)
        {
            a.tempurature -= Time.deltaTime * (a.passedTime*0.01f);
            a.passedTime += Time.deltaTime;
            if (a.tempurature <= minTemp)
            {
                ResourceManager.Instance.KillDragons();
                foreach (Transform UIelement in GameObject.Find("UI").transform)
                {
                    if (UIelement.name != "Lose")
                        UIelement.gameObject.SetActive(false);
                }
            }
        }
        return a;
    }
}