using UnityEngine;
using System.Collections;

public class TextureWrap : MonoBehaviour {

    public Texture myTexture;

	// Use this for initialization
	void Start () {

        Debug.Log("setting texture");

        this.GetComponent<Material>().mainTexture = myTexture;
        this.GetComponent<Material>().mainTexture.wrapMode = TextureWrapMode.Repeat;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
