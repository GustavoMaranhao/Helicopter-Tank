using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {

	GameObject camera;
	GameObject tank;
	GameObject trigger;
	GameObject turret;
	GameObject shot;

	private float maxMotorSpeed = 2000;
	private float motorSpeedModifier = 2000;
	float smooth = 2.0f;
	float tiltAngle = 30.0f;

	public WheelJoint2D rodaT, rodaD, rodaT2, rodaD2;  // liga nao as rodas, mas o wheeljoint do inspector!
	JointMotor2D motorT, motorT2;
	float aceleracao, aceleracao2;

	bool isShooting = false;
	bool shootingOnce = false;

	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		tank = GameObject.FindGameObjectWithTag ("tank");
		trigger = GameObject.Find ("ShootingTrigger");
		shot = GameObject.Find ("Shot");
		turret = GameObject.FindGameObjectWithTag ("Turret");
		motorT = new JointMotor2D ();

		Physics2D.IgnoreLayerCollision (11, 11, true);
		Physics2D.IgnoreLayerCollision (11, 12, true);
		Physics2D.IgnoreLayerCollision (13, 14, true);

		camera.transform.position = new Vector3(tank.transform.position.x+2f, tank.transform.position.y+1f, -3f);
	}
	
	// Update is called once per frame
	void Update () {
		isShooting = trigger.GetComponent<ShootingTrigger> ().isShooting;

		if (Input.GetKey(KeyCode.R))
			transform.localRotation = new Quaternion(0,0,0,1);
		aceleracao = Input.GetAxis ("Horizontal");  // devolve valor de -1 a 1, se nao aperta nada devolve 0
		aceleracao2 += Mathf.Abs(Input.GetAxis ("Horizontal")*0.01f);
		aceleracao2 = Mathf.Clamp (aceleracao2, -1f, 1f);
		motorT.motorSpeed = -aceleracao*motorSpeedModifier*aceleracao2;
		motorT2.motorSpeed = -aceleracao*motorSpeedModifier*aceleracao2;
		if (isShooting) {
			if(!shootingOnce){
				shootingOnce = true;
				GameObject[] Belt = GameObject.FindGameObjectsWithTag("TankBelt");
				foreach (GameObject go in Belt)
					go.GetComponent<Rigidbody2D>().mass = 10;

				GameObject[] InvisWall = GameObject.FindGameObjectsWithTag("InvisWall");
				foreach (GameObject go2 in InvisWall)
					go2.SetActive(false);
			}
			else{
				float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
				tiltAroundX = Mathf.Clamp(tiltAroundX,0,tiltAngle);
				Quaternion target = Quaternion.Euler (0, 0, tiltAroundX);
				turret.gameObject.transform.rotation = Quaternion.Slerp(turret.gameObject.transform.rotation, target,
					                                      				Time.deltaTime * smooth);

				shot.GetComponent<SpriteRenderer>().enabled = true;
				camera.transform.position = new Vector3(shot.transform.position.x+2f, shot.transform.position.y+1f, -5f);

				if(Input.GetMouseButtonDown(0)){
					shot.GetComponent<Rigidbody2D>().isKinematic = false;
					shot.GetComponent<Rigidbody2D>().mass = 5;
					shot.GetComponent<Rigidbody2D>().AddForce(turret.transform.right * 3000);
				}
			}
		}
		if (aceleracao == 0)
			aceleracao2 = 0;
		if ((Mathf.Abs(aceleracao) > 0.9f) && !isShooting)
		{
			camera.transform.position = new Vector3(tank.transform.position.x+2f, tank.transform.position.y+1f, -3f);
			rodaT.useMotor = true;
			rodaD.useMotor = true;
			motorT.maxMotorTorque = maxMotorSpeed;  // torque do motor
			rodaT.motor = motorT;
			rodaD.motor = motorT;
			rodaT2.useMotor = true;
			rodaD2.useMotor = true;
			motorT2.maxMotorTorque = maxMotorSpeed;  // torque do motor
			rodaT2.motor = motorT;
			rodaD2.motor = motorT;
		}
		else
		{
			rodaT.useMotor= false;
			rodaD.useMotor = false;
			rodaT2.useMotor= false;
			rodaD2.useMotor = false;
		}
	}
}
