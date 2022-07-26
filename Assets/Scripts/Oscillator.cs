﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(0f, 20f, 0f);
    [SerializeField] float period = 5f;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycle = Time.time / period;
        float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(cycle * tau);
        float movemntFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movemntFactor * movementVector;
        transform.position = startPosition + offset;
    }
}