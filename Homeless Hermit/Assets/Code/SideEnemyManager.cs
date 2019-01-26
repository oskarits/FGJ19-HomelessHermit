﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEnemyManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public int enemySpawnTime;

    private float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeElapsed < 0)
            return;
        timeElapsed += Time.deltaTime;
        if(timeElapsed > enemySpawnTime)
        {
            foreach (GameObject item in enemies)
            {
                Vector3 p = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1, Camera.main.nearClipPlane));
                Instantiate(item, p, new Quaternion());
            }
            timeElapsed = -1f;
        }
    }
}