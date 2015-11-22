using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;
using Arm = Thalmic.Myo.Arm;
using XDirection = Thalmic.Myo.XDirection;
using VibrationType = Thalmic.Myo.VibrationType;


public class cross : MonoBehaviour{

	GameObject img;
	SpriteRenderer sprit;

	public GameObject myo;
	
	public float roll = 0.0f;

	float bps = (float)135 / 60;

	// Use this for initialization
	void Start () {
		img = this.gameObject;
		sprit = img.GetComponent<SpriteRenderer> ();
		sprit.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("it works");
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
		Vector3 zeroRoll = computeZeroRollVector (myo.transform.forward);
		float roll = rollFromZero (zeroRoll, myo.transform.forward, myo.transform.up);

		if ((Time.time % bps < 0.5) && !(roll < 0)) {
			sprit.enabled = true;
			thalmicMyo.Vibrate(VibrationType.Medium);
		} else {
			sprit.enabled = false;
		}
	
	}

	float normalizeAngle (float angle)
	{
		if (angle > 180.0f) {
			return angle - 360.0f;
		}
		if (angle < -180.0f) {
			return angle + 360.0f;
		}
		return angle;
	}
	
	Vector3 computeZeroRollVector (Vector3 forward)
	{
		Vector3 antigravity = Vector3.up;
		Vector3 m = Vector3.Cross (myo.transform.forward, antigravity);
		Vector3 roll = Vector3.Cross (m, myo.transform.forward);
		
		return roll.normalized;
	}
	
	float rollFromZero (Vector3 zeroRoll, Vector3 forward, Vector3 up)
	{
		
		float cosine = Vector3.Dot (up, zeroRoll);
		
		Vector3 cp = Vector3.Cross (up, zeroRoll);
		float directionCosine = Vector3.Dot (forward, cp);
		float sign = directionCosine < 0.0f ? 1.0f : -1.0f;
		
		// Return the angle of roll (in degrees) from the cosine and the sign.
		return sign * Mathf.Rad2Deg * Mathf.Acos (cosine);
	}
}
