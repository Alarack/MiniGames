using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimHelper : MonoBehaviour {

    public enum AnimationEffect {
        Bounce,
        TweakEyes,
        BlingBeat,
        None
    }




    public List<AnimationData> animationData = new List<AnimationData>();
    public AnimationEffect animationEffect;
   // public string animationTrigger;


    private Animator _myAnim;


    [System.Serializable]
    public class AnimationData {
        public AnimationEffect effect;
        public string animationTrigger;


    }


    void Awake() {
        _myAnim = GetComponent<Animator>();
    }


    public void AnimEffect() {

        for (int i = 0; i < animationData.Count; i++) {
            if(animationEffect == animationData[i].effect) {
                _myAnim.SetTrigger(animationData[i].animationTrigger);
                break;
            }
        }



        //switch (effect) {
        //    case AnimationEffect.Bounce:
        //        _myAnim.SetTrigger("ButtonClick");

        //        break;
        //}

    }

    // Update is called once per frame
    void Update () {
		
	}
}
