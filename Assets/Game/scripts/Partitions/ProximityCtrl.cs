using System.Collections.Generic;
using UnityEngine;

namespace TinyBitTurtle
{
    [CreateAssetMenu]
    public class ProximityCtrl : SingletonScriptable<ProximityCtrl>
    {
        [SerializeField]
        private float radius = 10;

        private List<ProximityObject> listAll = new List<ProximityObject>();
        private List<ProximityObject> listActives = new List<ProximityObject>();
        private float radiusSquared;

        public void Init()
        {
            ProximityCtrl proximityCtrlSettings = Resources.Load<ProximityCtrl>("ProximityCtrlSettings") as ProximityCtrl;
            radiusSquared = proximityCtrlSettings.radius * proximityCtrlSettings.radius;
        }

        public void Register(ProximityObject obj)
        {
            listAll.Add(obj);
        }

        public void UpdateProximity(Vector3 newPos)
        {
            foreach (ProximityObject proximityObject in listAll)
            {
                bool activate = CheckDistance(newPos, proximityObject) && proximityObject.state != State.used;

                proximityObject.gameObject.SetActive(activate);
            }
        }

        private bool CheckDistance(Vector3 newPos, ProximityObject proximityObject)
        {
            return ((proximityObject.pos - newPos).sqrMagnitude < radiusSquared);
        }
    }
}