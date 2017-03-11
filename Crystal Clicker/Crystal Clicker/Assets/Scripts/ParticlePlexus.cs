using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticlePlexus : MonoBehaviour {

    public float maxDistance = 1f;
    public int maxConnections = 5;
    public int maxLineRenderers = 100;
    public LineRenderer lineRendererTemplate;
    List<LineRenderer> lineRenderers = new List<LineRenderer>();



    private ParticleSystem _particleSystem;
    private ParticleSystem.Particle[] _particles;
    private ParticleSystem.MainModule _mainModule;
    private Transform _transform;

    void Start() {
        _particleSystem = GetComponent<ParticleSystem>();
        _mainModule = _particleSystem.main;
    }

    void LateUpdate() {
        int maxParticles = _mainModule.maxParticles;

        if (_particles == null || _particles.Length < maxParticles) {
            _particles = new ParticleSystem.Particle[maxParticles];
        }

        int lineRendererIndex = 0;
        int lineRendererCount = lineRenderers.Count;

        if (lineRendererCount > maxLineRenderers) {
            for (int i = maxLineRenderers; i < lineRendererCount; i++) {
                Destroy(lineRenderers[i].gameObject);
            }

            int removedCount = lineRendererCount - maxLineRenderers;
            lineRenderers.RemoveRange(maxLineRenderers, removedCount);
            lineRendererCount -= removedCount;
        }

        if (maxConnections > 0 && maxLineRenderers > 0) {

            _particleSystem.GetParticles(_particles);
            int particleCount = _particleSystem.particleCount;

            float maxDistanceSquared = maxDistance * maxDistance;

            ParticleSystemSimulationSpace simulationSpace = _mainModule.simulationSpace;

            switch (simulationSpace) {
                case ParticleSystemSimulationSpace.Local:
                    _transform = transform;
                    break;

                case ParticleSystemSimulationSpace.Custom:
                    _transform = _mainModule.customSimulationSpace;
                    break;

                case ParticleSystemSimulationSpace.World:
                    _transform = transform;
                    break;

                default:

                    throw new System.NotSupportedException(
                        string.Format("Unsupported Simpluation Space '{0}'.",
                        System.Enum.GetName(typeof(ParticleSystemSimulationSpace), _mainModule.simulationSpace)));
            }

            for (int i = 0; i < particleCount; i++) {

                if (lineRendererIndex == maxLineRenderers) {
                    break;
                }

                Vector3 p1_position = _particles[i].position;

                int connections = 0;


                for (int j = i + 1; j < particleCount; j++) {

                    Vector3 p2_position = _particles[j].position;
                    float distanceSquared = Vector3.SqrMagnitude(p1_position - p2_position);

                    if (distanceSquared <= maxDistanceSquared) {

                        LineRenderer lr;

                        if (lineRendererIndex == lineRendererCount) {
                            lr = Instantiate(lineRendererTemplate, _transform, false);
                            lineRenderers.Add(lr);
                            lineRendererCount++;
                        }

                        lr = lineRenderers[lineRendererIndex];

                        lr.enabled = true;
                        lr.useWorldSpace = simulationSpace == ParticleSystemSimulationSpace.World ? true : false;

                        lr.SetPosition(0, p1_position);
                        lr.SetPosition(1, p2_position);

                        lineRendererIndex++;
                        connections++;

                        if (connections == maxConnections || lineRendererIndex == maxLineRenderers) {
                            break;
                        }
                    }
                }

            }

        }

        for (int i = lineRendererIndex; i < lineRenderers.Count; i++) {
            lineRenderers[i].enabled = false;
        }



    }
}
