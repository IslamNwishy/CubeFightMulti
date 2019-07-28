using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class TopPanelStatus : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text Status;

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.NetworkClientState.ToString() == "ConnectedToMaster")
            Status.text = "Connected to " + PhotonNetwork.CloudRegion.ToUpper() + " Server";
        else if (PhotonNetwork.NetworkClientState.ToString() == "Disconnected")
            Status.text = "Reconnecting...";
        else
            Status.text = PhotonNetwork.NetworkClientState.ToString();

    }




}
