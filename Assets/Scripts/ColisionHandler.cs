using UnityEngine;
using UnityEngine.SceneManagement;
public class ColisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay  = 0.3f;
    [SerializeField] float explosionVolume;
    [SerializeField] float successVolume;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem successParticle;
    Movement movement;
    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisabled = false;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        movement = GetComponent<Movement>();
    }
    void Update()
    {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L)) {LoadNextLevel();}
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) {return;}
        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                startSuccessSequence(loadDelay);
                break;
            case "Fuel":
                GetComponent<RocketFuel>().currentFuel = 100;
                Destroy(other.gameObject);
                break;
            default:
                StartCrashSequence(loadDelay);          
                break;
        }
    }
    void StartCrashSequence(float loadDelay)
    {
        isTransitioning = true;
        if (!audioSource.isPlaying)
        {
            audioSource.volume = explosionVolume;
            audioSource.PlayOneShot(explosion);
        }
        PlayExplosionParticle();
        movement.enabled = false;
        GetComponent<AudioSource>().Stop();
        Invoke("ReloadLevel",loadDelay);
    }
    void startSuccessSequence(float loadDelay)
    {
        isTransitioning = true;
        if (!audioSource.isPlaying)
        {
            audioSource.volume = successVolume;
            audioSource.PlayOneShot(success);
        }
        PlaySuccessParticle();
        movement.enabled = false;
        GetComponent<AudioSource>().Stop();
        Invoke("LoadNextLevel",loadDelay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void PlaySuccessParticle() {successParticle.Play();}
    void PlayExplosionParticle() {explosionParticle.Play();}
}
