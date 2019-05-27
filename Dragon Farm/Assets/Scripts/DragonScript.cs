using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonScript : MonoBehaviour
{
    float strength;
    float durability = 10;
    float rechargeTimer;
    float FireTimer;

    CharacterController controller;
    Animator animator;
    Image image;

    public float speed;
    float rotationSpeed;
    float gravity;
    bool die = false;
    bool breatheFire;
    GameObject fire;

    // Start is called before the first frame update
    void Start()
    {
        rechargeTimer = 0;
        
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        image = GetComponentInChildren<Image>();

        fire = GetComponentInChildren<ParticleSystem>().gameObject;
        fire.SetActive(false);

        speed = Random.Range(0, 5);

        strength = Random.Range(0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        rechargeTimer += Time.deltaTime;

        image.fillAmount = rechargeTimer / durability;

        if (rechargeTimer > durability)
        {
            animator.SetBool("BreatheFire", true);
            fire.SetActive(true);
            rechargeTimer = -9999;
            breatheFire = true;
        }
        if (breatheFire)
        {
            FireTimer += Time.deltaTime;
            Thermometer.Instance.IncreaseTemperature(strength * Time.deltaTime);
            if (FireTimer > strength)
            {
                rechargeTimer = 0;
                breatheFire = false;
                animator.SetBool("BreatheFire", false);
                fire.SetActive(false);
                FireTimer = 0;

            }
        }
        animator.SetFloat("Speed", speed);
        controller.Move(transform.forward *speed* Time.deltaTime);
            animator.SetBool("Die", die);
            die = false;

        Vector3 pos = transform.position;
        pos.y = Terrain.activeTerrain.SampleHeight(transform.position) + 5;
        transform.position = pos;
    }

    public void Kill()
    {
        die = true;
        image.gameObject.SetActive(false);
    }
}
