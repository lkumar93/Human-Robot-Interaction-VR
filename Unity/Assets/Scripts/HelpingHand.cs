using UnityEngine;
using System.Collections;

public class HelpingHand : MonoBehaviour {
	private int index = 0;

	public GameObject StateMachine;
	int prev_ExecuteFlag = 1;

	public struct moves_struct
	{
		public string cube_tag;
		public string basket_tag;
	}

	public moves_struct[] moves_list;
		
	void Start()
	{
		moves_list = new moves_struct[4];
	}

	void Update()
	{
		StateMachine StateMachineFlags = StateMachine.GetComponent<StateMachine>();
		if (StateMachineFlags.ExecuteFlag != prev_ExecuteFlag && StateMachineFlags.ExecuteFlag == 1)
		{
			prev_ExecuteFlag = StateMachineFlags.ExecuteFlag;
			Invoke("kine_caller", 3);
		}
		else if(StateMachineFlags.ExecuteFlag != prev_ExecuteFlag && StateMachineFlags.ExecuteFlag == 0)
		{
			prev_ExecuteFlag = StateMachineFlags.ExecuteFlag;
			kine_stopper();
		}
	}

	public void add_to_struct(string str1, string str2)
	{	
		moves_list[index].cube_tag = str1;
		moves_list[index].basket_tag = str2;
		index++;

		if (index > 0)
		{
			StateMachine StateMachineFlags = StateMachine.GetComponent<StateMachine>();
			StateMachineFlags.TrainingCompletedFlag = 1;
		}

	}

	public void kine_caller()
	{
		moves_list[index].cube_tag = "Final";
		moves_list[index].basket_tag = "Final";
		GameObject GO = GameObject.Find ("scriptHolder");
		KinematicsCall kine_call = GO.GetComponent<KinematicsCall> ();
		kine_call.StartRobot (moves_list,prev_ExecuteFlag );
	}

	public void kine_stopper()
	{
		GameObject GO = GameObject.Find ("scriptHolder");
		GameObject GO2 = GameObject.Find ("ROBOT_CONTROL");
		myKinematics my_kine_call = GO2.GetComponent<myKinematics> ();
		my_kine_call.StopKinematics ();
		KinematicsCall kine_call = GO.GetComponent<KinematicsCall> ();
		kine_call.StartRobot (moves_list, prev_ExecuteFlag);
	}

}
