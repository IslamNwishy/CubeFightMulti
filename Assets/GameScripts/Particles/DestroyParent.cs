using Photon.Pun;
using System.Collections;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    public float LifeTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("End");
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(LifeTime);
        PhotonNetwork.Destroy(gameObject);

    }
}
