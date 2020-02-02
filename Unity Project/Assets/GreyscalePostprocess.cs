using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GreyscalePostprocess : MonoBehaviour
{
    public Shader shader;

    public AnimationCurve greynessCurve;

    private Material mat;

    private void Awake()
    {
        mat = new Material(shader);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.onPaddleDistance += handlePaddleDistance;
    }

    // Update is called once per frame
    void handlePaddleDistance(float distance)
    {
        mat.SetFloat("_Greyness", greynessCurve.Evaluate(distance));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
    }
}
