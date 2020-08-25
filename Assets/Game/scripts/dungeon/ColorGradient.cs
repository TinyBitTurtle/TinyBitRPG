using UnityEngine;

[CreateAssetMenu]
public class ColorGradient : GenericGrid<Color>
{
    [Header("Gradient")]
    [Space(10)]
    public float gradientSlope;
    public float influenceRadius = 1;
    public int numColors;

    // called after all the registrations
    public override void Init()
    {
        // create grid
        base.Init();

        // create color gradients in the grid
    }

    public override Color Evaluate(Vector2 pos)
    {
        return defaultNode;
    }
}
