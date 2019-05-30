using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ExcursionSystem : MonoBehaviour
{
    public RectTransform partyMemberContainer;
    public RectTransform ExcursionsContainer;
    List<Excursion> activeMissions;

    public GameObject newPartyMember;
    public GameObject newExcursion;
    public List<Person> partyMembers = new List<Person>();

    public GearSelection gearSelection;

    public float partyStrength;

    private Excursion selectedExcursion;

    int selectedPerson;

    // Start is called before the first frame update
    void OnEnable()
    {

        Clear();
        partyStrength = 1;
        selectedPerson = -1;
        GenerateExcursion();

        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Summit";

        List<Person> allPeople = ResourceManager.Instance.people;

        foreach (Person p in allPeople)
        {
            if (p.GetType() == typeof(Warrior))
            {
                partyMembers.Add(p);
            }
        }
        ShowList();
        //AddEmptyPartyMember();
    }

    void Clear()
    {
        foreach (Transform child in partyMemberContainer)
        {
            GameObject.Destroy(child.gameObject);
        }

        partyMemberContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

        partyMembers.Clear();

        foreach (Transform child in ExcursionsContainer)
        {
            GameObject.Destroy(child.gameObject);
        }

        ExcursionsContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (selectedPerson >= 0)
        {
            partyStrength = partyMembers[selectedPerson].GetStats().health;
        } else
        {
            partyStrength = 1;
        }

        UpdateList();
    }


    void ShowList()
    {
        for (int i = 0; i < partyMembers.Count; i++)
        {
            GameObject addedPerson = Instantiate(newPartyMember, partyMemberContainer);
            addedPerson.transform.localPosition += new Vector3(0, -partyMemberContainer.sizeDelta.y, 0);
            int selected = i;
            addedPerson.GetComponent<Button>().onClick.AddListener(() => selectPerson(selected));
            Transform gearInfo = addedPerson.transform.GetChild(2);
            gearInfo.GetComponentInChildren<Button>().onClick.AddListener(() => AddGear(selected));

            addedPerson.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = partyMembers[i].personName;
            addedPerson.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = partyMembers[i].GetStats().ToString();
            if (!partyMembers[i].actionAvailable())
            {
                addedPerson.GetComponent<Image>().color = Color.grey;
            }

            partyMemberContainer.sizeDelta += new Vector2(0, 200);
        }
    }

    public void selectPerson(int _selected)
    {
        Color selected = new Color(0, 1, 0, 100f / 255f);
        Color notSelected = new Color(1, 1, 1, 100f / 255f);
        
        if(selectedPerson >=0)
        partyMemberContainer.GetChild(selectedPerson).GetComponent<Image>().color = notSelected;

        selectedPerson = _selected;

        partyMemberContainer.GetChild(selectedPerson).GetComponent<Image>().color = selected;
    }

    private void ToggleAdd(Transform info)
    {
        foreach (Transform child in info)
        {
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }
    }
    void UpdateList()
    {
        for (int i = 0; i < partyMembers.Count; i++)
        {
            if (!partyMembers[i].actionAvailable())
            {
                partyMemberContainer.GetChild(i).GetComponent<Image>().color = Color.grey;
            }
            else
            {
                partyMemberContainer.GetChild(i).GetComponent<Image>().color = Color.white;
            }
            partyMemberContainer.GetChild(i).transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = partyMembers[i].personName;
            partyMemberContainer.GetChild(i).transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = partyMembers[i].GetStats().ToString();
        }
        if (selectedPerson >= 0)
        {
            Color selected = new Color(0, 1, 0, 100f / 255f);
            partyMemberContainer.GetChild(selectedPerson).GetComponent<Image>().color = selected;
        }

        for (int i = 0; i < activeMissions.Count; i++)
        {
            ExcursionsContainer.GetChild(i).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = activeMissions[i].WinChance(partyStrength).ToString() + "%";
        }
    }
    /*private void AddEmptyPartyMember()
    {
        GameObject addedPartyMember = Instantiate(newPartyMember, partyMemberContainer);
        addedPartyMember.transform.localPosition += new Vector3(0, -partyMemberContainer.sizeDelta.y, 0);
        addedPartyMember.GetComponentInChildren<Button>().onClick.AddListener(() => selectedPerson(addedPartyMember));
        Transform personInfo = addedPartyMember.transform.GetChild(0);
        Transform gearInfo = addedPartyMember.transform.GetChild(2);

        personInfo.GetComponentInChildren<Button>().onClick.AddListener(() => EnterPartyMemberValues(addedPartyMember));
        gearInfo.GetComponentInChildren<Button>().onClick.AddListener(() => AddGear(addedPartyMember));
        partyMemberContainer.sizeDelta += new Vector2(0, 200);        
    }
    /*public void SelectPerson()
    {
        Clear();
        List<Person> allPeople = ResourceManager.Instance.people;

        foreach (Person p in allPeople)
        {
            if (p.GetType() == typeof(Smith))
            {
                peopleToShow.Add(p);
            }
        }
        ShowList();
    }
    private void EnterPartyMemberValues(GameObject obj)
    {
        Person p = new Warrior("John Smith");
        
        Transform personInfo = obj.transform.GetChild(0);
        Transform gearInfo = obj.transform.GetChild(2);

        personInfo.GetChild(1).GetComponent<TextMeshProUGUI>().text = p.personName;
        personInfo.GetChild(2).GetComponent<TextMeshProUGUI>().text = p.GetStats().ToString();

        ToggleAdd(personInfo);

        AddEmptyPartyMember();
    }*/

    private void AddGear(int _selected)
    {
        gearSelection.selectedPerson = partyMembers[_selected];
        gearSelection.gameObject.SetActive(true);
    }

    private void GenerateExcursion()
    {
        activeMissions = RandomExcursions();
        
    }

    public void ShutWindow()
    {
        gameObject.SetActive(false);
    }

    public void ConfirmSelection()
    {
        if (selectedPerson >= 0 && selectedExcursion.UIelement != null)
        {
            if (selectedExcursion.WinChance(partyStrength) > Random.Range(0, 100))
            {
                foreach (Reward reward in selectedExcursion.rewards)
                {
                    ResourceManager.Instance.AddReward(reward);
                }
                selectedExcursion = new Excursion(5, selectedExcursion.UIelement);
                gameObject.SetActive(false);
            } else
            {
                Debug.Log("Failed");
            }
        }
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

        for (int i = 0; i < 5; i++)
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

        public decimal WinChance(float partyStrength)
        {
            decimal chance = decimal.Round((decimal)partyStrength / (decimal)difficulty, 2) * 100;
            if (chance > 100)
                chance = 100;
            return chance;
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

            Transform rewardContainer = UIelement.transform.GetChild(2).GetChild(0).GetChild(0);

            foreach (Transform child in rewardContainer)
            {
                GameObject.Destroy(child.gameObject);
                rewardContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            }

            foreach (Reward reward in rewards)
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
}
