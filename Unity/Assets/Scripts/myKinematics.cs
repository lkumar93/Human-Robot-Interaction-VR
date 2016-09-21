using UnityEngine;
using System.Collections;

public class myKinematics : MonoBehaviour
{

    public Transform baseFrame;
    public Transform lowerArm;
    public Transform upperArm;
    public Transform baseReference;
	public Transform Tip;
	public Transform elbow_target;
	public Transform hand_target;
	public Transform elbow_joint;
    private Vector3 target;
	private float inTime1 = 0, inTime2 = 3;
	GameObject cube;
	private bool move_cube = true;

    public void StartKinematics(float inputX, float inputY, float inputZ, bool move_cube, string tag, int count)
    {

        if (inputX != 0 && inputY != 0 && inputZ != 0)
        {
			inTime1 = 2;
			inTime2 = 3;
			this.move_cube = move_cube;
			target = new Vector3(inputX, inputY, inputZ);
			cube = GameObject.FindGameObjectWithTag (tag);
			if (count % 3 != 1) {
				StartCoroutine (RotateBaseToTarget ());
				Invoke ("ArmKinematics", 2);
			} else
				ArmKinematics();
		}
    }

	public void StopKinematics(){
		Debug.Log ("Stop CoRoutines");
		StopAllCoroutines ();
	}

	public void ArmKinematics()
	{
		Debug.Log ("Time.time =" + Time.time);
		StartCoroutine(RobotIK());
	}

    IEnumerator RotateBaseToTarget()
    {
		elbow_target.position = new Vector3(elbow_joint.position.x-1, elbow_joint.position.y + 10, elbow_joint.position.z);
        Quaternion _lookRotation;
        Vector3 _direction;

        _direction = (target - baseFrame.position);

        _lookRotation = Quaternion.LookRotation(_direction);
        Quaternion start = baseFrame.rotation;

        for (float t = 0f; t < 1f; t += Time.deltaTime / inTime1)
        {
			baseFrame.localRotation = Quaternion.Euler(0f, (Quaternion.Lerp(start, _lookRotation, t)).eulerAngles.y, 0f);
			hand_target.position = Tip.position;
			elbow_target.position = new Vector3(elbow_joint.position.x-1, elbow_joint.position.y + 10, elbow_joint.position.z);
            yield return null;
        }

		yield return null;
    }

	IEnumerator RobotIK(){
		float t = 0f;
		for (t = 0f; t < 1f; t += Time.deltaTime / inTime2) {
			hand_target.position = Vector3.Lerp (hand_target.position, target, t);
			yield return null;
		}
		yield return null;
	}
}
