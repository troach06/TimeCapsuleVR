//using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weelco.SpeechControl;

public class LoadNextEvent : MonoBehaviour {
    public GameObject speechRecognizer, teacher, son, doctor, remote;
    public AudioClip teacherClip2, sonClip2, doctorClip2;
    bool teacherTalked, sonTalked, doctorTalked;
    OVRInput.Controller controller;
    // Use this for initialization
    void Start()
    {

        StartCoroutine(LoadEvent());
        if (!Application.isEditor)
        {
            controller = OVRInput.Controller.RTrackedRemote;
        }
    }
	
	// Update is called once per frame
	void Update () {
	}

    public IEnumerator LoadEvent() {

        if (SceneManager.GetActiveScene().name == "Title") {
            yield return new WaitUntil(() => OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger));
            Camera.main.GetComponent<OVRScreenFade>().FadeOut();
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("Kid");
        }
            if (SceneManager.GetActiveScene().name == "Kid")
        {
            yield return new WaitForSeconds(3);
            yield return new WaitUntil(() => remote.GetComponent<PullObjects>().grabbedObjects.Count == 10);
            yield return new WaitForSeconds(3);
            Camera.main.GetComponent<OVRScreenFade>().FadeOut();
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("School");
        }
        if (SceneManager.GetActiveScene().name == "School")
        {
            if (!teacherTalked)
            {
                yield return new WaitForSeconds(45);
                teacherTalked = true;
            }
            speechRecognizer.GetComponent<SpeechControl>().StartRecord();
            yield return new WaitForSeconds(5);
            speechRecognizer.GetComponent<SpeechControl>().StopRecord();

            if (!speechRecognizer.GetComponent<Example>().yesDetected)
            {
                StartCoroutine(LoadEvent());
            }
            else
            {
                teacher.GetComponent<AudioSource>().PlayOneShot(teacherClip2);
                yield return new WaitForSeconds(20);
                Camera.main.GetComponent<OVRScreenFade>().FadeOut();
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene("College");
            }
        }
        if (SceneManager.GetActiveScene().name == "College")
        {
            yield return new WaitForSeconds(120);
            Camera.main.GetComponent<OVRScreenFade>().FadeOut();
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("Office");
        }
        if (SceneManager.GetActiveScene().name == "Office")
        {
            yield return new WaitUntil(() => MovementDetection.movementCompleted);
            Camera.main.GetComponent<OVRScreenFade>().FadeOut();
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("House");
        }
        if (SceneManager.GetActiveScene().name == "House")
        {
            if (!sonTalked)
            {
                yield return new WaitForSeconds(35);
                sonTalked = true;
            }
            speechRecognizer.GetComponent<SpeechControl>().StartRecord();
            yield return new WaitForSeconds(5);
            speechRecognizer.GetComponent<SpeechControl>().StopRecord();

            if (!speechRecognizer.GetComponent<Example>().sonResponseDetected)
            {
                StartCoroutine(LoadEvent());
            }
            else
            {
                son.GetComponent<AudioSource>().PlayOneShot(sonClip2);
                yield return new WaitForSeconds(10);
                Camera.main.GetComponent<OVRScreenFade>().FadeOut();
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene("Hospital");
            }
        }
        if (SceneManager.GetActiveScene().name == "Hospital")
        {
            if (!doctorTalked)
            {
                yield return new WaitForSeconds(50);
                doctorTalked = true;
            }
            speechRecognizer.GetComponent<SpeechControl>().StartRecord();
            yield return new WaitForSeconds(5);
            speechRecognizer.GetComponent<SpeechControl>().StopRecord();

            if (!speechRecognizer.GetComponent<Example>().doctorResponseDetected)
            {
                StartCoroutine(LoadEvent());
            }
            else
            {
                doctor.GetComponent<AudioSource>().PlayOneShot(doctorClip2);
                yield return new WaitForSeconds(20);
                Camera.main.GetComponent<OVRScreenFade>().FadeOut();
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene("End");
            }
        }
        if (SceneManager.GetActiveScene().name == "End")
        {
            yield return new WaitForSeconds(60);
            Camera.main.GetComponent<OVRScreenFade>().FadeOut();
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("Title");
        }
    }
}
