using UnityEngine;

public class PlayBackgoundParticle : MonoBehaviour
{
    [SerializeField] private int _timePlayParticle = 40;
    private ParticleSystem _particlesystem;
    
    void Start()
    {
        _particlesystem = GetComponent<ParticleSystem>();
        _particlesystem.Simulate(_timePlayParticle);
        _particlesystem.Play();
    }
}
