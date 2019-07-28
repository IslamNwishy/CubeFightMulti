using Photon.Pun;
using UnityEngine;

public class ArmController : MonoBehaviour
{

    public float Offset;
    public float FireRate = 0;

    private float TimetoFire = 0f;

    public int AmmoCount = 10;
    private bool isReloading = false;
    public float reloadTime = 1f;

    private float currentReloadTime;
    private int currentAmmo;
    public Transform firePoint;

    private ObjectPooler objectPooler;
    private PhotonView PV;

    public GameObject Bullet;
    public UIBehaviour UI;

 



    void Start()
    {
        PV = GetComponent<PhotonView>();
        if(PV.IsMine){
        objectPooler = ObjectPooler.Instance;
        currentAmmo = AmmoCount;
        currentReloadTime = reloadTime;
        UI.OnChange(currentAmmo, "Ammo");
        }
    }



    void Update()
    {

        if (PV.IsMine)
        {

            //Prespective Camera
            //Getting the difference between the mouse position and the game object and setting the angle accordingly
            Ray MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float midpoint = (transform.position - Camera.main.transform.position).magnitude * 0.75f;
            Vector3 difference = (MouseRay.origin + MouseRay.direction * midpoint) - transform.position;
            difference.Normalize();
            float RotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, RotZ + Offset);

            //Orthographic Camera
            //Getting the difference between the mouse position and the game object and setting the angle accordingly
            // Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            // difference.Normalize();
            //float RotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0f, 0f, RotZ +Offset);





            //Controlling the gun rotation
            if (RotZ > 90 || RotZ < -90)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, -RotZ + 180);
            }
        }

    }

    void FixedUpdate()
    {
       

        if (PV.IsMine)
        {
            //Ammo controller
            if (currentAmmo <= 0 || (currentAmmo < AmmoCount && Input.GetKey(KeyCode.Mouse1))||(currentAmmo < AmmoCount && Input.GetKey(KeyCode.R)))
            {
                isReloading = true;
                Debug.Log("Reloading...");
            }

            //Firing
            if (!isReloading)
            {
                if (FireRate == 0)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Fire();
                        currentAmmo--;
                        UI.OnChange(currentAmmo, "Ammo");

                    }
                }
                else
                {
                    if (Input.GetButton("Fire1"))
                    {
                        if (TimetoFire <= 0)
                        {
                            TimetoFire = 1 / FireRate;
                            Fire();
                            currentAmmo--;
                            UI.OnChange(currentAmmo, "Ammo");
                        }
                        else
                            TimetoFire -= Time.deltaTime;

                    }
                }

            }

            //Reload Process
            else
            {
                if (currentReloadTime <= 0)
                {
                    currentAmmo = AmmoCount;
                    isReloading = false;
                    currentReloadTime = reloadTime;
                    UI.OnChange(currentAmmo, "Ammo");
                    Debug.Log("Done!");

                }
                else
                    currentReloadTime -= Time.deltaTime;
            }
        }
    }


    public void Fire()
    {

        //objectPooler.SpawnFromPool("Bullet",firePoint.position,firePoint.rotation);
        GameObject Temp = PhotonNetwork.Instantiate(Bullet.name, firePoint.position, firePoint.rotation);

    }




}