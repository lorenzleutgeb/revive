using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;
using System;

public class HighlightObject : MonoBehaviour {

    public Vector3 endPosition;
    private Vector3 startPosition;

    public float speed = 1f;

    private float timeStartedLerping;
    private bool isLerping;
    private bool isAborted;
    private float frameCount;

	// Use this for initialization
	void Start ()
    {
        startPosition = this.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isLerping && !isAborted)
        {
            frameCount += Time.deltaTime;
            timeStartedLerping = (float)(Time.deltaTime * 3 * 0.15 * (1 - (float)Math.Pow(frameCount, 2)) * frameCount + 3 * 0.75 * (1 - frameCount) * Math.Pow(frameCount, 2) + 1 * Math.Pow(frameCount, 3) * speed);

            this.transform.localPosition = Vector3.Lerp(startPosition, endPosition, timeStartedLerping);

            if(timeStartedLerping >= 1.0f)
            {
                isLerping = false;
            }
        } else if(isAborted)
        {
            startPosition = this.transform.localPosition = Vector3.zero;
        }
	}

    void StartLerping()
    {
        frameCount = 0;
        isAborted = false;
        isLerping = true;
        timeStartedLerping = 0;
    }

    void OnGazeEnter()
    {
        StartLerping();
    }

    void OnGazeLeave()
    {
        isAborted = true;
        this.transform.localPosition = Vector3.zero;
    }
}
