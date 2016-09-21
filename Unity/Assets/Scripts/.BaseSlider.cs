using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaseSlider : MonoBehaviour {

	float theta;
	public Transform RobotBase;

	// sets robot base slider to the appropriate starting position 
	void Start () {
		theta = RobotBase.rotation.y;
	}
}
