using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public GameObject newPerson;

    public List<Person> peopleToShow;

    public RectTransform peopleContainer;

    int selectedPerson;

    // Start is called before the first frame update
    void Start()
    {
        peopleToShow = new List<Person>();
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

    void ShowList()
    {
        for (int i = 0; i< peopleToShow.Count;i++)
        {
            GameObject addedPerson = Instantiate(newPerson, peopleContainer);
            addedPerson.transform.localPosition += new Vector3(0, -peopleContainer.sizeDelta.y, 0);
            int selected = i;
            addedPerson.GetComponent<Button>().onClick.AddListener(() => selectPerson(selected));

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
        }
    }

    public void selectPerson(int _selectedPerson)
    {
        System.Type t = peopleToShow[selectedPerson].GetType();
        Debug.Log(t == typeof(Smith));
        if (t == typeof(Cook))
        {
            if (ResourceManager.Instance.food.amount > 0)
            {
                ResourceManager.Instance.food.amount--;
                peopleToShow[_selectedPerson].recharge = 30;
                // Decrease Hunger
            }
        }
        else if (t== typeof(Carpenter))
        {
            if (ResourceManager.Instance.wood.amount > 0)
            {
                ResourceManager.Instance.wood.amount--;
                peopleToShow[_selectedPerson].recharge = 30;
                ResourceManager.Instance.populationLimit++;
            }
        }
        else if (t == typeof(Smith))
        {
            if (ResourceManager.Instance.metal.amount > 0)
            {
                ResourceManager.Instance.metal.amount--;
                peopleToShow[_selectedPerson].recharge = 30;
                ResourceManager.Instance.gears.Add(new Gear(true));
            }
        }
        
    }
}
