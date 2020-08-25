using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_Instance;

    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                // find unity object first
                m_Instance = (T)FindObjectOfType(typeof(T));

                // create a new instance
                if (m_Instance == null)
                {
                    m_Instance = (new GameObject("singletonMonoBehaviour_" + typeof(T))).AddComponent<T>();
                }
            }

            return m_Instance;
        }
    }

    // let the garbage collector do the work
    void OnDestroy()
    {
        m_Instance = null;
    }
}

public class SingletonScriptable<T> : ScriptableObject where T : ScriptableObject
{
    private static T m_Instance;

    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                // find unity object first (assume unique)
                m_Instance = (T)FindObjectOfType(typeof(T));

                // create a new instance
                if (m_Instance == null)
                {
                    m_Instance = ScriptableObject.CreateInstance<T>();
                }
            }

            return m_Instance;
        }
    }
}

public class Singleton<T> where T : new()
{
    private static T m_Instance;

    public static T Instance
    {
        get
        {
            // create a new instance
            if (m_Instance == null)
            {
                m_Instance = new T();
            }

            return m_Instance;
        }
    }

    // let the garbage collector do the work
    void OnDestroy()
    {
        m_Instance = default(T);
    }
}