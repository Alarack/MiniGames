using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSeek : MonoBehaviour {

    public Transform target;
    //public Transform target2;
    public float force = 10f;

    public bool lerp;
    public bool delayedStart;
    public float delay;
    [Space(10)]
    public float particleRemainingLifeDelay;

    private ParticleSystem _particleSystem;
    private ParticleSystem.Particle[] _particles;
    private ParticleSystem.MainModule _particleSystemMainModule;
    private float _timer;
    //private float _particleTimer;
    //private ParticleHolder[] _holders;

    void Start() {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystemMainModule = _particleSystem.main;
    }


    void LateUpdate() {

        if (target == null)
            return;

        if (delayedStart && _timer < delay) {
            _timer += Time.deltaTime;
            return;
        }



        int maxParticles = _particleSystemMainModule.maxParticles;

        if (_particles == null || _particles.Length < maxParticles) {
            _particles = new ParticleSystem.Particle[maxParticles];
        }


        _particleSystem.GetParticles(_particles);


        float forceDeltaTime = force * Time.deltaTime;

        Vector3 targetTransformPosition;

        //Vector3 targetTransformPosition2;

        switch (_particleSystemMainModule.simulationSpace) {

            case ParticleSystemSimulationSpace.Local:
                targetTransformPosition = transform.InverseTransformPoint(target.position);
                //targetTransformPosition2 = transform.InverseTransformPoint(target2.position);

                break;

            case ParticleSystemSimulationSpace.Custom:
                targetTransformPosition = _particleSystemMainModule.customSimulationSpace.InverseTransformPoint(target.position);
                //targetTransformPosition2 = _particleSystemMainModule.customSimulationSpace.InverseTransformPoint(target2.position);

                break;

            case ParticleSystemSimulationSpace.World:
                targetTransformPosition = target.position;
                //targetTransformPosition2 = target2.position;

                break;

            default:
                throw new System.NotSupportedException(
                    string.Format("Unsupported Simpluation Space '{0}'.",
                    System.Enum.GetName(typeof(ParticleSystemSimulationSpace), _particleSystemMainModule.simulationSpace)));
        }




        for (int i = 0; i < _particles.Length; i++) {

            if(_particles[i].remainingLifetime >= particleRemainingLifeDelay) {
                continue;
            }

            if (lerp) {
                _particles[i].position = Vector2.Lerp(_particles[i].position, new Vector2(target.position.x, target.position.y), forceDeltaTime);
            }
            else {
                Vector3 directionToTarget = Vector3.Normalize(targetTransformPosition - _particles[i].position);
                Vector3 seekForce = (directionToTarget) * forceDeltaTime;
                _particles[i].velocity += seekForce;
            }
        }



        _particleSystem.SetParticles(_particles, _particles.Length);
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }

    void OnParticleCollision(GameObject other) {
        Debug.Log(other + " Part");

        other.GetComponent<ParticleCollector>().AddBar();
    }

}
