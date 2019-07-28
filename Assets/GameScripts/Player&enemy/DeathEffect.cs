using Photon.Pun;
using System.Collections;
using UnityEngine;

public class DeathEffect : MonoBehaviour//, IPunObservable
{
    public GameObject Effect;
    private PhotonView PV;

    public GameObject Child;

    private UIBehaviour UI;

    public int TimeToRespawn = 1;
    Color col;




    private void Start()
    {
        PV = GetComponent<PhotonView>();
        UI = GetComponent<UIBehaviour>();

        int SpawnPicker = Random.Range(0, GameSetup.GS.SpawnPoints.Length);

        if (PV.IsMine)
        {
            OnRespawn(SpawnPicker);

        }
    }



    public void OnDie()
    {
        if (PV.IsMine)
            StartCoroutine("SpawnChild");
    }



    public void OnRespawn(int spawnpoint)
    {
        
        GameObject Temp = PhotonNetwork.Instantiate(Child.name, GameSetup.GS.SpawnPoints[spawnpoint].position, Quaternion.identity);
        if (PlayerPrefs.HasKey("RGBColor"))
        {
            string[] RGB = new string[3];
            RGB = PlayerPrefs.GetString("RGBColor").Split(',');

            col.r=float.Parse(RGB[0]);
            col.g=float.Parse(RGB[1]);
            col.b=float.Parse(RGB[2]);
            col.a=1f;

            Temp.GetComponent<SpriteRenderer>().color=col;
        }
        Temp.GetComponent<Health>().Parent = this;
        Temp.GetComponent<Health>().UI = UI;
        Temp.GetComponent<Health>().Arm.UI = UI;
    }


    IEnumerator SpawnChild()
    {
        if (PV.IsMine)
        {
            int SpawnPicker = Random.Range(0, GameSetup.GS.SpawnPoints.Length);
            yield return new WaitForSeconds(TimeToRespawn);
            OnRespawn(SpawnPicker);
        }
    }



}
