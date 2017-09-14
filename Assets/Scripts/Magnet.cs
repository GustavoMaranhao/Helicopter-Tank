using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour {

	public bool magnetCollided = false;

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Roda")
		  magnetCollided = true;

		if (coll.gameObject.tag == "Wall")
			GameObject.FindGameObjectWithTag ("magnet").layer = 0;
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Roda")
			magnetCollided = false;
	}
}
