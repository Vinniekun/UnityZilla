using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

    public float item_chance = 62.5f;

    public GameObject[] itemsPrefabs;

	void Start () {
        float time = 3f + Random.Range(0, 5);
        Invoke("GenerateItem", time);
        InvokeRepeating("CheckIfItemAppears", time + 5.0f, 5.0f);

    }

    public void GenerateItem(){
        string[] items = new string[2] { "beer", "grape" };
      
        int key = Random.Range(0, 2);

        Vector3 position = new Vector3(16, Random.Range(-4, 0.5f), 0);
        Instantiate(itemsPrefabs[key], position, Quaternion.identity);
    }

    public void CheckIfItemAppears() { 
        if (Random.Range(0,100) < item_chance) { 
            GenerateItem();
            ReduceItemChance();
        }
    }	
	
    public void ReduceItemChance()
    {
        item_chance -= 1;
        if (item_chance < 1)
            item_chance = 1;
    }


}
