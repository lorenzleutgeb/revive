using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;
using System;

public class HighlightObject : MonoBehaviour {

    public Vector3 endPosition;
    private Vector3 startPosition;
    private Vector3 maxReachedPosition;

    public float speed = 1.5f;
    public float backspeed = 0.5f;

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
            frameCount += Time.deltaTime * speed;
            timeStartedLerping = (float)(Time.deltaTime * 3 * 0.15 * (1 - (float)Math.Pow(frameCount, 2)) * frameCount + 3 * 0.75 * (1 - frameCount) * Math.Pow(frameCount, 2) + 1 * Math.Pow(frameCount, 3) * speed);

            this.transform.localPosition = Vector3.Lerp(startPosition, endPosition, timeStartedLerping);

            if(timeStartedLerping >= 1.0f)
            {
                isLerping = false;
            }
        } else if(isAborted && isLerping)
        {

            frameCount += Time.deltaTime * backspeed;
            timeStartedLerping = (float)(Time.deltaTime * 3 * 0.15 * (1 - (float)Math.Pow(frameCount, 2)) * frameCount + 3 * 0.75 * (1 - frameCount) * Math.Pow(frameCount, 2) + 1 * Math.Pow(frameCount, 3) * speed);

            this.transform.localPosition = Vector3.Lerp(maxReachedPosition, Vector3.zero, timeStartedLerping);

            if (timeStartedLerping >= 1.0f)
            {
                isLerping = false;
            }

            //startPosition = this.transform.localPosition = Vector3.zero;
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
        maxReachedPosition = this.transform.localPosition;
        frameCount = 0;
        isAborted = true;
        isLerping = true;
        this.transform.localPosition = Vector3.zero;
    }
}
