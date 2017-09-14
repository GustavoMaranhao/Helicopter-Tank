using UnityEngine;
using System.Collections;

public class ShootingTrigger : MonoBehaviour {

	public bool isShooting;

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "tank")
			isShooting = true;
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "tank")
			isShooting = false;
	}
}
