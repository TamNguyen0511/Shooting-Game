﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hits = 1;
    public int point = 100;
    public Vector3 rotator;
    public Material hitMat;

    Material orgMat;
    Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(rotator * (transform.position.x + transform.position.y));
        renderer = GetComponent<Renderer>();
        orgMat = renderer.sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotator * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        hits--;
        //Score points
        if (hits <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.Score += point;
        }

        renderer.sharedMaterial = hitMat;

        //Invoke("RestoreMat", 0.05f);
    }

    void RestoreMat()
    {
        renderer.sharedMaterial = orgMat;
    }
}
