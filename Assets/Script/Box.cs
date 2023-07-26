using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {
    public bool onCrox;

    public bool Move(Vector2 direction) {
        if (BoxBlocked(transform.position, direction)) {
            return false;
        }
        else {
            transform.Translate(direction);
            TransformForCrox();
            return true;
        }
    }

    private bool BoxBlocked(Vector3 position, Vector2 direction) {
    Vector2 newPos = new Vector2(position.x, position.y) + direction;
    GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
    foreach (var wall in walls) {
        if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y) {
            return true;
        }
    }

    GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
    foreach (var box in boxes) {
        if (box == gameObject) continue; // Skip checking against itself

        if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y) {
            return true; // There is another box in the direction we want to move
        }
    }

    return false;
}

    private void TransformForCrox() {
        GameObject[] crosses = GameObject.FindGameObjectsWithTag("Cross");
        foreach (var cross in crosses) {
            if (transform.position.x == cross.transform.position.x && transform.position.y == cross.transform.position.y) {
                GetComponent<SpriteRenderer>().color = Color.red;
                onCrox = true;
                return;
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        onCrox = false;
    }
}