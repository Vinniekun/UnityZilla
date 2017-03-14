using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour {

	void Actions(){
		if (Input.GetButtonDown ("Jump"))
            SceneManager.LoadScene("Game");
        
        else if (Input.GetButtonDown ("Quit")) {
            Application.Quit();
		}
	}

	void Update () {
		Actions ();
	}
}
