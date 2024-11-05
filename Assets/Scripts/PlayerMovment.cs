using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float MomentSpeed = 5f;
    public float RotateSpeed = 180f;
    float shipBoundaryRadius = 0.5f;
    public UnityEngine.Vector2 mapSize = new UnityEngine.Vector2(20f, 20f);
    private LineRenderer lineRenderer;
    public GameObject mapBoundaryObject;
    void Start()
    {
        // Inancializálás LineRenderer
        lineRenderer = mapBoundaryObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 5; // 4 sarokpont + záró visszatérés az első pontra
        lineRenderer.loop = true;
        lineRenderer.startWidth = 0.1f; // Vonal vastagsága
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Alapértelmezett material a vonalhoz
        lineRenderer.startColor = Color.red; // Vonal színe
        lineRenderer.endColor = Color.red;

        // Definiáljuk a határok pontjait
        UnityEngine.Vector3[] boundaryPoints = new UnityEngine.Vector3[5];
        boundaryPoints[0] = new UnityEngine.Vector3(-mapSize.x / 2, mapSize.y / 2, 0);  // bal felső
        boundaryPoints[1] = new UnityEngine.Vector3(mapSize.x / 2, mapSize.y / 2, 0);   // jobb felső
        boundaryPoints[2] = new UnityEngine.Vector3(mapSize.x / 2, -mapSize.y / 2, 0);  // jobb alsó
        boundaryPoints[3] = new UnityEngine.Vector3(-mapSize.x / 2, -mapSize.y / 2, 0); // bal alsó
        boundaryPoints[4] = boundaryPoints[0]; // vissza az első ponthoz

        // Kirajzoljuk a vonalakat
        lineRenderer.SetPositions(boundaryPoints);
    }

    void Update()
    {
        //Rotate
        UnityEngine.Quaternion rot = transform.rotation;
        float z = rot.eulerAngles.z;
        z -= Input.GetAxis("Horizontal") * RotateSpeed * Time.deltaTime;
        rot = UnityEngine.Quaternion.Euler(0, 0, z);
        transform.rotation = rot;

        //Move
        UnityEngine.Vector3 pos = transform.position;
        UnityEngine.Vector3 velocity = new UnityEngine.Vector3(0, Input.GetAxis("Vertical") * MomentSpeed * Time.deltaTime, 0);
        pos += rot * velocity;

        //Restrict to the camera boundries

        if (pos.y + shipBoundaryRadius > mapSize.y / 2)
        {
            pos.y = mapSize.y / 2 - shipBoundaryRadius;
        }
        if (pos.y - shipBoundaryRadius < -mapSize.y / 2)
        {
            pos.y = -mapSize.y / 2 + shipBoundaryRadius;
        }


        if (pos.x + shipBoundaryRadius > mapSize.x / 2)
        {
            pos.x = mapSize.x / 2 - shipBoundaryRadius;
        }
        if (pos.x - shipBoundaryRadius < -mapSize.x / 2)
        {
            pos.x = -mapSize.x / 2 + shipBoundaryRadius;
        }

        transform.position = pos;
    }

}
