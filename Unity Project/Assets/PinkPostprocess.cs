using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkPostprocess : MonoBehaviour
{
    public Shader shader;
    public AnimationCurve pinkCurve;
    public Color color;

    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = new Material(shader);
        mat.SetColor("_PinkColor", color);

        GameController.instance.onPaddleDistance += handlePaddleDistance;
    }

    // Update is called once per frame
    void handlePaddleDistance(float alpha)
    {
        mat.SetColor("_PinkColor", color);
        mat.SetFloat("_Pinkness", pinkCurve.Evaluate(alpha));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
    }
}
