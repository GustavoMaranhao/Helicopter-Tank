using UnityEngine;
using System.Collections;

public class UIScript : MonoBehaviour {
	private GameObject camera;
	private GameObject magnet;
	private GameObject helicopter;
	
	void Start(){
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		magnet = GameObject.FindGameObjectWithTag ("magnet");
		helicopter = GameObject.FindGameObjectWithTag ("heli");
	}
	
	public void Reset () {
		Application.LoadLevel (Application.loadedLevel);
	}
	
	public void ThrowAgain () {
		camera.transform.position = new Vector3 (helicopter.transform.position.x, helicopter.transform.position.y - 1.5f, -4f);
		helicopter.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
		helicopter.GetComponent<Heli> ().thrown = false;
		magnet.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
		magnet.layer = 0;
	}
}
