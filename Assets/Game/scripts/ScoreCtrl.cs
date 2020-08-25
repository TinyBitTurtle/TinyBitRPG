using UnityEngine;
using System.Collections;

public class ScoreCtrl : MonoBehaviour
{
    //public float m_fLifespan;
    //private float m_LifespanCounter;

    //public float m_fSpeed;
    //private Vector3 vVelocity = new Vector3();

    void Awake()
    {
    }

    public void Reset()
    {
        //m_LifespanCounter = 0;

        //transform.localScale = Vector3.zero;
        //iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(40.0f, 40.0f, 0.0f), "time", 1, "easeType", "easeOutElastic"));
        //iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(0.0f, 0.0f, 0.0f), "time", 1, "delay", 1.0f, "easeType", "easeInElastic"));
    }

    // Update is called once per frame
    void Update()
    {
        // is it dead?
        //m_LifespanCounter += Time.deltaTime;
        //if (m_LifespanCounter >= m_fLifespan)
        //{
        //    Managers.UIMgr.DeregisterFloater(gameObject);
        //    PoolManager.Despawn(gameObject);
        //}

        //vVelocity = Vector3.up * m_fSpeed * Time.deltaTime;
        //transform.position += vVelocity;
    }
}
