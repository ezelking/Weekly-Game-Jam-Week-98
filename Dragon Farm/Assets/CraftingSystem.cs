using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public GameObject newPerson;

    public List<Person> peopleToShow;

    public RectTransform peopleContainer;

    int selectedPerson;
    public GearSelection gearSelection;

    public LogController log;

    // Start is called before the first frame update
    void OnEnable()
    {
        selectedPerson = -1;
        peopleToShow = new List<Person>();
        Clear();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateList();
    }

    public void SelectGear()
    {
        Clear();
        List<Person> allPeople = ResourceManager.Instance.people;

        foreach(Person p in allPeople)
        {
            if (p.GetType() == typeof(Smith))
            {
                peopleToShow.Add(p);
            }
        }
        ShowList();
    }

    public void SelectTown()
    {
        Clear();
        List<Person> allPeople = ResourceManager.Instance.people;

        foreach (Person p in allPeople)
        {
            if (p.GetType() == typeof(Carpenter))
            {
                peopleToShow.Add(p);
            }
        }
        ShowList();
    }

    public void SelectFood()
    {
        Clear();
        List<Person> allPeople = ResourceManager.Instance.people;

        foreach (Person p in allPeople)
        {
            if (p.GetType() == typeof(Cook))
            {
                peopleToShow.Add(p);
            }
        }
        ShowList();
    }

    void Clear()
    {
        foreach (Transform child in peopleContainer)
        {
            GameObject.Destroy(child.gameObject);
        }

        peopleContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

        peopleToShow.Clear();
    }
    private void AddGear(int _selected)
    {
        gearSelection.selectedPerson = peopleToShow[_selected];
        gearSelection.gameObject.SetActive(true);
    }
    void ShowList()
    {
        for (int i = 0; i< peopleToShow.Count;i++)
        {
            GameObject addedPerson = Instantiate(newPerson, peopleContainer);
            addedPerson.transform.localPosition += new Vector3(0, -peopleContainer.sizeDelta.y, 0);
            int selected = i;
            addedPerson.GetComponent<Button>().onClick.AddListener(() => selectPerson(selected));
            addedPerson.transform.GetChild(2).GetComponentInChildren<Button>().onClick.AddListener(() => AddGear(selected));
            addedPerson.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = peopleToShow[i].personName;
            addedPerson.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = peopleToShow[i].GetStats().ToString();

            if (!peopleToShow[i].actionAvailable())
            {
                addedPerson.GetComponent<Image>().color = Color.grey;
            }

            peopleContainer.sizeDelta += new Vector2(0, 150);
        }
    }
    public void ShutWindow()
    {
        gameObject.SetActive(false);
    }

    void UpdateList()
    {
        for (int i = 0; i < peopleToShow.Count; i++)
        {
            if (!peopleToShow[i].actionAvailable())
            {
                peopleContainer.GetChild(i).GetComponent<Image>().color = Color.grey;
            } else
            {
                peopleContainer.GetChild(i).GetComponent<Image>().color = Color.white;
            }
            peopleContainer.GetChild(i).transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = peopleToShow[i].personName;
            peopleContainer.GetChild(i).transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = peopleToShow[i].GetStats().ToString();
        }
        if (selectedPerson >= 0)
        {
            Color selected = new Color(0, 1, 0, 100f / 255f);
            peopleContainer.GetChild(selectedPerson).GetComponent<Image>().color = selected;
        }
    }
    public void ConfirmSelection()
    {
        System.Type t = peopleToShow[selectedPerson].GetType();

        if (t == typeof(Cook))
        {
            if (ResourceManager.Instance.food.amount > 0)
            {
                ResourceManager.Instance.food.amount--;
                peopleToShow[selectedPerson].recharge = 30;
                HungerMeter.Instance.DecreaseHunger(peopleToShow[selectedPerson].GetStats().strength);
                log.AddLogMessage("Hunger levels decreased", true);
                ShutWindow();
            }
        }
        else if (t == typeof(Carpenter))
        {
            if (ResourceManager.Instance.wood.amount > 0)
            {
                ResourceManager.Instance.wood.amount--;
                peopleToShow[selectedPerson].recharge = 30;
                ResourceManager.Instance.populationLimit += peopleToShow[selectedPerson].GetStats().strength ;
                log.AddLogMessage("Population limit increased", true);
                ShutWindow();
            }
        }
        else if (t == typeof(Smith))
        {
            if (ResourceManager.Instance.metal.amount > 0)
            {
                ResourceManager.Instance.metal.amount--;
                peopleToShow[selectedPerson].recharge = 30;
                for (int i = 0; i < peopleToShow[selectedPerson].GetStats().strength; i++)
                {
                    Gear newGear = new Gear(true);
                    ResourceManager.Instance.gears.Add(newGear);

                    log.AddLogMessage("New Gear: " + newGear.name + " " + newGear.stats.ToString(), true);
                }
                ShutWindow();
            }
        }
    }
    public void selectPerson(int _selectedPerson)
    {
        if (!peopleToShow[_selectedPerson].actionAvailable())
        {
            selectedPerson = _selectedPerson;
        }
        
    }
}
