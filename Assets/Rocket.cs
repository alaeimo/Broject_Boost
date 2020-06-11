﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float mainThrust = 20f;
    [SerializeField] float rcsThrust = 100f;

    Rigidbody rigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Ok");
                break;
            case "Fuel":
                print("Fuel");
                break;
            case "Finish":
                print("Finish");
                break;
            default:
                print("Dead");
                break;
        }
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)){
            rigidBody.AddRelativeForce(Vector3.up*mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control of the rotation
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rcsThrust * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rcsThrust * Time.deltaTime);

        }
        rigidBody.freezeRotation = false; // lets physics control of the rotation
    }
}
