using UnityEngine;

namespace TinyBitTurtle
{
    public class Potion : Pickable
    {
        private void Start()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            spriteRenderer.material.color = Color.green;

        }
        public new void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            // add health to player
        }
    }
}