using UnityEngine;

namespace TinyBitTurtle
{
    public partial class GameSettings : ScriptableObject
    {
        [Header("Common")]
        [Space(10)]
        public string product;
        public string version;
    }
}