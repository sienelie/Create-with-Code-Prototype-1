using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // declare variables
    public GameObject player;
    [SerializeField] private Vector3 offset = new Vector3(0, 6, -12);

    // Update is called once per frame
	// but the vehicle moves once per time because of deltaTime
    // LateUpdate to remove the stutter effect
	// letting the vehicle move first before the camera for a smoother movement
    void LateUpdate()
    {
        // Offset the camera behind the player by adding the player's position
        transform.position = player.transform.position + offset;
    }
}
