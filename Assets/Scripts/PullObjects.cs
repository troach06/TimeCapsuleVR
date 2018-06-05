using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PullObjects : MonoBehaviour
{
    LineRenderer line;
    bool canGrab = true;
    bool canMove;
    public Text discoverText;
    OVRInput.Controller controller;
    Transform obj;
    public float speed;
    public GameObject discover;
    public float throwSpeed;
    public List <string> grabbedObjects;
    // Use this for initialization
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        grabbedObjects = new List<string>();
        if (!Application.isEditor)
        {
            controller = OVRInput.Controller.RTrackedRemote;
            //if (OVRInput.GetActiveController() == OVRInput.Controller.LTrackedRemote)
            //{
            //    controller = OVRInput.Controller.LTrackedRemote;
            //    rightRemote.SetActive(false);
            //}
            //else if (OVRInput.GetActiveController() == OVRInput.Controller.RTrackedRemote)
            //{
            //    controller = OVRInput.Controller.RTrackedRemote;
            //    leftRemote.SetActive(false);
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetectGrab();
        DetectObjects();
        MoveObject();
    }


    public void MoveObject()
    {
        float step = speed * Time.deltaTime;
        if (transform.GetChild(0).tag == "Object")
        {
            if (obj.transform.localPosition == new Vector3(0, 0, 0.5f))
            {
                canMove = false;
            }
            if (canMove)
            {
                obj.LookAt(transform);
                obj.localPosition = Vector3.MoveTowards(obj.localPosition, new Vector3(0, 0, 0.5f), step);
            }
        }
    }
    public void DetectGrab()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                canGrab = true;
                if (transform.GetChild(0).tag == "Object")
                {
                    obj.GetComponent<Rigidbody>().isKinematic = false;
                    obj.parent = null;
                    TossObject(obj.GetComponent<Rigidbody>());
                }
            }
        }
        else
        {
            if (!OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                canGrab = true;
                if (transform.GetChild(0).tag == "Object")
                {
                    obj.GetComponent<Rigidbody>().isKinematic = false;
                    obj.parent = null;
                    TossObject(obj.GetComponent<Rigidbody>());
                }
            }
        }
    }

    public void TossObject(Rigidbody rigidBody)
    {
        if (!Application.isEditor)
        {
            rigidBody.AddForce(OVRInput.GetLocalControllerAcceleration(controller) * throwSpeed, ForceMode.Impulse);
        }
    }

    public void DetectObjects()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            if (Application.isEditor)
            {
                Debug.DrawRay(ray.origin, ray.direction * 10000f, Color.green);
            }
            if (hit.collider.gameObject.tag == "Object" && canGrab)
            {
                print("Object");
                line.enabled = true;

                if (Application.isEditor)
                {
                    if (Input.GetKeyDown(KeyCode.LeftControl) && canGrab)
                    {
                        canGrab = false;
                        hit.transform.parent = transform;
                        hit.transform.SetSiblingIndex(0);
                        hit.collider.GetComponent<Rigidbody>().isKinematic = true;
                        obj = hit.transform;
                        canMove = true;
                        if (SceneManager.GetActiveScene().name == "Kid")
                        {
                            if (!grabbedObjects.Contains(obj.name))
                            {
                                discover.SetActive(true);
                                StartCoroutine(ResetDiscoverText());
                                GetComponent<AudioSource>().Play();
                                grabbedObjects.Add(obj.name);
                                discoverText.text = "Discovered " + obj.name + "\n" + grabbedObjects.Count + "/10";

                            }
                        }
                        }
                    }
                else if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && canGrab)
                    {
                        canGrab = false;
                        hit.transform.parent = transform;
                        hit.transform.SetSiblingIndex(0);
                        hit.collider.GetComponent<Rigidbody>().isKinematic = true;
                        obj = hit.transform;
                        canMove = true;
                    if (SceneManager.GetActiveScene().name == "Kid")
                    {
                        if (!grabbedObjects.Contains(obj.name))
                        {
                            discover.SetActive(true);
                            StartCoroutine(ResetDiscoverText());
                            GetComponent<AudioSource>().Play();
                            grabbedObjects.Add(obj.name);
                            discoverText.text = "Discovered " + obj.name + "\n" + grabbedObjects.Count + "/10";

                        }
                    }
                    }
                }
            else
            {
                line.enabled = false;
            }
        }
        else
        {
            line.enabled = false;
        }
    }

    IEnumerator ResetDiscoverText()
    {
        yield return new WaitForSeconds(3);
        discover.SetActive(false);
    }
}
