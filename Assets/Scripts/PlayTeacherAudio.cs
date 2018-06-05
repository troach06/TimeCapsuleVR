using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTeacherAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(PlayAudio());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator PlayAudio()
    {
        yield return new WaitForSeconds(35);
        GetComponent<AudioSource>().Play();
    }
}
