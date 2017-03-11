﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gem : MonoBehaviour {

    private Animator _myAnim;
    public GameObject deathEffect;

    public Constants.GemColor gemColor;
    public SpriteManager[] sprites;

    public GameObject[] fragments;
    public int maxFragments;
    public int minFragments;

    private SpriteRenderer _mySpriteRenderer;
    private EnergyGuage mainGuage;

    [Serializable]
    public class SpriteManager {
        public Constants.GemColor fragmentColor;
        public Sprite sprite;
    }


    void Awake () {
        _myAnim = GetComponentInChildren<Animator>();
        _mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start() {
        mainGuage = MainHUD.mainHUD.mainAttractor.GetComponentInParent<EnergyGuage>();
        for (int i = 0; i < sprites.Length; i++) {

            if (sprites[i].fragmentColor == gemColor) {
                _mySpriteRenderer.sprite = sprites[i].sprite;
                break;
            }
        }

    }
	
	void Update () {
		if(transform.position.y < -6f) {
            Missed();
            Destroy(gameObject);
        }
	}


    private void OnMouseDown() {
        //Debug.Log("Clicked");
        _myAnim.SetTrigger("Click");
    }


    public void Explode() {
        GameObject particles = Instantiate(deathEffect, transform.position, Quaternion.identity) as GameObject;

        if(particles.GetComponent<ParticleSeek>() != null) {

            Vector3 conversion = ((RectTransform)(MainHUD.mainHUD.redEnergyAttractor)).TransformPoint(MainHUD.mainHUD.redEnergyAttractor.position);

            GameObject empty = new GameObject();
            empty.transform.localPosition = conversion;

            empty.AddComponent<DestroyAfterDuration>();
            empty.GetComponent<DestroyAfterDuration>().duration = 5f;

            particles.GetComponent<ParticleSeek>().SetTarget(empty.transform);
        }

        Destroy(gameObject);
    }

    public void SpawnFragments() {
        int numFrags = UnityEngine.Random.Range(minFragments, maxFragments);
        int index = UnityEngine.Random.Range(0, fragments.Length - 1);

        for (int i = 0; i < numFrags; i++) {

            GameObject fragement = Instantiate(fragments[index], transform.position, transform.rotation) as GameObject;
            GemFragment fragScript = fragement.GetComponent<GemFragment>();

            fragScript.fragmentColor = gemColor;

            //fragScript.SetTarget(SetAttractor(gemColor));
            fragScript.SetTarget(MainHUD.mainHUD.mainAttractor);

            float randomDelay = UnityEngine.Random.Range(.5f, 1f);
            fragScript.seekDelay = randomDelay;

        }

        Destroy(gameObject);
    }


    public void RecieveAnimEvent(Action animEvent) {
        animEvent();
    }

    public void Missed() {
        mainGuage.AdjustFill(-.02f);
        mainGuage.numMissed++;
        mainGuage.missedText.text = mainGuage.numMissed + " Missed";
    }



    private Transform SetAttractor(Constants.GemColor color) {

        switch (color) {
            case Constants.GemColor.Blue:

                return MainHUD.mainHUD.blueEnergyAttractor;

            case Constants.GemColor.Red:

                return MainHUD.mainHUD.redEnergyAttractor;

            case Constants.GemColor.Green:

                return MainHUD.mainHUD.greenEnergyAttractor;

            case Constants.GemColor.Yellow:

                return MainHUD.mainHUD.yellowEnergyAttractor;

            default:

                return MainHUD.mainHUD.redEnergyAttractor;
        }


    }
}