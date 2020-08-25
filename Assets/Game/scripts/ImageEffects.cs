using UnityEngine;

public class ImageEffects : SingletonMonoBehaviour<ImageEffects>
{
    [Range(0.0f, 1.0f)]
    public float intensity;
    public Material BWMaterial;
    public float duration;

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (intensity == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }

        BWMaterial.SetFloat("_bwBlend", intensity);
        Graphics.Blit(source, destination, BWMaterial);
    }
}
