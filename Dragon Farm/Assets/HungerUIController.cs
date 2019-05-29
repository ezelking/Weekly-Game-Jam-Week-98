using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerUIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hunger = 100 - HungerMeter.Instance.getHunger();
        //Debug.Log(((hunger % 10f) / 10));
        for (int i = 0; i < 10; i++)
        {
            if(((i+1)*10) > hunger)
            {
                if (hunger > ((i) * 10))
                {
                    transform.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount = ((hunger % 10f) / 10);
                }
                else
                {
                    transform.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount = 0;
                }
            } else
            {
                transform.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount = 1;
            }
        }
    }
}
