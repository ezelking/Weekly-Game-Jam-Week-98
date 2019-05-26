using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlResourceManager : MonoBehaviour
{
    public TextMeshProUGUI amountDragons, amountPeople, amountFood, amountWood, amountMetal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int[] amounts = ResourceManager.Instance.GetResourceAmounts();

        amountDragons.text = amounts[0].ToString();
        amountPeople.text = amounts[1].ToString();
        amountFood.text = amounts[2].ToString();
        amountWood.text = amounts[3].ToString();
        amountMetal.text = amounts[4].ToString();
    }
}
