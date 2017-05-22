using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
	public int lastStandingCount = -1;
	public Text standingDisplay;
	public GameObject pinSet;

	private bool ballEnteredBox = false;
	private float lastChangedTime;
	private Ball ball;

	void Start () {
		ball = GameObject.FindObjectOfType<Ball> ();
	}
	
	void Update () {
		standingDisplay.text = CountStanding ().ToString ();

		if (ballEnteredBox) {
			UpdateStandingCountAndSettle ();
		}
	}

	public void RaisePins () {
		foreach (Pins pin in GameObject.FindObjectsOfType<Pins>()) {
			pin.RaiseIfStanding();
		}
	}

	public void LowerPins () {
		foreach (Pins pin in GameObject.FindObjectsOfType<Pins>()) {
			pin.Lower();
		}
	}

	public void RenewPins () {
		GameObject newPins = Instantiate (pinSet);
		newPins.transform.position += new Vector3 (0, 20, 0);
	}

	void UpdateStandingCountAndSettle () {
		int currentStanding = CountStanding ();

		if (currentStanding != lastStandingCount) {
			lastChangedTime = Time.time;
			lastStandingCount = currentStanding;
			return;
		}

		float settleTime = 3f; // How long to wait to consider pins settled

		if ((Time.time - lastChangedTime) > settleTime) {
			PinsHaveSettled ();
		}
	}

	void PinsHaveSettled () {
		ball.Reset ();
		lastStandingCount = -1; // Indicates pins have settled, and ball not back in box
		standingDisplay.color = Color.green;
	}

	int CountStanding() {
		int standing = 0;

		foreach (Pins pin in GameObject.FindObjectsOfType<Pins>()) {
			if (pin.IsStanding ()) {
				standing++;
			}
		}

		return standing;
	}



	void OnTriggerEnter(Collider collider) {
		GameObject thingHit = collider.gameObject;

		if (thingHit.GetComponent<Ball>()) {
			ballEnteredBox = true;
			standingDisplay.color = Color.red;
		}
	}
}
