﻿using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class HighlightObject : MonoBehaviour {

    public Vector3 endPosition;
    private Vector3 startPosition;

    public float speed = 1f;

    private float timeStartedLerping;
    private bool isLerping;
    private bool isAborted;

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
            timeStartedLerping += Time.deltaTime*3*0.15*(1-timeStartedLerping^2)*timeStartedLerping+3*0.75*(1-timeStartedLerping)*timeStartedLerping^2+1*timeStartedLerping^3* speed;

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
