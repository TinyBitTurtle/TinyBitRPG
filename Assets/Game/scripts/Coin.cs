using UnityEngine;

namespace TinyBitTurtle
{
    public class Coin : Pickable
    {
        public new void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            // add money to player
        }
    }
}