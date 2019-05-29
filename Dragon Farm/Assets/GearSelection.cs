using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GearSelection : MonoBehaviour
{
    public GameObject newGear;

    public Person selectedPerson;

    public int selectedGear;
    public RectTransform GearContainer;

    // Start is called before the first frame update
    void Start()
    {
        selectedPerson = new Warrior("Henk");
        for (int i = 0; i < ResourceManager.Instance.gears.Count; i++)
        {
            GameObject addedPartyMember = Instantiate(newGear, GearContainer);
            addedPartyMember.transform.localPosition += new Vector3(0, -GearContainer.sizeDelta.y, 0);
            addedPartyMember.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ResourceManager.Instance.gears[i].name;
            addedPartyMember.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ResourceManager.Instance.gears[i].stats.ToString();
            int temp = i;
            addedPartyMember.GetComponentInChildren<Button>().onClick.AddListener(() => selectGear(temp));
            GearContainer.sizeDelta += new Vector2(0, 200);
        }
        Debug.Log(selectedPerson.GetStats().ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShutWindow()
    {
        gameObject.SetActive(false);
    }

    public void ConfirmSelection()
    {
        selectedPerson.AddGear(ResourceManager.Instance.gears[selectedGear]);
        ResourceManager.Instance.gears.RemoveAt(selectedGear);
        Destroy(GearContainer.GetChild(selectedGear));
        selectedGear = -1;
        Debug.Log(selectedPerson.GetStats().ToString());
    }

    public void selectGear(int _selectedGear)
    {
        Color selected = new Color(0, 1, 0, 100f / 255f);
        Color notSelected = new Color(1, 1, 1, 100f / 255f);

        if (GearContainer.GetChild(selectedGear) != null)
            GearContainer.GetChild(selectedGear).GetComponent<Image>().color = notSelected;

        selectedGear = _selectedGear;

        GearContainer.GetChild(selectedGear).GetComponent<Image>().color = selected;
    }
}
