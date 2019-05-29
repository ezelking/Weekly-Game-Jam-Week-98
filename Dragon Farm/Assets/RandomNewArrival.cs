using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNewArrival : MonoBehaviour
{
    public GameObject newArrivalPopUp;
    public float countDownTimer = 30f;
    int townLevel = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ResourceManager.Instance.people.Count < ResourceManager.Instance.populationLimit)
        {
            countDownTimer -= Time.deltaTime;
            if (countDownTimer < 0 && NoPopUpsOpen())
            {
                newArrivalPopUp.SetActive(true);
                countDownTimer = 30f;
            }
        }

        foreach (Person p in ResourceManager.Instance.people)
        {
            p.Recharge();
        }
    }

    bool NoPopUpsOpen()
    {
        return !transform.FindChild("Lose").gameObject.activeSelf && !transform.FindChild("Excursion").gameObject.activeSelf && !transform.FindChild("Crafting").gameObject.activeSelf;
    }
}
