using UnityEngine;
using System.Collections;

public class KinematicsCall : MonoBehaviour
{
	public Transform Tip;
	public Transform Inter;
    GameObject robot_controller;
    myKinematics kine_func;
	int count = 0;
	int start_time;
	float modulo;
	int state = 1;
	Transform cubes;
	Transform baskets;
	int last_time = 0;
	bool robot = false;
	int flag = 0;
	bool stop_robot = false;
	int moves_ct = 0;
	HelpingHand.moves_struct[] move_arr;

    public void StartRobot(HelpingHand.moves_struct[] arr, int flag)
    {
		move_arr = arr;
		moves_ct = move_arr.Length;
		Debug.Log ("MoveNum " + moves_ct);
		this.flag = flag;
		robot_controller = GameObject.Find("ROBOT_CONTROL");
        kine_func = (myKinematics)robot_controller.GetComponent(typeof(myKinematics));
		start_time = Mathf.CeilToInt(Time.time);
		robot = true;
    }

	void Update(){
		if (flag == 1) {
			if (robot) {
				if (count == 0 || ((Mathf.CeilToInt (Time.time) - start_time) % 6 == 0 && count < ((moves_ct - 1) * 3) + 1 && Mathf.CeilToInt (Time.time) != last_time)) {
					if (move_arr [count / 3].cube_tag == "Final") {
						flag = 0;
					}
					CallRobot ();
					last_time = Mathf.CeilToInt (Time.time);
					count ++;
				}
				if (count % 3 != 1) {
					cubes.transform.position = Tip.position;
				}
			}
		}
	}

	void CallRobot()
	{
		if (count % 3 == 0) {
			cubes = GameObject.FindGameObjectWithTag (move_arr [count/3].cube_tag + "Robot").GetComponent<Transform> ();
			kine_func.StartKinematics (cubes.position.x, cubes.position.y, cubes.position.z, false, cubes.tag, count);
		} else if (count % 3 == 1) {
			kine_func.StartKinematics (cubes.position.x, Inter.position.y, cubes.position.z, true, cubes.tag, count);
		} else if (count % 3 == 2) {
			baskets = GameObject.FindGameObjectWithTag (move_arr [count/3].basket_tag + "Robot").GetComponent<Transform> ();
			kine_func.StartKinematics (baskets.position.x, baskets.position.y, baskets.position.z, true, cubes.tag, count);
		}		
	}
}
