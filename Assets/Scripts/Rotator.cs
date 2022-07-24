using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Rotator : MonoBehaviour
{
    [SerializeField] Vector3 rotationVector = new Vector3(0.5f, 1f, 0f);
    [SerializeField] float angle = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationVector,angle);
    }
}
