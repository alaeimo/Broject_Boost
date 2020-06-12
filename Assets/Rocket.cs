using UnityEngine;
using UnityEngine.SceneManagement;
public class Rocket : MonoBehaviour
{
    [SerializeField] float mainThrust = 20f;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] ParticleSystem engine1Particles;
    [SerializeField] ParticleSystem engine2Particles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] AudioClip mainEngineAudio;
    [SerializeField] AudioClip successAudio;
    [SerializeField] AudioClip deathAudio;


    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive,Dying,Transcending}
    State state = State.Alive;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
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
        SceneManager.LoadScene(1);
    }
    private void RespondToTheThrusting()
    {
        if (Input.GetKey(KeyCode.Space)){
            ApplyThrusting();
        }
        else
        {
            audioSource.Stop();
            engine1Particles.Stop();
            engine2Particles.Stop();

        }
    }

    private void ApplyThrusting()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineAudio);

        }
        engine1Particles.Play();
        engine2Particles.Play();
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
