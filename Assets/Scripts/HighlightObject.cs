using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class HighlightObject : MonoBehaviour {

    public Vector3 Direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        

	}

    void OnGazeEnter()
    {
        this.transform.localPosition += Direction;
    }

    void OnGazeLeave()
    {
        this.transform.localPosition -= Direction;
    }
}
