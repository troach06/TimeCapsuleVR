using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDoctorAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Play());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Play()
    {
        yield return new WaitForSeconds(50);
        GetComponent<AudioSource>().Play();
    }
}
