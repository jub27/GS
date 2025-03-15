using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [SerializeField] private Light sunLight;
    [SerializeField] private float speedMultiply;

    void Update()
    {
        sunLight.transform.Rotate(Vector3.right, Time.deltaTime * speedMultiply);
    }
}
