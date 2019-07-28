using Photon.Pun;
using UnityEngine;


public class PlayerProperties : MonoBehaviour, IPunObservable
{
    Color col;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            Color colo = GetComponent<SpriteRenderer>().color;
            stream.SendNext(colo.r);
            stream.SendNext(colo.g);
            stream.SendNext(colo.b);


        }
        else
        {
            col.r=(float)stream.ReceiveNext();
            col.g=(float)stream.ReceiveNext();
            col.b=(float)stream.ReceiveNext();
            col.a=1f;
            GetComponent<SpriteRenderer>().color=col;
        }
    }
}
