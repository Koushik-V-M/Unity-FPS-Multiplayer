using UnityEngine;
using UnityEngine.Networking;

//[RequireComponent(typeof(playerWeapon))]
public class playerShoot : NetworkBehaviour
{
    public playerWeapon weapon;
    
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    private bool fireRateSwitch;
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            fireRateSwitch = false;
;        }
        if (cam == null)
        {
            Debug.Log("no camera present");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (pausemenu.isOn)
        {
            return;
        }
        //checking the switch in fire rate
        if (Input.GetKeyDown(KeyCode.F))
        {
            fireRateSwitch = !fireRateSwitch;
        }


        if (!fireRateSwitch)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f / weapon.fireRateWeapon);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }
        
    }

    [Client]
    void Shoot()
    {
        print("shot");
        CmdOnShoot();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
        {
            if (hit.collider.transform.root.tag == "Player")
            {
                CmdPlayerShot(hit.transform.root.name,weapon.damage,transform.name);
            }
            CmdOnHit(hit.point, hit.normal);
        }
    }

    [Command]
    void CmdPlayerShot(string playerid,int damage,string sourceID)
    {
        print(playerid + " got shot");

        player _player = gameManager.getPlayer(playerid);
        _player.RpctakeDamage(damage,sourceID);
    }

    [Command]
    void CmdOnShoot()
    {
        RpcOnShoot();
    }
    [ClientRpc]
    void RpcOnShoot()
    {
        //weapon.gunFlare.Play;
        ParticleSystem ps = weapon.gunFlare;
        ps.Play();
        //Destroy(ps, 1f);
    }

    [Command]
    void CmdOnHit(Vector3 point, Vector3 normal)
    {
        RpcOnHit(point, normal);
    }

    [ClientRpc]
    void RpcOnHit(Vector3 point, Vector3 normal)
    {
        GameObject hitEffect = (GameObject)Instantiate(weapon.hitEffect, point, Quaternion.LookRotation(normal));
        Destroy(hitEffect, 1.5f);
    }
}