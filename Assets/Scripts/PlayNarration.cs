using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayNarration : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Play());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Play()
    {
        yield return new WaitForSeconds(5);
        GetComponent<AudioSource>().Play();
    }
}
