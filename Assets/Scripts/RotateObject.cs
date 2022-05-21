using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private int _rotationSpeed = 100;
    private int _rotationAngles = 0;

    void FixedUpdate()
    {
        _rotationAngles += _rotationSpeed;
        transform.eulerAngles = new Vector3(0, _rotationAngles * Time.deltaTime,0);
    }
}
