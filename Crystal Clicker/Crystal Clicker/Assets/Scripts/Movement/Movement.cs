using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {


    public enum MoveType {
        None,
        Erratic,
        Smooth
    }


    public MoveType movetype;
    public float speed;
    public float fuel;

    private Rigidbody2D _myBody;

	void Awake () {
        _myBody = GetComponent<Rigidbody2D>();
	}

    void Update() {

        if(fuel > 0) {
            fuel -= Time.deltaTime;
        }

    }

	void FixedUpdate () {

        switch (movetype) {
            case MoveType.Erratic:
                Vector2 randomCircle = Random.insideUnitCircle * speed;
                Vector3 offset = new Vector3(transform.position.x * randomCircle.x, transform.position.y * randomCircle.y, 0f);
                transform.position += offset * Time.deltaTime;
                break;
        }


        if(fuel > 0)
            _myBody.AddForce(transform.up * speed * Time.deltaTime);


	}
}
