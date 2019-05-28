using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    public void Test()
    {/*
        Person p = new Warrior("John Doe", new Stats(2,4,5));

        PersonwithGear pWithGear = new PersonwithGear(p, "Strong Armor", new Stats(1, 0, 1));
        p =new PersonwithGear(p, "Strong Armor", new Stats(1, 0, 1));
        Debug.Log(p.Stats().health);
        Debug.Log(pWithGear.Stats().health);*/
        Food food = new Food(5);

        ResourceManager.Instance.AddResource(food);

        ResourceManager.Instance.AddPerson(new Warrior("John", new Stats(1,1,1)));
    }
}

[CustomEditor(typeof(test))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        test myScript = (test)target;
        if (GUILayout.Button("Test Function"))
        {
            myScript.Test();
        }
    }
}