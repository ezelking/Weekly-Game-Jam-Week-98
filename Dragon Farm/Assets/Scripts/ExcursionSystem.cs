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
    List<Excursion> activeMissions;

    public GameObject newPartyMember;
    public GameObject newExcursion;
    public List<Person> partyMembers = new List<Person>();

    public float partyStrength;

    private Excursion selectedExcursion;

    // Start is called before the first frame update
    void Start()
    {
        partyStrength = 1;

        GenerateExcursion();

        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Summit";

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
        activeMissions = RandomExcursions();

        foreach (Excursion excursion in activeMissions)
        {

            Transform rewardContainer = excursion.UIelement.transform.GetChild(2).GetChild(0).GetChild(0);

            foreach (Reward reward in excursion.rewards)
            {
                GameObject rewardText = new GameObject("Reward", typeof(RectTransform));
                rewardText.transform.SetParent(rewardContainer);
                rewardText.AddComponent<TextMeshProUGUI>();
                rewardText.GetComponent<TextMeshProUGUI>().text = reward.name;
                if (reward.GetType().IsSubclassOf(typeof(Resource)))
                {
                    rewardText.GetComponent<TextMeshProUGUI>().text += " X " + ((Resource)reward).amount;
                }
                rewardText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                rewardText.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 75);
                rewardText.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0);
                rewardText.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0);
                rewardContainer.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 75);
                rewardText.GetComponent<RectTransform>().localPosition = new Vector2(0, -37.5f);
            }
        }
    }

    public void ShutWindow()
    {
        gameObject.SetActive(false);
    }

    public void ConfirmSelection()
    {
        foreach (Reward reward in selectedExcursion.rewards)
        {
            ResourceManager.Instance.AddReward(reward);
        }
        gameObject.SetActive(false);
    }

    public void selectExcursion(GameObject _selectedExcursion)
    {
        Color selected = new Color(0, 1, 0, 100f/255f);
        Color notSelected = new Color(1, 1, 1, 100f/255f);

        if (selectedExcursion.UIelement != null)
        selectedExcursion.UIelement.GetComponent<Image>().color = notSelected;

        foreach (Excursion excursion in activeMissions)
        {
            if (excursion.UIelement == _selectedExcursion)
            {
                selectedExcursion = excursion;
            }
        }

        selectedExcursion.UIelement.GetComponent<Image>().color = selected;
    }

    private List<Excursion> RandomExcursions()
    {
        List<Excursion> excursions = new List<Excursion>();

        for (int i = 0; i < Random.Range(1, 10); i++)
        {
            ExcursionsContainer.sizeDelta += new Vector2(0, 200);
            GameObject addedExcursion = Instantiate(newExcursion, ExcursionsContainer);
            addedExcursion.GetComponent<Button>().onClick.AddListener(() => selectExcursion(addedExcursion));
            addedExcursion.transform.localPosition += new Vector3(0, ExcursionsContainer.sizeDelta.y, 0);
            Excursion excursion = new Excursion(5, addedExcursion);

            addedExcursion.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = excursion.name;
            addedExcursion.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = excursion.WinChance(partyStrength).ToString() + "%";
            //excursion.UIelement = addedExcursion;
            excursions.Add(excursion);
        }

        return excursions;
    }

    public struct Excursion
    {
        public string name;
        public float difficulty;
        public List<Reward> rewards;

        public GameObject UIelement;

        public float WinChance(float partyStrength)
        {            
            return difficulty / partyStrength;
        }

        public Excursion(int maxLevel, GameObject _UIelement)
        {
            name = "Cave";
            difficulty = Random.Range(1, maxLevel);
            rewards = new List<Reward>();
            for (int i = 0; i < difficulty; i++)
            {
                switch (Random.Range(0, 4)) {

                    case 0:
                        rewards.Add(new Dragon());
                        break;
                    case 1:
                        rewards.Add(new Wood(Random.Range(1,10)));
                        break;
                    case 2:
                        rewards.Add(new Metal(Random.Range(1, 10)));
                        break;
                    case 3:
                        rewards.Add(new Food(Random.Range(1, 10)));
                        break;

                }
            }
            UIelement = _UIelement;
        }
    }
}
