﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SideEnemyHoming : SideEnemyBase
{
    public GameObject player;
    private float secondsElapsed;
    public List<SideEnemyFeet> bodyParts;

    // Start is called before the first frame update
    void Start()
    {
        direction = Direction.Falling;
        secondsElapsed = 0;
        if(player == null)
        {
            player = GameObject.Find("SidePlayer");
        }
        bodyParts.ForEach(e => e.Enable(1));
    }

    // Update is called once per frame
    void Update()
    {
        secondsElapsed += Time.deltaTime;
        if (secondsElapsed > 1f && direction != Direction.Falling)
        {
            direction = GetPlayerDirection();
            secondsElapsed = 0;
        }

        WalkToDirection();
        RemoveIfOutsideScreen();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        IgnoreCollisionWithItem(col);
        if (direction == 0)
        {
            var spriteRenderer = col.gameObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && "Wall".Equals(spriteRenderer.sortingLayerName))
            {
                // We hit the ground, start moving
                direction = GetPlayerDirection();
                bodyParts.ForEach(e => e.Enable(2));
                secondsElapsed = 0;
            }
        }
    }

    private Direction GetPlayerDirection()
    {
        if (player == null)
            return Direction.Falling;
        Vector3 direction = player.transform.position - transform.position;
        float angle = Vector2.SignedAngle(direction, transform.up);
        if(angle > 0)
            return Direction.Right;
        return Direction.Left;
    }
}
