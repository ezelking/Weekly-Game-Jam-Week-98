using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionScript : MonoBehaviour
{
    public List<GameObject> choices;
    List<Person> people;
    private int selectedPerson=-1;

    private void Start()
    {
        people = new List<Person>(); 
        for (int i = 0; i < 3; i++)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    people.Add(new Warrior("Jon", new Stats(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3))));
                    break;
                case 1:
                    people.Add(new Cook("Jon", new Stats(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3))));
                    break;
                case 2:
                    people.Add(new Smith("Jon", new Stats(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3))));
                    break;
                case 3:
                    people.Add(new Carpenter("Jon", new Stats(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3))));
                    break;
            }
            choices[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = people[i].personName;
            choices[i].transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = people[i].GetType().ToString();
            choices[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = people[i].originalStats.ToString();
            int selected = i;
            choices[i].GetComponent<Button>().onClick.AddListener(() => selectPerson(selected));
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ConfirmSelection()
    {
        people[selectedPerson].Spawn();
        switch (Random.Range(0, 4))
        {
            case 0:
                people[selectedPerson] = new Warrior("Jon", new Stats(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3)));
                break;
            case 1:
                people[selectedPerson] = new Cook("Jon", new Stats(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3)));
                break;
            case 2:
                people[selectedPerson] = new Smith("Jon", new Stats(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3)));
                break;
            case 3:
                people[selectedPerson] = new Carpenter("Jon", new Stats(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3)));
                break;
        }

        choices[selectedPerson].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = people[selectedPerson].personName;
        choices[selectedPerson].transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = people[selectedPerson].GetType().ToString();
        choices[selectedPerson].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = people[selectedPerson].originalStats.ToString();

        choices[selectedPerson].GetComponent<Image>().color = new Color(1, 1, 1, 100f / 255f);
        gameObject.SetActive(false);
    }

    public void selectPerson(int _selectedPerson)
    {
        Color selected = new Color(0, 1, 0, 100f / 255f);
        Color notSelected = new Color(1, 1, 1, 100f / 255f);

        if (selectedPerson >= 0 && selectedPerson < choices.Count)
            choices[selectedPerson].GetComponent<Image>().color = notSelected;
        
        selectedPerson = _selectedPerson;

        choices[selectedPerson].GetComponent<Image>().color = selected;

        ConfirmSelection();
    }
}
