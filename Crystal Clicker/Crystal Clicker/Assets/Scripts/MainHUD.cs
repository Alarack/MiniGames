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

    public int maxGemCount = 15;

    //[HideInInspector]
    //public int gemCount;

    public List<Gem> gems = new List<Gem>();

	void Awake () {
		
        if(mainHUD == null) {
            mainHUD = this;
        }
        else {
            Destroy(gameObject);
        }

	}

	void Update () {
		
	}
}
