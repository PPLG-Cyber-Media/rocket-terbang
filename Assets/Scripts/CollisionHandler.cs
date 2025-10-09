using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] AudioClip crashSound;
  [SerializeField] AudioClip finishSound;
  
  [SerializeField] ParticleSystem explosionParticle;
  [SerializeField] ParticleSystem finishParticle;

  private void OnCollisionEnter(Collision collision)
  {

    Debug.Log(collision.gameObject);
    Debug.Log(collision.gameObject.tag);

    switch (collision.gameObject.tag)
    {
      case "Friendly":
        Debug.Log("Everything is looking good!");
        break;

      case "Finish":
        Debug.Log("You Win!");
        finishParticle.Play();
        GetComponent<AudioSource>().PlayOneShot(finishSound);
        break;

      default:
        Debug.Log("Crash!!");
        explosionParticle.Play();
        GetComponent<Movement>().enabled = false; // matikan script movement
        GetComponent<AudioSource>().PlayOneShot(crashSound);
        break;
    }

  }
}
