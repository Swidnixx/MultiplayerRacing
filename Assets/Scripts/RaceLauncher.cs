using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class RaceLauncher : MonoBehaviourPunCallbacks
{
    public void JoinRace()
    {
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom(); //do³¹cz do pokoju
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings(); // po³¹cz z serverem
        }
    }

    //kiedy po³aczono z serverem
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    //kiedy nie uda³o siê do³¹czyæ do pokoju
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 } );
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("SampleScene");
        //PhotonNetwork.LoadLevel("SampleScene");
    }
}
