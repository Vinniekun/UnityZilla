using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beach_manager : MonoBehaviour {

	public GameObject wave1;
    public GameObject wave2;

	private float tempo = 2.0f;
	private float intervalo = 0.4f;

	void CriaOnda(){

        int wave = Random.Range(0, 2);
        if (wave == 0)
            Instantiate(wave1);
        else
            Instantiate(wave2);

    }




    void Update () {

		if (Time.time >= tempo) {
			CriaOnda ();
			tempo += intervalo + Random.Range(0.28f, 0.9f);
		}

	}
}
