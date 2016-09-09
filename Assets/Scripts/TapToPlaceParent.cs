using UnityEngine;
using System.Collections;

public class TapToPlaceParent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnSelect()
    {
        this.transform.parent.SendMessage("OnSelect");
    }
}
