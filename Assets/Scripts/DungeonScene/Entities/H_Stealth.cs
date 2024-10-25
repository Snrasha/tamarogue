using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Disable Entities visibility until these are within the Player FOVCollisionHolder
public class H_Stealth : MonoBehaviour
{
    private Renderer sprite;

    public void Start()
    {
        sprite = GetComponent<Entity>().spriteAnimator.spriteRenderer;
       // sprite.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "FOVCollisionHolder")
        {
            //Debug.Log("Enemy is visible");
            sprite.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "FOVCollisionHolder")
        {
            //Debug.Log("Enemy is hidden");
            sprite.enabled = false;
        }
    }
}
