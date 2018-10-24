using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : MonoBehaviour {

    public float speed; // Card speed movement.

    Vector3 origin;
    Vector3 destiny = new Vector3(0f, 0f, 0f);    
    float startTime;
    float distance;
    bool go = false;

    
    void Start () {
        origin = transform.position;       
        startTime = Time.time;
        distance = Vector3.Distance(origin, destiny);
    }
		
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            go = true;
        }

        if (go)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;
            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / distance;
            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(origin, destiny, fracJourney);

            if(transform.position == destiny)
            {
                go = false;
            }
        }
	}
}
