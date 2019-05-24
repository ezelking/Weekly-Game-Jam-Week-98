using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlThermometerFill : MonoBehaviour
{
    Image fillImage;

    // Start is called before the first frame update
    void Start()
    {
        fillImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        fillImage.fillAmount = Thermometer.Instance.getTemp();
    }
}
