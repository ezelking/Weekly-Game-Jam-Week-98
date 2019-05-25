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

    public float partyStrength;

    // Start is called before the first frame update
    void Start()
    {
        partyStrength = 1;

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
                gearInfo.GetChild(1).GetComponent<TextMeshProUGUI>().text = g.name;
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
        List<Excursion> missions = RandomExcursions();

        foreach (Excursion excursion in missions)
        {
            ExcursionsContainer.sizeDelta += new Vector2(0, 200);
            GameObject addedExcursion = Instantiate(newExcursion, ExcursionsContainer);
            addedExcursion.transform.localPosition += new Vector3(0, ExcursionsContainer.sizeDelta.y, 0);

            addedExcursion.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = excursion.name;
            addedExcursion.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = excursion.WinChance(partyStrength).ToString() + "%";

            Transform rewardContainer = addedExcursion.transform.GetChild(2).GetChild(0).GetChild(0);

            foreach (Reward reward in excursion.rewards)
            {
                GameObject rewardText = Instantiate(new GameObject("Reward", typeof(RectTransform)), rewardContainer);
                rewardText.AddComponent<TextMeshProUGUI>();
                rewardText.GetComponent<TextMeshProUGUI>().text = reward.name;
                rewardText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                rewardText.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 75);
                rewardText.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0);
                rewardText.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0);
                rewardContainer.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 75);
                rewardText.GetComponent<RectTransform>().localPosition = new Vector2(0, -37.5f);
            }
        }
    }

    private List<Excursion> RandomExcursions()
    {
        List<Excursion> excursions = new List<Excursion>();

        for (int i = 0; i < Random.Range(1, 10); i++)
        {
            excursions.Add(new Excursion(5));
        }

        return excursions;
    }

    public struct Excursion
    {
        public string name;
        public float difficulty;
        public List<Reward> rewards;
        
        public float WinChance(float partyStrength)
        {            
            return difficulty / partyStrength;
        }

        public Excursion(int maxLevel)
        {
            name = "Cave";
            difficulty = Random.Range(1, maxLevel);
            rewards = new List<Reward>();
            for (int i = 0;i< difficulty; i++)
            {
                rewards.Add(new Dragon());
            }
        }
    }

    public abstract class Reward
    {
        public string name;
    }

    public class Resource : Reward
    {
        int amount;
    }

    public class Iron : Resource
    {
        public Iron()
        {
            name ="iron";
        }
    }

    public class Wood : Resource
    {
        public Wood()
        {
            name = "wood";
        }
    }

    public class Dragon : Reward
    {
        public Dragon()
        {
            name = "Drogon";
        }
    }
}
