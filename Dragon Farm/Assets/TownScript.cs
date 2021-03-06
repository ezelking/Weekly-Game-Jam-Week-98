﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownScript : MonoBehaviour
{
    public GameObject highlighed;

    public GameObject craftingUI;

    public GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        highlighed.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlighed.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            if (NoPopUpsOpen())
            craftingUI.SetActive(true);
        }
    }

    bool NoPopUpsOpen()
    {
        return !UI.transform.FindChild("Lose").gameObject.activeSelf && !UI.transform.FindChild("Excursion").gameObject.activeSelf && !UI.transform.FindChild("Crafting").gameObject.activeSelf && !UI.transform.FindChild("NewArrival").gameObject.activeSelf;
    }
}
