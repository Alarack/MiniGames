using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemFragment : MonoBehaviour {

    public float speed;
    public float rotationSpeed;
    public int points;
    public float seekDelay;

    //public Sprite[] sprites;
    public SpriteManager[] sprites;
    public Transform target;

    public Constants.GemColor fragmentColor;

    private SpriteRenderer _mySpriteRenderer;
    private Rigidbody2D _myBody;
    private float _timer;


    [System.Serializable]
    public class SpriteManager {
        public Constants.GemColor fragmentColor;
        public Sprite sprite;
    }


	void Awake () {
        _myBody = GetComponent<Rigidbody2D>();
        _mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

    void Start() {
        for(int i = 0; i < sprites.Length; i++) {
            if (sprites[i].fragmentColor == fragmentColor) {
                _mySpriteRenderer.sprite = sprites[i].sprite;
                break;
            }
        }
        _myBody.angularVelocity = Random.Range(-rotationSpeed, rotationSpeed);
        _myBody.velocity = Random.insideUnitCircle * speed;
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }

	void FixedUpdate () {
        if (target != null) {
            _timer += Time.deltaTime;
            if (_timer > seekDelay /*&& Vector2.Distance(transform.position, target.transform.position) > 0.01f*/) {
                if (Vector2.Distance(transform.position, target.position) > 1f) {
                    transform.position = Vector2.Lerp(transform.position, target.position, speed * Time.deltaTime);
                }
                else {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, ((speed+1) * Time.deltaTime));
                }
            }

        }
        else {
            Debug.Log("Target Null");
        }

	}

    public void Die() {
        GameObject particle = Constants.GetParticleByColor(fragmentColor);

        GameObject explosion = Instantiate(particle , transform.position, Quaternion.identity) as GameObject;

        Destroy(explosion, 0.3f);
        //explosion.AddComponent<DestroyAfterDuration>();
        //DestroyAfterDuration cleanUp = explosion.GetComponent<DestroyAfterDuration>();
        //cleanUp.duration = 0.3f;

        Destroy(gameObject);

    }

}