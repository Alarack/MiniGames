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

	void Start () {
		
	}

	void Update () {

        switch (movetype) {
            case MoveType.Erratic:
                Vector2 randomCircle = Random.insideUnitCircle * speed;

                Vector3 offset = new Vector3(transform.position.x * randomCircle.x, transform.position.y * randomCircle.y, 0f);

                transform.position += offset * Time.deltaTime;


                break;
        }


	}
}
