using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHUD : MonoBehaviour {

    public static MainHUD mainHUD;
    public Transform redEnergyAttractor;
    public Transform greenEnergyAttractor;
    public Transform blueEnergyAttractor;
    public Transform yellowEnergyAttractor;
    public Transform mainAttractor;


	void Awake () {
		
        if(mainHUD == null) {
            mainHUD = this;
        }
        else {
            Destroy(gameObject);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
