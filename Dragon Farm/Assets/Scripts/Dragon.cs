using UnityEngine;

public class Dragon : Reward
{
    float size;
    float strength;
    float durability;

    GameObject dragon = (GameObject)Resources.Load("FreeDragons/Prefab/HP/Blue", typeof(GameObject));

    public Dragon()
    {
        name = "Drogon";

        size = Random.Range(0, 100) / 100f;
        strength = Random.Range(0, 1);
        durability = Random.Range(0, 1);

        GameObject.Instantiate(dragon).transform.localScale = new Vector3(size,size,size);
    }
}
