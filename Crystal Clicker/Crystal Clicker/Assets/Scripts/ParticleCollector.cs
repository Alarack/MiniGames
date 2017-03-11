using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollector : MonoBehaviour {

    public int score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void AddBar() {
        score++;
    }




    //void OnTriggerEnter2D(Collider2D other) {
    //    Debug.Log(other + " 2d");
    //}

    //void OnParticleCollision(GameObject other) {
    //    Debug.Log(other + " Part");
    //}
}
