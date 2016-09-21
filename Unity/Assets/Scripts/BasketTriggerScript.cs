using UnityEngine;
using System.Collections;

public class BasketTriggerScript : MonoBehaviour {

	private string basket_tag; 
	private string obj_tag;
	Collision other2;
	void OnCollisionEnter(Collision other)
	{
			other2 = other;
			basket_tag = this.tag;
			obj_tag = other.gameObject.tag;
			GameObject GO = GameObject.Find ("scriptHolder");
			HelpingHand Helper = GO.GetComponent<HelpingHand> ();
			if (obj_tag != "Untagged" && obj_tag != null && obj_tag != ""){
			Helper.add_to_struct (obj_tag, basket_tag);
			other.gameObject.SetActive (false);
		}
	}
}
