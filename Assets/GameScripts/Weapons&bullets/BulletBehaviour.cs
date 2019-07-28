using Photon.Pun;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.gameObject.CompareTag("Platform"))
        //{
          //  gameObject.tag = "BulletInactive";
            //gameObject.layer = 12;
        //}

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death"))
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
