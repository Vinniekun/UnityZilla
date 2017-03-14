using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

	private float velocidade = 15.0f;
	public GameObject background;


	void Morte (){
		Destroy (gameObject);
	}

	public void Movement(){
		transform.position += Vector3.left * velocidade * Time.deltaTime;
	}

	void Update () {

		Movement ();

		if (transform.position.x < -15)
			Morte ();
	}
}
