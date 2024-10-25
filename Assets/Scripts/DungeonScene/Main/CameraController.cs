using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The main problem we try to solve here is that the Main Camera is a game object that is already in place before the Player Entity is generated, so it cannot be assigned as the Player instance is null */
public class CameraController : MonoBehaviour
{
    public Transform target;

    public void Init()
    {
        this.GetComponent<Camera>().orthographicSize = 12;
        target = Engine.Instance.GetPlayerEntity().transform; // Find the Player
        gameObject.transform.SetParent(target); // Assign the camera game object as a child of the Player Transform
        gameObject.transform.localPosition = new Vector3(0, 0, -1); // Reset its position to 0,0,-1 from the parent game object
    }
}
