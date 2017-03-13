using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : SpecialEffect {



	void Update () {
		
	}

    public override void Effect() {
       for(int i= 0; i < MainHUD.mainHUD.gems.Count; i++) {
            Rigidbody2D gemBody = MainHUD.mainHUD.gems[i].GetComponent<Rigidbody2D>();
            gemBody.gravityScale = .05f;
            gemBody.velocity *= .1f;
            gemBody.gameObject.layer = 10;
        }
    }
}
