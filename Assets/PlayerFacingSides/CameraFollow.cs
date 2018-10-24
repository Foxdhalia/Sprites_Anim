using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object
    public float playerPosXMin;
    public float playerPosXMax;

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera


    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (player.transform.position.x >= playerPosXMin && player.transform.position.x <= playerPosXMax 
            && player.transform.position.y > -5)
        {
            transform.position = player.transform.position + offset;
        }       
    }
}