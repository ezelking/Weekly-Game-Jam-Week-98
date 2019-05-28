using UnityEngine;

public class Dragon : Reward
{
    float size;
    float strength;
    float durability;

    public GameObject dragon;

    public Dragon()
    {
        name = "Drogon";

        size = Random.Range(0, 100) / 100f;
        strength = Random.Range(0, 1);
        durability = Random.Range(0, 1);

    }

    public void Spawn()
    {
        dragon = GameObject.Instantiate((GameObject)Resources.Load("FreeDragons/Prefab/HP/Blue", typeof(GameObject)));
        dragon.transform.localScale = new Vector3(size, size, size);
        ResourceManager.Instance.AddDragon(this);
    }
}
