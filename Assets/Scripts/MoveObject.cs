using UnityEngine;

[DisallowMultipleComponent]
public class MoveObject : MonoBehaviour
{
    [SerializeField] private Vector3 _movePosition;
    [SerializeField] private float _moveSpeed = 0.8f;
    private Vector3 _startPosition; 

    void Start()
    {
        _startPosition = transform.position; 
    }
    
    void Update()
    {
        transform.position = _startPosition + (_movePosition * Mathf.PingPong(Time.time * _moveSpeed, 1));
    }
}
