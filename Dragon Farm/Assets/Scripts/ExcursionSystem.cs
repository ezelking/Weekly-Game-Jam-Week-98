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
    public RectTransform ExcursionsContainer;

    public GameObject newPartyMember;
    public GameObject newExcursion;
    public List<Person> partyMembers = new List<Person>();

    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        GenerateExcursion();

        AddEmptyPartyMember();
    }

    private void ToggleAdd(Transform info)
    {
        foreach (Transform child in info)
        {
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }


    }

    private void AddEmptyPartyMember()
    {
        GameObject addedPartyMember = Instantiate(newPartyMember, partyMemberContainer);
        addedPartyMember.transform.localPosition += new Vector3(0, -partyMemberContainer.sizeDelta.y, 0);

        Transform personInfo = addedPartyMember.transform.GetChild(0);
        Transform gearInfo = addedPartyMember.transform.GetChild(2);
        ToggleAdd(personInfo);
        ToggleAdd(gearInfo);

        personInfo.GetComponentInChildren<Button>().onClick.AddListener(() => EnterPartyMemberValues(addedPartyMember));
        partyMemberContainer.sizeDelta += new Vector2(0, 200);        
    }

    private void EnterPartyMemberValues(GameObject obj)
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

        AddEmptyPartyMember();
    }

    private void GenerateExcursion()
    {
        foreach (int i in new List<int>(5) { 1,1,1,1,1})
        {
            ExcursionsContainer.sizeDelta += new Vector2(0, 200);
            GameObject addedExcursion = Instantiate(newExcursion, ExcursionsContainer);
            addedExcursion.transform.localPosition += new Vector3(0, ExcursionsContainer.sizeDelta.y, 0);

            string name = "Cave";
            float winchance = 10.5f;

            addedExcursion.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
            addedExcursion.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = winchance.ToString() + "%";

            Transform rewardContainer = addedExcursion.transform.GetChild(2).GetChild(0).GetChild(0);

            foreach (int j in new List<int>(5) { 1,1,1,1,1})
            {
                GameObject Reward = Instantiate(new GameObject("Reward", typeof(RectTransform)), rewardContainer);
                Reward.AddComponent<TextMeshProUGUI>().text = "Dragon";
                Reward.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                Reward.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 75);
                Reward.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0);
                Reward.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0);
                rewardContainer.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 75);
                Reward.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
            }
        }
    }
}
