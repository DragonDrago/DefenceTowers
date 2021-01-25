using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 1f);// this method called destroy in delay,
        // usual Destroy(gameObject), without delay, but this one is with given interval of time
        // like ==> Destroy(gameObject,delayTime); ==> "float delayTime=1.3f;" soething like that;
	}
	
	
	
}
