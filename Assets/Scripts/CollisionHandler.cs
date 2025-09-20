using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
  private void OnCollisionEnter(Collision collision)
  {

    Debug.Log(collision.gameObject);
    Debug.Log(collision.gameObject.tag);

        switch (collision.gameObject.tag)
        {
            case "Finish":
                Debug.Log("You Win!");
                break;

            case "Obstacle":
                Debug.Log("Crash!!");
                break;
                
            default:
                break;
        }

    }
}
