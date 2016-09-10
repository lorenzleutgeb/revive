using UnityEngine;
using System.Collections;

public class SmokeActivator : MonoBehaviour {

    GameObject smoke;
    ParticleSystem particleSystem;
    bool isActive;
    float smokeDelayTime = 0;

	// Use this for initialization
	void Start () {
        smoke = this.transform.Find("Smoke").gameObject;
        particleSystem = smoke.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	    
        /*if(!isActive && smokeDelayTime > 0)
        {
            smokeDelayTime -= Time.deltaTime;
            Debug.Log("Timeleft: " + smokeDelayTime);
        } else if(!isActive)
        {
            if (smoke != null)
            {
                smoke.SetActive(false);
            }
        }*/
	}

    void OnGazeEnter()
    {
        if(smoke != null)
        {
            smoke.SetActive(true);
            isActive = true;
            particleSystem.loop = true;
            particleSystem.Play();
        }
    }

    void OnGazeLeave()
    {
        isActive = false;
        smokeDelayTime = 1;
        particleSystem.loop = false;
    }

    
}
