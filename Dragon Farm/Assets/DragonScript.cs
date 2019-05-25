using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : MonoBehaviour
{
    float strength;
    float durability;

    CharacterController controller;
    Animator animator;

    public float speed;
    float rotationSpeed;
    float gravity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", speed);
        controller.Move(transform.forward *speed* Time.deltaTime);
    }
}
