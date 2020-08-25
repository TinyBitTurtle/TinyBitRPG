using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform target = null;

    public float speed = 0.125f;
    public Vector3 offset;

    public void Attach(GameObject obj)
    {
        if(obj != null)
            target = obj.transform;
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            Attach(obj);
        }
        else
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed);
            smoothedPosition.z = transform.position.z;

            transform.position = smoothedPosition;
        }
    }

}