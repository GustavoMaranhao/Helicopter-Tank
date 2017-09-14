using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	private GameObject magnet;
	private GameObject helicopter;

	private float startLoc = 0f;
	private float endLoc = 0f;
	private float totalDistance = 0f;
	private bool isCounting = false;
	
	void Start(){
		magnet = GameObject.FindGameObjectWithTag ("magnet");
		helicopter = GameObject.FindGameObjectWithTag ("heli");
	}

	void Update () {
		if (helicopter.GetComponent<Heli> ().thrown) {
			if(!isCounting){
				isCounting = true;
				startLoc = magnet.transform.position.x;
			} else {
				if(GetComponent<Rigidbody2D> ().velocity.magnitude >= 0.5f)
					endLoc = transform.position.x;
				else{
					totalDistance = endLoc - startLoc;
					print (totalDistance);
				}
			}
		}
	}
}
