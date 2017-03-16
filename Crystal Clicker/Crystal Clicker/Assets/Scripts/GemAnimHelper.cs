using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemAnimHelper : MonoBehaviour {



    public ButtonAnimHelper.AnimationEffect effect;


    private Gem _parent;
    private Animator _anim;


	void Awake () {
        _parent = GetComponentInParent<Gem>();
        _anim = GetComponentInParent<Animator>();
	}

    void Start() {

        switch (effect) {
            case ButtonAnimHelper.AnimationEffect.TweakEyes:
                _anim.SetTrigger("TweakEyes");
                break;

            case ButtonAnimHelper.AnimationEffect.BlingBeat:
                _anim.SetTrigger("BlingBeat");
                break;
        }

    }

    public void SendAnimEvent() {
        _parent.RecieveAnimEvent(_parent.SpawnFragments);
    }
	

}
