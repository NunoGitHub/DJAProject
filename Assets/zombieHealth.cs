﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieHealth : MonoBehaviour
{
    float health, maxHealth = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void DealDamage(string weapon, GameObject obj, float distance)
    {
        if (weapon == "Rifle")
            obj.GetComponent<zombieHealth>().health -= 20.0f / (distance * 0.2f);
        else if (weapon == "Revolver")
            obj.GetComponent<zombieHealth>().health -= 45.0f / (distance * 0.01f);
        else if (weapon == "Shotgun")
            obj.GetComponent<zombieHealth>().health -= 5.0f / (distance * 0.5f);

        Debug.Log(obj.GetComponent<zombieHealth>().health);

        if (obj.GetComponent<zombieHealth>().health <= 0.0f)
            GameObject.Destroy(obj);
    }
}
