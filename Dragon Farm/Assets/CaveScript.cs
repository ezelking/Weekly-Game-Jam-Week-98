using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveScript : MonoBehaviour
{
    public GameObject highlighed;
    public GameObject excursionUI;

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
            excursionUI.SetActive(true);
        }
    }
}
