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



	void Start () {

        InvokeRepeating("LaunchGem", 0f, Random.Range(minSpawnTime, maxSpawnTime));

	}

	void Update () {
        //if (Input.GetMouseButtonDown(0)) {
        //    LaunchGem();
        //}
	}




    public void LaunchGem() {

        Vector3 angle = new Vector3(0f,0f, Random.Range(minAngle, maxAngle));

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
    }


}
