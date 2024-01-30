using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class CarManagment : MonoBehaviour
{

    public GameObject citroen;
    public GameObject renault;

    public new Camera camera;

    public float deltaSpawnTime = 3.0f;

    private Vector3 cameraPosition;
    private float cameraWidth;
    private float carLevel = -0.75f;
    private float nextSpawnTime;
    private System.Random random;

    void Start()
    {
        cameraPosition = camera.transform.position;
        cameraWidth = camera.orthographicSize * camera.aspect;
        nextSpawnTime = Time.time + deltaSpawnTime;
        random = new System.Random();
    }

    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            int car = random.Next(2);
            cameraPosition = camera.transform.position;
            if (car == 0)
            {
                Vector3 carPosition = new Vector3(cameraPosition.x - 2.0f - cameraWidth, carLevel - 0.75f, 0.0f);
                GameObject citroenInstance = Instantiate(citroen, carPosition, Quaternion.identity);
                Destroy(citroenInstance, 5.0f);
            }
            else
            {
                Vector3 carPosition = new Vector3(cameraPosition.x + 2.0f + cameraWidth, carLevel + 0.75f, 0.0f);
                GameObject renaultInstance = Instantiate(renault, carPosition, Quaternion.identity);
                Destroy(renaultInstance, 5.0f);
            }
            nextSpawnTime = Time.time + deltaSpawnTime;
        }
    }


}
