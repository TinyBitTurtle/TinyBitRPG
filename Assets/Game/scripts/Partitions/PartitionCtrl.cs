using UnityEngine;
using UnityEngine;
using UnityEngine.Events;

public class PartitionCtrl : SingletonMonoBehaviour<PartitionCtrl>
{
    public Partition partition;

    void Awake()
    {
    }

    private void UpdatePartition()
    {
        if (partition != null)
            partition.OnUpdate();
    }

    private void Insert(float value)
    {
        if (partition != null)
            partition.Oninsert(value);
    }
}
