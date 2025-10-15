using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] AudioClip crashSound;
  [SerializeField] AudioClip finishSound;
  
  [SerializeField] ParticleSystem explosionParticle;
  [SerializeField] ParticleSystem finishParticle;

  float timeout = 2.0f;

  bool isControllable = true;

  bool isCollisionActive = true;

  private void OnCollisionEnter(Collision collision)
  {

    Debug.Log(collision.gameObject.tag);

    if(isCollisionActive == false || isControllable == false)
    {
      return;
    }

    switch (collision.gameObject.tag)
    {
      case "Friendly":
        Debug.Log("Everything is looking good!");
        break;

      case "Finish":
        // disable kontrol roket
        isControllable = false;
        GetComponent<Movement>().enabled = false;

        Debug.Log("You Win!");
        finishParticle.Play();
        GetComponent<AudioSource>().PlayOneShot(finishSound);
        
        Invoke("NextLevel", timeout);
        break;

      default:
        isCollisionActive = false;
        GetComponent<Movement>().enabled = false; // matikan script movement

        Debug.Log("Crash!!");
        explosionParticle.Play();
        GetComponent<AudioSource>().PlayOneShot(crashSound);

        Invoke("RestartLevel", timeout);
        break;
    }

  }

  private void NextLevel()
  {
    // load next level
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextLevel = currentSceneIndex + 1;

    // kalau udah di level terakhir, balik ke level pertama
    if (nextLevel == SceneManager.sceneCountInBuildSettings)
    {
      SceneManager.LoadScene(0);
    }
    
    // ke level selanjutnya
    SceneManager.LoadScene(nextLevel);
  }

  private void RestartLevel()
  {
    // nge restart level jika crash
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
