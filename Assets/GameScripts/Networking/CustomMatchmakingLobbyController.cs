using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CustomMatchmakingLobbyController : MonoBehaviourPunCallbacks
{


    [SerializeField]
    private GameObject lobbyConnectButton, LoadingButton; //button used for joining a Lobby.

    [SerializeField]
    private GameObject PlayerUI; //UI Player to change color.

    [SerializeField]
    private InputField playerNameInput; //Input field so player can change their NickName
    private string roomName; //string for saving room name
    private int roomSize; //int for saving room size
    private List<RoomInfo> roomListings; //list of current rooms
    [SerializeField]
    private Transform roomsContainer; //container for holding all the room listings
    [SerializeField]
    private GameObject roomListingPrefab; //prefab for displayer each room in the lobby




    [System.Serializable]
    public class Room
    {
        public string RoomName;
        public GameObject RoomObject;
    }

    public static ObjectPooler Instance;

    public List<Room> Rooms;
    public Dictionary<string, GameObject> RoomDictionary;
    // Start is called before the first frame update
    void Start()
    {
        RoomDictionary = new Dictionary<string, GameObject>();

        foreach (Room pool in Rooms)
        {
            RoomDictionary.Add(pool.RoomName, pool.RoomObject);
        }

    }




    public override void OnConnectedToMaster() //Callback function for when the first connection is established successfully.
    {

        PhotonNetwork.AutomaticallySyncScene = true; //Makes it so whatever scene the master client has loaded is the scene all other clients will load
        LoadingButton.SetActive(false);
        lobbyConnectButton.SetActive(true); //activate button for connecting to lobby
        roomListings = new List<RoomInfo>(); //initializing roomListing

        //check for player name saved to player prefs
        if (PlayerPrefs.HasKey("NickName"))
        {
            if (PlayerPrefs.GetString("NickName") == "")
            {
                PhotonNetwork.NickName = "Player " + Random.Range(100, 1000); //random player name when not set
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString("NickName"); //get saved player name
            }
        }
        else
        {
            PhotonNetwork.NickName = "Player " + Random.Range(100, 1000); //random player name when not set
        }
        playerNameInput.text = PhotonNetwork.NickName; //update input field with player name
    }





    public void PlayerNameUpdateInputChanged(string nameInput) //input function for player name. paired to player name input field
    {
        PhotonNetwork.NickName = nameInput;
        PlayerPrefs.SetString("NickName", nameInput);
    }



    public override void OnRoomListUpdate(List<RoomInfo> roomList) //Once in lobby this function is called every time there is an update to the room list
    {
        int tempIndex;
        foreach (RoomInfo room in roomList) //loop through each room in room list
        {
            if (roomListings != null) //try to find existing room listing
            {
                tempIndex = roomListings.FindIndex(ByName(room.Name));
            }
            else
            {
                tempIndex = -1;
            }
            if (tempIndex != -1) //remove listing because it has been closed
            {
                roomListings.RemoveAt(tempIndex);
                Destroy(roomsContainer.GetChild(tempIndex).gameObject);
            }
            if (room.PlayerCount > 0) //add room listing because it is new
            {
                roomListings.Add(room);
                ListRoom(room);
            }
        }
    }
    static System.Predicate<RoomInfo> ByName(string name) //predicate function for seach through room list
    {
        return delegate (RoomInfo room)
        {
            return room.Name == name;
        };
    }
    void ListRoom(RoomInfo room) //displays new room listing for the current room
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsContainer);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.SetRoom(room.Name, room.MaxPlayers, room.PlayerCount);
        }
    }
    public void RoomNameInputChanged(string nameIn) //input function for changing room name. paired to room name input field
    {
        roomName = nameIn;
    }
    public void OnRoomSizeInputChanged(string sizeIn) //input function for changing room size. paired to room size input field
    {
        roomSize = int.Parse(sizeIn);
    }
    public void CreateRoomOnClick() //function paired to the create room button
    {
        Debug.Log("Creating room now");
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom(roomName, roomOps); //attempting to create a new room
    }
    public override void OnCreateRoomFailed(short returnCode, string message) //create room will fail if room already exists
    {
        Debug.Log("Tried to create a new room but failed, there must already be a room with the same name");
    }





    public void OnEnableRoomByName(string name)
    {
        foreach (Room room in Rooms)
        {
            room.RoomObject.SetActive(false);
        }

        RoomDictionary[name].SetActive(true);

    }

    public void OnEnablePlayerUI()
    {
        PlayerUI.SetActive(true);
    }
    public void OnDisablePlayerUI()
    {
        PlayerUI.SetActive(false);
    }
    public void OnLobbyExit()
    {
        PhotonNetwork.LeaveLobby();
    }
    public void OnLobbyEnter()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnReloadScene()
    {
        StartCoroutine("Reload");
    }
    IEnumerator Reload()
    {
        PhotonNetwork.Disconnect();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void OnExitApplication()
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }

}