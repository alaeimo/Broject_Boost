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

    Rigidbody rigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                print("Hit Finish");
                successParticles.Play();
                Invoke("LoadNextLevel", 1f);
                break;
            default:
                print("Dead");
                explosionParticles.Play();
                Invoke("LoadFirstLevel", 1f);
                break;
        }
    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)){
            rigidBody.AddRelativeForce(Vector3.up*mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            engine1Particles.Play();
            engine2Particles.Play();
        }
        else
        {
            audioSource.Stop();
            engine1Particles.Stop();
            engine2Particles.Stop();

        }
    }

    private void Rotate()
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
