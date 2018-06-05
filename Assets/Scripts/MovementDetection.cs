using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDetection : MonoBehaviour {
    public static bool movementCompleted;
    public int moveAmount;
    public bool firstCheck, secondCheck;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FirstCheckpoint" && !firstCheck)
        {
            moveAmount++;
            firstCheck = true;
        }
        if (other.gameObject.name == "SecondCheckpoint" && !secondCheck)
        {
            moveAmount++;
            secondCheck = true;
        }
        if (other.gameObject.name == "ProjectManager" && moveAmount == 2)
        {
            movementCompleted = true;
        }
    }
}
