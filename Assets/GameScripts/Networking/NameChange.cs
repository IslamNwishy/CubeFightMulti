using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class NameChange : MonoBehaviour
{
    private string nameInput;
    public InputField PlayerName;

    private void OnEnable()
    {
        if (PlayerPrefs.GetString("NickName") == "")
        {
            nameInput = "Player " + Random.Range(100, 1000);
            PhotonNetwork.NickName = nameInput;
            PlayerPrefs.SetString("NickName", nameInput);
        }
        else
            PlayerName.text = PlayerPrefs.GetString("NickName");
    }
    public void ApplyChanges()
    {
        PhotonNetwork.NickName = nameInput;
        PlayerPrefs.SetString("NickName", nameInput);
    }

    public void TakeInput(string input)
    {
        nameInput = input;
    }
}
