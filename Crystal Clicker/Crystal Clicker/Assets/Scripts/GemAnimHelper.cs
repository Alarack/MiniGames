using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemAnimHelper : MonoBehaviour {

    private Gem _parent;


	void Awake () {
        _parent = GetComponentInParent<Gem>();
	}

    public void SendAnimEvent() {
        _parent.RecieveAnimEvent(_parent.SpawnFragments);
    }
	

}
