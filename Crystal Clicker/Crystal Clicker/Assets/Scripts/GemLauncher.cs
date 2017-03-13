using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemLauncher : MonoBehaviour {

    public GameObject gem;
    public float minSpeed;
    public float maxSpeed;
    public float minRotationSpeed;
    public float maxRotationSpeed;
    public float minSize;
    public float maxSize;

    public float minSpawnTime;
    public float maxSpawnTime;
    public float maxAngle;
    public float minAngle;

    public float difficultyTimeIncrament = 10f;

    public int burst;


    private float _timer;
    private float _speedTimer;


	void Start () {

        //InvokeRepeating("LaunchGem", 0f, Random.Range(minSpawnTime, maxSpawnTime));
        LaunchGem(1);
	}

	void Update () {

        _timer += Time.deltaTime;
        _speedTimer += Time.deltaTime;

        if (_timer > RandomizeSpawnTime()) {

            if(MainHUD.mainHUD.gems.Count < MainHUD.mainHUD.maxGemCount)
                LaunchGem(burst);

            _timer = 0f;
        }

        if(_speedTimer > difficultyTimeIncrament) {
            IncreaseSpawnSpeed();
            _speedTimer = 0f;
        }
	}


    public void IncreaseSpawnSpeed() {
        minSpawnTime *= 0.95f;
        maxSpawnTime *= 0.95f;
        burst++;

        if(burst > MainHUD.mainHUD.maxGemCount) {
            burst--;
        }
    }

    public float RandomizeSpawnTime() {
        return Random.Range(minSpawnTime, maxSpawnTime);
    }

    public void LaunchGem(int number) {

        for (int i = 0; i < number; i++) {
            Vector3 angle = new Vector3(0f, 0f, Random.Range(minAngle, maxAngle));

            transform.localRotation = Quaternion.Euler(angle);

            GameObject activeGem = Instantiate(gem, transform.position, Quaternion.identity);
            Gem gemScript = activeGem.GetComponent<Gem>();

            int randColor = Random.Range(0, 4); //TODO: find a way to make this not hardcoded
            Constants.GemColor color = (Constants.GemColor)randColor;

            gemScript.gemColor = color;

            activeGem.transform.localScale *= Random.Range(minSize, maxSize);
            Rigidbody2D gemBody = activeGem.GetComponent<Rigidbody2D>();
            gemBody.velocity = transform.up * Random.Range(minSpeed, maxSpeed);
            gemBody.angularVelocity = Random.Range(minRotationSpeed, maxRotationSpeed);

            //MainHUD.mainHUD.gemCount++;
            MainHUD.mainHUD.gems.Add(gemScript);

        }
    }

}
