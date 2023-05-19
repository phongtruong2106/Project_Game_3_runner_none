using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public float groundHeight;

    private new BoxCollider2D collider2D;

    private void Awake() {
       collider2D = GetComponent<BoxCollider2D>();
       groundHeight = transform.position.y + (collider2D.size.y /2);
    }
}
