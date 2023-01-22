using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpin : MonoBehaviour
{
    private float currRotation = 0;
    private float currSpeed = 0;

    void Start()
    {
        currRotation = Random.Range(0, 360);
        transform.Rotate(0, 0, currRotation);
        currSpeed = Random.Range(0.5f, 1.5f);
    }

    void Update()
    {
        currRotation = currRotation + (currSpeed * Time.deltaTime * 60);
        Quaternion target = Quaternion.Euler(0, 0, currRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * 5);
    }
}
