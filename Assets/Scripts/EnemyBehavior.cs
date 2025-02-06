using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; 
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public Transform cubicControlPoint1;
    public Transform cubicControlPoint2;
    public Transform quadraticControlPoint;

    public float speed = 2f;
    public bool useCubicLerp;
    private float t = 0f;
    public int damage = 1;

    private void Update()
    {
        t += Time.deltaTime * speed;
        t = Mathf.Clamp01(t); // Ensure t stays between 0 and 1

        if (useCubicLerp)
        {
            transform.position = CubicBezier(startPoint.position, cubicControlPoint1.position, cubicControlPoint2.position, endPoint.position, t);
        }
        else
        {
            transform.position = QuadraticBezier(startPoint.position, quadraticControlPoint.position, endPoint.position, t);
        }

        if (t >= 1f)
        {
            GameManager.Instance.TakeDamage(1);
            Destroy(gameObject);
        }
    }

    private Vector3 QuadraticBezier(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        return Mathf.Pow(1 - t, 2) * start + 2 * (1 - t) * t * control + t * t * end;
    }

    private Vector3 CubicBezier(Vector3 start, Vector3 control1, Vector3 control2, Vector3 end, float t)
    {
        return Mathf.Pow(1 - t, 3) * start + 3 * Mathf.Pow(1 - t, 2) * t * control1 + 
               3 * (1 - t) * Mathf.Pow(t, 2) * control2 + Mathf.Pow(t, 3) * end;
    }
}
