using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartParticles : MonoBehaviour
{
    public ParticleSystem _particles;

    public AnimationCurve alphaCurve;

    Material _material;

    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.onPaddleDistance += handlePaddleDistance;
        _material = _particles.GetComponent<ParticleSystemRenderer>().sharedMaterial;
    }
    
    public void handlePaddleDistance(float alpha)
    {
        var color = _material.color;
        color.a = alphaCurve.Evaluate(alpha);
        _material.color = color;
    }
}
