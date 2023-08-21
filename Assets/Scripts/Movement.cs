
using UnityEngine;
public class Movement : MonoBehaviour
{
    [SerializeField] float rocketThrust = 100f;
    [SerializeField] float rotationValue = 100f;
    [SerializeField] float audioUpFadeSpeed = 0.01f;
    [SerializeField] float audioDownFadeSpeed = 0.01f;
    [SerializeField] ParticleSystem mainBoosterParticle;
    [SerializeField] ParticleSystem leftBoosterParticle;
    [SerializeField] ParticleSystem rightBoosterParticle;
    Rigidbody rb;
    AudioSource audioSourse;
    RocketFuel rf;
    void Start()
    {
        StartingActions();
    }
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }  
    private void StartingActions()
    {
        Caches();
        audioSourse.volume = 0;
    }
    private void Caches()
    {
        rb = GetComponent<Rigidbody>();
        audioSourse = GetComponent<AudioSource>();
        rf = GetComponent<RocketFuel>();
    }
    void ProcessThrust()
    {
        if (IsPressingSpace())
        {
            StartBosting();
        }
        else
        {
            StopBoosting();
        }
    }
    void ProcessRotation()
    {
        if (IsPressingA())
        {
            RotateLeft();
        }
        else if (IsPressingD())
        {
            RotateRight();
        }
    }
    void StartBosting()
    {
        PlayMainParticle();
        rb.AddRelativeForce(Vector3.up * rocketThrust * Time.deltaTime);
        if (audioSourse.volume < 0.5)
        {
            audioSourse.volume += audioUpFadeSpeed * Time.deltaTime;
        }
    }
    void StopBoosting()
    {
        audioSourse.volume -= audioDownFadeSpeed * Time.deltaTime;
    }
    void RotateRight()
    {
        ApplyRotation(-rotationValue);
        PlayRightBoosterParticle();
    }
    void RotateLeft()
    {
        ApplyRotation(rotationValue);
        PlayLeftBoosterParticle();
    }
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
    bool IsPressingSpace() {return Input.GetKey(KeyCode.Space) && IsHavingFuel();}
    bool IsPressingA() {return Input.GetKey(KeyCode.A) && IsHavingFuel();}
    bool IsPressingD() {return Input.GetKey(KeyCode.D) && IsHavingFuel();}
    bool IsHavingFuel() {return rf.currentFuel > 0;}
    void PlayLeftBoosterParticle() {leftBoosterParticle.Play();}
    void PlayRightBoosterParticle() {rightBoosterParticle.Play();}
    void PlayMainParticle() {mainBoosterParticle.Play();}
}