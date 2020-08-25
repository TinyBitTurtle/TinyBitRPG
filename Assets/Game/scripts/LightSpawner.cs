using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace TinyBitTurtle
{
    public class LightSpawner : Spawner
    {
        public override void spawnAction(SpawnCallback callback, Vector3 pos)
        {
            base.spawnAction(callback, pos);

            Light2D newLight = newGameObject.GetComponent<Light2D>();
            if (newLight)
            {
                newLight.color = new Color(Mathf.Clamp(newLight.color.r + Random.Range(-variance, variance), 0, 1), Mathf.Clamp(newLight.color.g + Random.Range(-variance, variance), 0, 1), Mathf.Clamp(newLight.color.b + Random.Range(-variance, variance), 0, 1));
            }
        }
    }
}