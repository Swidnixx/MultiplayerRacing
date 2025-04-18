using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class RaceLauncher : MonoBehaviourPunCallbacks
{
    bool connecting;

    public void JoinRace()
    {
        if (connecting) return;
        connecting = true;

        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom(); //do��cz do pokoju
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings(); // po��cz z serverem
        }
    }

    //kiedy po�aczono z serverem
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    //kiedy nie uda�o si� do��czy� do pokoju
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 } );
    }

    public override void OnJoinedRoom()
    {
        //SceneManager.LoadScene("Race");
        PhotonNetwork.LoadLevel("SampleScene");
    }
}
