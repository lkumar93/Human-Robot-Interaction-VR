﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;
using Leap.Unity;

public class Start_Up : MonoBehaviour {

	LeapProvider provider;

	float l_GrabAngle;
	float l_strength;
	Vector3 l_palmPos = new Vector3(0.0f,0.0f,0.0f);
	Vector3 l_pickup_pos = new Vector3(0.0f,0.0f,0.0f);
	Vector3 l_pickup_release_pos = new Vector3(0.0f,0.0f,0.0f);
	bool l_ObjectDetected;
	bool l_ObjectHeld = false;
	GameObject l_pickup;
	string l_pickup_tag_name;
	float l_curr_strength = 30;

	float r_GrabAngle;
	float r_strength;
	Vector3 r_palmPos = new Vector3(0.0f,0.0f,0.0f);
	Vector3 r_pickup_pos = new Vector3(0.0f,0.0f,0.0f);
	Vector3 r_pickup_release_pos = new Vector3(0.0f,0.0f,0.0f);
	bool r_ObjectDetected;
	bool r_ObjectHeld = false;
	GameObject r_pickup;
	string r_pick_up_tag_name;
	float r_curr_strength = 30;

	public GameObject Haptics;
	public GameObject StateMachine;

	public static int HapticPalmFeedback;

	private int count=0;

	void Start () {
		provider = FindObjectOfType<LeapProvider> ();
	}

	void Update ()
	{
		Frame frame = provider.CurrentFrame;
		List<Hand> hands = frame.Hands;
		HandModel hml = FindObjectOfType<HandModel> ();
		StateMachine StateMachineFlags = StateMachine.GetComponent<StateMachine>();
	

		if (hands.Count == 1){
			for (int h = 0; h < hands.Count; h++) {
				Hand hand = hands [h];

				//---------------------------------------------------------------------------------------------
				if (hand.IsLeft) {	
					

					l_strength = hand.GrabStrength;	
					l_palmPos = hml.GetPalmPosition ();

					//Raycast
					if (!l_ObjectHeld && (StateMachineFlags.TrainFlag == 1)) {	
						l_ObjectDetected = false;
						RaycastHit l_hit;
						bool hitornot = Physics.Raycast (l_palmPos, hml.GetPalmNormal (), out l_hit, 20);
						if ( hitornot) {
							l_pickup = l_hit.transform.gameObject ;
							l_ObjectDetected = true;
						}
						// ATTACH TO HAND
						if ((l_ObjectDetected) && (l_strength > l_curr_strength / 100)) {
							l_ObjectHeld = true;
							l_pickup_pos = l_hit.transform.position;
						}
					}
					//KEEP TO Hand
					if (l_ObjectHeld && l_strength > l_curr_strength / 100)
						l_pickup.transform.position = l_palmPos+ (hml.GetPalmNormal ()) / 8;
					else if (l_strength < l_curr_strength / 100){
						if (l_ObjectHeld) {
							l_pickup_release_pos = l_palmPos;
						}
						l_ObjectHeld = false;
					}
				}
				//--------------------------------------------------------------------------------------
				if (hand.IsRight) {	
					r_strength = hand.GrabStrength;	
					r_palmPos = hml.GetPalmPosition ();

					//Apply Raycast only if object not held
					if (!r_ObjectHeld && ( StateMachineFlags.TrainFlag == 1)) {	
						r_ObjectDetected = false;
						RaycastHit r_hit;			
						if (Physics.Raycast (r_palmPos, hml.GetPalmNormal (), out r_hit, 20)) {
							r_pickup = r_hit.transform.gameObject;
							r_ObjectDetected = true;
						}
						// ATTACH TO HAND
						if ((r_ObjectDetected) && (r_strength > r_curr_strength / 100)) {
							r_ObjectHeld = true;
							r_pickup_pos = r_hit.transform.position;
							//Debug.Log ("Right Pick Up"+r_pickup_pos);
							//Debug.Log ("Right Pick Up tag"+	r_pickup.tag);
						}
					}

					//KEEP IN Hand
					if (r_ObjectHeld && r_strength > r_curr_strength / 100)
						r_pickup.transform.position = r_palmPos + (hml.GetPalmNormal ()) / 8;
					else if (r_strength < r_curr_strength / 100) {
						if (r_ObjectHeld) {
							r_pickup_release_pos = r_palmPos;
						}

						r_ObjectHeld = false;
					}
				}

				HapticFeedback HapticFlags = Haptics.GetComponent<HapticFeedback>();

				if(l_ObjectHeld || r_ObjectHeld)
				{

					HapticFlags.HapticPalmFeedback = 1;

				}

				if(l_ObjectHeld == false && r_ObjectHeld == false && HapticFlags.HapticPalmFeedback == 1)
				{
					HapticFlags.HapticPalmFeedback = 0;
					

				}

				//----------------------------------------------------------------------------------------
			}

		}//end of if

	}//End of update
		
}//End of Class









