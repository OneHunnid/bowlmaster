using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pins : MonoBehaviour {
	public float distToRaise = 40f;
	public float standingThreshold = 3f;

	private Rigidbody rigidBody;

	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
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

	public void RaiseIfStanding () {
		rigidBody.useGravity = false;
		if (IsStanding ()) {
			transform.Translate (new Vector3 (0, distToRaise, 0), Space.World);
		}
	}

	public void Lower () {
		transform.Translate (new Vector3 (0, -distToRaise, 0), Space.World);
		rigidBody.useGravity = true;
	}
}
