using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SinglePlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    private PhotonView photonView;
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetButtonDown("Fire1"))
        {
            CmdShoot();
        }
    }

    void CmdShoot()
    {
        
        photonView.RPC("RpcShoot", RpcTarget.All, firePoint.position, firePoint.rotation);
    }

    [PunRPC]
    void RpcShoot(Vector3 position, Quaternion rotation)
    {
        Instantiate(projectilePrefab, position, rotation);
    }
}
