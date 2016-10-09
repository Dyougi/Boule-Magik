using UnityEngine;
using System.Collections;

public class DynamicTiling : MonoBehaviour {

	// UNITY METHODES

	void Start () {

            GetComponent<Renderer>().material.mainTextureScale = new Vector2(gameObject.transform.localScale.x, -gameObject.transform.localScale.y);
    }
	
}
