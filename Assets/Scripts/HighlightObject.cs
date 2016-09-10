using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class HighlightObject : MonoBehaviour {

    public Vector3 endPosition;
    private Vector3 startPosition;

    public float speed = 1f;

    private float timeStartedLerping;
    private bool isLerping;

	// Use this for initialization
	void Start ()
    {
        startPosition = this.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isLerping)
        {
            timeStartedLerping += Time.deltaTime * speed;

            this.transform.localPosition = Vector3.Lerp(startPosition, endPosition, timeStartedLerping);

            if(timeStartedLerping >= 1.0f)
            {
                isLerping = false;
            }
        }
	}

    void StartLerping()
    {
        isLerping = true;
        timeStartedLerping = 0;
    }

    void OnGazeEnter()
    {
        StartLerping();
    }

    void OnGazeLeave()
    {
        this.transform.localPosition = Vector3.zero;
    }
}
