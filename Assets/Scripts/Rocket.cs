using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    [SerializeField] private Text _energy;
    [SerializeField] private int _energyTotal = 2000;
    [SerializeField] private int _energyConsumption = 300;
    [SerializeField] private int _energyBattery = 500;
    [SerializeField] private float _rotationSpeed = 320f;
    [SerializeField] private float _flyForce = 2000f; 
    [SerializeField] private AudioClip _flySound;
    [SerializeField] private AudioClip _finishSound;
    [SerializeField] private AudioClip _boomSound;
    [SerializeField] private ParticleSystem _boomParticle;
    [SerializeField] private ParticleSystem _finishParticle;
    [SerializeField] private ParticleSystem _flyParticle;

    private FunctionsUI _functionsUI;
    private bool _condBattery = true;
    private Rigidbody _rigidbody;
    private AudioSource _audiosource;

    enum State { Playing, Dead, NextLevel};
    enum Mode { Creative, Simple};
    State state;
    Mode mode;

    void Start()
    {
        _functionsUI = GetComponent<FunctionsUI>();
        _rigidbody = GetComponent<Rigidbody>();
        _audiosource = GetComponent<AudioSource>();
        state = State.Playing;
        mode = Mode.Simple;
        _energy.text = _energyTotal.ToString();
        //_functionsUI.DisableCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && Debug.isDebugBuild)
        {
            if(mode == Mode.Simple)
                mode = Mode.Creative;
            else if (mode == Mode.Creative)
                mode = Mode.Simple;
        }
    }

    void FixedUpdate()
    {
        if (State.Playing == state || ((Mode.Creative == mode && Debug.isDebugBuild) && State.NextLevel != state))
        {
            RocketLaunch();
            RocketRotate();
        }
        if (_condBattery == false)
            _condBattery = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (State.Playing != state)
            return;

        switch (collider.gameObject.tag)
        {
            case "Battery":
                Destroy(collider.gameObject);
                BatteryAdd();
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (State.Playing != state)
            return;

        switch (collision.gameObject.tag)
        {
            case "FinalPlatform":
                Finish();
                break;
            case "Friendly":
                break;
            default:
                Lose();
                break;
        }
    }

    public void Finish()
    {
        state = State.NextLevel;
        StopPlayerParticleAudio();
        _audiosource.PlayOneShot(_finishSound);
        _finishParticle.Play();
        _rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        _rigidbody.freezeRotation = true;
        _functionsUI.EnableWinPanel();
    }

    public void Lose()
    {
        if(mode == Mode.Simple)
        {
            state = State.Dead;
            StopPlayerParticleAudio();
            _boomParticle.Play();
            _audiosource.PlayOneShot(_boomSound);
            _rigidbody.freezeRotation = false;
            _functionsUI.EnableLosePanel();
        }
    }
    
    public void RocketLaunch()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _energyTotal -= Mathf.RoundToInt(_energyConsumption * Time.deltaTime);
            _energy.text = _energyTotal.ToString();
            _rigidbody.AddRelativeForce(Vector3.up * _flyForce * Time.deltaTime);
            if (!_flyParticle.isPlaying && !_audiosource.isPlaying)
                PlayPlayerParticleAudio();
            if (_energyTotal < 1)
            {
                _energy.text = "0";
                Lose();
            }
        }
        else
            StopPlayerParticleAudio();
    }

    public void StopPlayerParticleAudio()
    {
        _audiosource.Stop();
        _flyParticle.Stop();
    }

    public void PlayPlayerParticleAudio()
    {
        _audiosource.PlayOneShot(_flySound);
        _flyParticle.Play();
    }

    public void RocketRotate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);   
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.back * _rotationSpeed * Time.deltaTime);
    }

    public void BatteryAdd()
    {
        if (_condBattery == true)
        {
            _energyTotal += _energyBattery;
            _energy.text = _energyTotal.ToString();
            _condBattery = false;
        }
    }
}
