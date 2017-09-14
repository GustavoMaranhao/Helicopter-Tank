using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shot : MonoBehaviour {

	float score = 0;
	Text scoreText;

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Castle") {
			coll.gameObject.SetActive (false);
			score++;
			scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
			scoreText.text = "Blocks Destroyed: " + score;
		}
	}
}
