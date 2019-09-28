using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public Text connectionStatusText;
    [Header("Player Characters")]
    public GameObject[] playerPrefabs;
    private string currentPlayerPrefab;

    [Header("Login Panel")]
    public GameObject loginPanel;
    public InputField nameField;

    [Header("GameOptionPanel")]
    public GameObject gameOptionPanel;
    public Button PlayButton;
    public Button CharacterListButton;

    [Header("CharacterListPanel")]
    public GameObject characterListPanel;
    public Button characterSelectButton;

    [Header("LoadingPanel")]
    public GameObject loadingPanel;
    public Text playerCount;

    [Range(0, 1)]
    private int team;



    // Start is called before the first frame update
    void Start()
    {
        ShowOnlyOnePanel(loginPanel.name);
        PhotonNetwork.AutomaticallySyncScene = true;
        currentPlayerPrefab = "Dynamik";
    }

    // Update is called once per frame
    void Update()
    {
        connectionStatusText.text = PhotonNetwork.NetworkClientState.ToString();
    }

    #region Photon Callbacks

    public override void OnConnectedToMaster()
    {
        ShowOnlyOnePanel(gameOptionPanel.name);
    }


    public override void OnJoinedRoom()
    {
        playerCount.text = PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable() { { "team", team++ } });
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable() { { "character", currentPlayerPrefab } });
        ShowOnlyOnePanel(loadingPanel.name);

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        playerCount.text = PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;

        if(team > 1)
        {
            team = 0;
        }

        newPlayer.SetCustomProperties(new Hashtable() { { "team", team++ } });
        newPlayer.SetCustomProperties(new Hashtable() { { "character", currentPlayerPrefab } });

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }

    

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);

        //룸이 없으면 만든다.
        string roomName = "Room " + Random.Range(1000, 10000);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }


    #endregion

    #region UI Methods

    public void OnLoginBtnClicked()
    {
        PhotonNetwork.NickName = nameField.text;
        if (PhotonNetwork.OfflineMode)
        {
            PhotonNetwork.Disconnect();
        }
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.ConnectUsingSettings();


    }

    public void OnPlayBtnClicked()
    {
        PhotonNetwork.JoinRandomRoom();

    }

    public void OnCharacterListBtnClicked()
    {


    }

    public void OnMapSelectBtnClicked()
    {

    }

    public void ShowOnlyOnePanel(string _panelName)
    {
        loginPanel.SetActive(_panelName.Equals(loginPanel.name));
        gameOptionPanel.SetActive(_panelName.Equals(gameOptionPanel.name));
        characterListPanel.SetActive(_panelName.Equals(characterListPanel.name));
        loadingPanel.SetActive(_panelName.Equals(loadingPanel.name));
    }

    #endregion

}
