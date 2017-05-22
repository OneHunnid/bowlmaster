﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pins : MonoBehaviour {

	public float standingThreshold = 3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		IsStanding ();
	}

	public bool IsStanding() {
		Vector3 rotationInEuler = transform.rotation.eulerAngles;
		float tiltInX = Mathf.Abs(270 - rotationInEuler.x);
		float tiltInZ = Mathf.Abs(rotationInEuler.z);

		if (tiltInX < standingThreshold && tiltInZ < standingThreshold) {
			return true;
		} else {
			return false;
		}
	}
}
