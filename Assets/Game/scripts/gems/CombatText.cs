using EZObjectPools;
using UnityEngine;

public class CombatText : MonoBehaviour
{
    public AnimationCurve fadeCurve;
    public AnimationCurve sizeCurve;
    public float size = 1f;
    public float gravity = 0.1f;

    private float parametric;
    private float lifeSpan;
    private float age;
    private bool alive;

    public void Init(string message, float duration, float speed, float angle)
    {
       GetComponent<UILabel>().text = message;
       parametric = 1;

       age = lifeSpan = duration;

        Vector3 speedVector = Vector3.up * speed;
        Vector3 ejectionVector = Quaternion.AngleAxis(angle, Vector3.forward) * speedVector;

        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = gravity;
        rigidbody2D.AddForce(ejectionVector, ForceMode2D.Impulse);

        alive = true;
    }

    private void Update()
    {
        if (alive == false)
        {
            gameObject.SetActive(false);
            return;
        }

        age -= Time.deltaTime;
        parametric = age / lifeSpan;
        // alpha
        GetComponent<UILabel>().alpha = fadeCurve.Evaluate(parametric);
        // size
        float newSize = sizeCurve.Evaluate(parametric) * size;
        transform.localScale = new Vector3(newSize, newSize, newSize);

        if (age < 0)
        {
            GetComponent<PooledObject>().Disable();
            alive = false;
        }
    }
}
