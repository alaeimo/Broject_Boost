using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class Rocket : MonoBehaviour
{
    [SerializeField] float mainThrust = 15f;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] ParticleSystem engineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] AudioClip mainEngineAudio;
    [SerializeField] AudioClip successAudio;
    [SerializeField] AudioClip deathAudio;
    public bool gameIsPaused = false;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive,Dying,Transcending}
    [SerializeField]  State state = State.Alive;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((state == State.Alive) && !gameIsPaused)
        {
            RespondToTheThrusting();
            RespondToTheRotating();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state!= State.Alive) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }
    private void StartSuccessSequence() {
        state = State.Transcending;
        successParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        Invoke("LoadNextLevel", 1f);
    }
    private void StartDeathSequence()
    {
        state = State.Dying;
        explosionParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(deathAudio);
        Invoke("LoadFirstLevel", 1f);
    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
    private void LoadNextLevel()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == sceneCount)
            nextScene = 0;
        SceneManager.LoadScene(nextScene);
    }
    private void RespondToTheThrusting()
    {
        if (Input.GetKey(KeyCode.Space)){
            ApplyThrusting();
        }
        else
        {
           audioSource.Stop();
           engineParticles.Stop();
        }
    }

    private void ApplyThrusting()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineAudio);
            engineParticles.Play();
        }


    }
    private void RespondToTheRotating()
    {
        rigidBody.freezeRotation = true; // take manual control of the rotation
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rcsThrust * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rcsThrust * Time.deltaTime);

        }
        rigidBody.freezeRotation = false; // lets physics control of the rotation
    }
}
