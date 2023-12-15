using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class BackgroundManagment : MonoBehaviour
{

    public GameObject citroen;
    public GameObject renault;

    public new Camera camera;

    public float deltaSpawnTime;

    private Vector3 cameraPosition;
    private float cameraWidth;
    private Vector3 spawnOffset;
    private float nextSpawnTime;
    private System.Random random;

    void Start()
    {
        cameraPosition = camera.transform.position;
        cameraWidth = camera.orthographicSize * camera.aspect;
        spawnOffset = new Vector3(2.0f, -3.1f);
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
                spawnOffset.x = -2.0f;
                Vector3 carPosition = cameraPosition + spawnOffset;
                carPosition.x -= cameraWidth;
                carPosition.z = 0;
                GameObject citroenInstance = Instantiate(citroen, carPosition, Quaternion.identity);
                Destroy(citroenInstance, 5.0f);
            }
            else
            {
                spawnOffset.x = 2.0f;
                Vector3 carPosition = cameraPosition + spawnOffset;
                carPosition.x += cameraWidth;
                carPosition.z = 0;
                GameObject renaultInstance = Instantiate(renault, carPosition, Quaternion.identity);
                Destroy(renaultInstance, 5.0f);
            }
            nextSpawnTime = Time.time + deltaSpawnTime;
        }
    }


}
