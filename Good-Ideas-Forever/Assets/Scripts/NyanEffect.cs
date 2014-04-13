using UnityEngine;
using System.Collections;

public class NyanEffect : MonoBehaviour {

	public Texture [] nyan;
	LineRenderer rend;
	int count = 0;
	int secondcounter = 0;

	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponent<LineRenderer>();
		rend.material.mainTexture = nyan[0];
	}
	
	// Update is called once per frame
	void Update () {
		if(count%5==0)
			secondcounter++;
		rend.material.mainTexture = nyan[secondcounter%9];
		count++;
	
	}
}
