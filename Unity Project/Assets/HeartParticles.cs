using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HeartParticles : MonoBehaviour
{
    public ParticleSystem _particles;

    Material _material;

    // Start is called before the first frame update
    void Start()
    {
        _material = _particles.GetComponent<ParticleSystemRenderer>().sharedMaterial;
    }
    
    public void SetAlpha(float alpha)
    {
        var color = _material.color;
        color.a = alpha;
        _material.color = color;
    }
}
