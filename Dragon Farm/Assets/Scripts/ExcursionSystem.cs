using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ExcursionSystem : MonoBehaviour
{
    UnityEvent AddPersonEvent = new UnityEvent();
    UnityEvent AddGearEvent = new UnityEvent();

    public RectTransform partyMemberContainer;
    public Transform ExcursionsContainer;

    public GameObject newPartyMember;

    public List<Person> partyMembers = new List<Person>();

    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        AddEmpty();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ToggleAdd(Transform info)
    {
        foreach (Transform child in info)
        {
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }


    }

    private void AddEmpty()
    {
        GameObject addedPartyMember = Instantiate(newPartyMember, partyMemberContainer);
        addedPartyMember.transform.localPosition += new Vector3(0, -partyMemberContainer.sizeDelta.y, 0);

        Transform personInfo = addedPartyMember.transform.GetChild(0);
        Transform gearInfo = addedPartyMember.transform.GetChild(2);
        ToggleAdd(personInfo);
        ToggleAdd(gearInfo);

        personInfo.GetComponentInChildren<Button>().onClick.AddListener(() => EnterValues(addedPartyMember));
        partyMemberContainer.sizeDelta += new Vector2(0, 200);

        
    }

    private void EnterValues(GameObject obj)
    {
        Person p = new PersonwithGear(new Warrior("John Doe", new Stats(2, 4, 5)), "Strong Armor", new Stats(1, 0, 1));

        Transform personInfo = obj.transform.GetChild(0);
        Transform gearInfo = obj.transform.GetChild(2);

        ToggleAdd(personInfo);
        ToggleAdd(gearInfo);

        if (p.GetGears().Count > 0)
        {
            foreach (Gear g in p.GetGears())
            {
                gearInfo.GetChild(1).GetComponent<TextMeshProUGUI>().text = g.name + i++;
            }
        }
        else
        {
            ToggleAdd(gearInfo);
        }

        AddEmpty();
    }
}
