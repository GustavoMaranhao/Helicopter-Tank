using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Heli : MonoBehaviour {

	private float lift = 0;
	private float dir = 0;
	private bool grab = false;
	private Rigidbody2D Rgd;

	public GameObject camera;
	public GameObject ball;
	public GameObject magnet;

	private int minForce = 11;
	private int maxForce = 27;
	private int ballThrowForce = 5;

	private bool magnetCollision;
	public bool thrown = false;

	// Use this for initialization
	void Start () {
		Rgd = GetComponent<Rigidbody2D> ();
		//lift = minForce;
		Rgd.centerOfMass = new Vector3 (0, 0, 0);

		//Physics2D.IgnoreLayerCollision (8, 8, true);
		Physics2D.IgnoreLayerCollision (9, 10, true);
	}
	
	// Update is called once per frame
	void Update () {
		dir = Input.GetAxis ("Horizontal") * maxForce * 0.15f;
		lift = Input.GetAxis ("Vertical") * maxForce;

		Mathf.Clamp (lift, minForce, maxForce);

		if(Input.GetMouseButtonDown(0) && magnet.GetComponent<Magnet>().magnetCollided){
			ball.transform.position = magnet.transform.position;
			ball.GetComponent<HingeJoint2D>().enabled = true;
			magnet.layer = 10;
			maxForce = 35;
		}

		if (Input.GetMouseButtonDown (1) && ball.GetComponent<HingeJoint2D>().enabled) {
			ball.GetComponent<HingeJoint2D> ().enabled = false;
			maxForce = 27;
			thrown = true;
		}

		if (thrown) {
			Rgd.constraints = RigidbodyConstraints2D.FreezeAll;
			GameObject.FindGameObjectWithTag("magnet").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			camera.transform.position = new Vector3 (ball.transform.position.x, ball.transform.position.y + 1.5f, -4f);
		} else
			camera.transform.position = new Vector3 (Rgd.transform.position.x, Rgd.transform.position.y - 1.5f, -4f);
	}

	void FixedUpdate(){
		Rgd.AddRelativeForce (new Vector2 (0, lift));

		float ang = Vector2.Angle (transform.right, Vector3.up);
		Rgd.AddTorque (-dir + (ang - 90) * 0.1f);
	}
}
