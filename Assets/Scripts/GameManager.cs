using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("LoginPanel")]
    public GameObject loginPanel;
    public Text connettionStatusText;
    public InputField userNameInput;

    [Header("GameOptionPanel")]
    public GameObject gameOptionPanel;

    [Header("CreateRoomPanel")]
    public GameObject createRoomPanel;
    public InputField roomName;
    public InputField maxPlayerCount;

    [Header("InsideRoomPanel")]
    public GameObject insideRoomPanel;
    public Text roomInfoText;
    public GameObject playerListParent;
    public GameObject playerListPrefab;
    public GameObject startGameButton;

    [Header("RoomListPanel")]
    public GameObject roomListPanel;

    [Header("JoinRandomRoomPanel")]
    public GameObject joinRandomRoomPanel;
    public byte maxPlayerPerRoom = 2;

    Dictionary<int, GameObject> playerListCached = new Dictionary<int, GameObject>();
    

    public void ShowOnlyOnePanel(string panelName)
    {
        loginPanel.SetActive(panelName.Equals(loginPanel.name));
        gameOptionPanel.SetActive(panelName.Equals(gameOptionPanel.name));
        createRoomPanel.SetActive(panelName.Equals(createRoomPanel.name));
        insideRoomPanel.SetActive(panelName.Equals(insideRoomPanel.name));
        roomListPanel.SetActive(panelName.Equals(roomListPanel.name));
        joinRandomRoomPanel.SetActive(panelName.Equals(joinRandomRoomPanel.name));

    }

    // Start is called before the first frame update
    void Start()
    {
        ShowOnlyOnePanel(loginPanel.name);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Update()
    {
        connettionStatusText.text = PhotonNetwork.NetworkClientState.ToString();
    }


    #region photon callbacks

    public override void OnConnected()
    {
        Debug.Log("인터넷에 연결됨");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("포톤 서버에 연결됨");
        ShowOnlyOnePanel(gameOptionPanel.name);
    }

    public override void OnJoinedRoom()
    {   
        foreach(Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            Debug.Log("방에 있는 사람 : " + player.NickName);
        }
        //로컬플레이어가 방에 들어가면 방을 셋팅 한다. 

        ShowOnlyOnePanel(insideRoomPanel.name);
        roomInfoText.text = PhotonNetwork.CurrentRoom.Name + " : "  + PhotonNetwork.CurrentRoom.PlayerCount +  " / " + PhotonNetwork.CurrentRoom.MaxPlayers;

        //방장이면 게임시작 가능
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            startGameButton.SetActive(true);
        }
        else
            startGameButton.SetActive(false);

        //모든 플레이어를 순회하며 플레이어 리스트 오브젝트를 생성한다.
        foreach (var player in PhotonNetwork.CurrentRoom.Players.Values)
        {

            GameObject playerList = Instantiate(playerListPrefab);
            playerList.transform.Find("PlayerNameText").GetComponent<Text>().text = player.NickName;
            playerList.transform.SetParent(playerListParent.transform, false);

            //로컬 플레이어면
            if (player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                playerList.transform.Find("PlayerIndicator").gameObject.SetActive(true);
            }
            else
                playerList.transform.Find("PlayerIndicator").gameObject.SetActive(false);

            //나중에 플레이어가 나가면 파괴하기 위해
            playerListCached.Add(player.ActorNumber, playerList);

        }

        

       


    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        roomInfoText.text = PhotonNetwork.CurrentRoom.Name + " : " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
        //다른 플레이어가 나가면 플레이어리스트 항목을 파괴한다.
        Destroy(playerListCached[otherPlayer.ActorNumber].gameObject);
        playerListCached.Remove(otherPlayer.ActorNumber);

        //로컬 플레이어가 방장이 되었으면
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            startGameButton.SetActive(true);
        }
        else
            startGameButton.SetActive(false);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //다른 플레이어가 들어오면 방을 다시 셋팅

        roomInfoText.text = PhotonNetwork.CurrentRoom.Name + " : " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;

        GameObject playerList = Instantiate(playerListPrefab);
        playerList.transform.Find("PlayerNameText").GetComponent<Text>().text = newPlayer.NickName;
        playerList.transform.SetParent(playerListParent.transform, false);

       

        if (newPlayer.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            playerList.transform.Find("PlayerIndicator").gameObject.SetActive(true);
        }
        else
            playerList.transform.Find("PlayerIndicator").gameObject.SetActive(false);

        playerListCached.Add(newPlayer.ActorNumber, playerList);
    }

    public override void OnLeftRoom()
    {
        //로컬플레이어가 방을 나가면 모든 플레이어 리스트를 파괴한다.
        foreach (GameObject playerListObj in playerListCached.Values)
        {
            Destroy(playerListObj);
        }
        playerListCached.Clear();
    }



    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        string roomName = "Room" + Random.Range(0, 100);
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayerPerRoom });

    }




    #endregion

    #region UI methods

    public void Login()
    {
        PhotonNetwork.NickName = userNameInput.text;
        PhotonNetwork.ConnectUsingSettings();

    }

    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = byte.Parse(maxPlayerCount.text);
        PhotonNetwork.CreateRoom(roomName.text, options);
    }


    public void OnCreateRoomButtonClicked()
    {
        ShowOnlyOnePanel(createRoomPanel.name);
    }

    public void OnJoinRandomRoomButtonClicked()
    {
        ShowOnlyOnePanel(joinRandomRoomPanel.name);
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnBackButtonClicked()
    {
        ShowOnlyOnePanel(gameOptionPanel.name);
        if(PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        
    }

    public void OnStartGameButtonClicked()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }

    #endregion


 




}
