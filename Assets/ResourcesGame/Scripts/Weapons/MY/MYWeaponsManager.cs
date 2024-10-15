using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MyWeaponsManager : MonoBehaviourPunCallbacks
{
    public List<MyWeapon> weapons; // Lista de armas
    private float fireRate = 0f; // Intervalo de disparo
    public float Rate = 0.5f; // Intervalo de disparo
    private int currentWeaponIndex = 0; // Índice del arma actual
    private Transform camera;
    PhotonView photonView;
    private void Start()
    {
        camera = Camera.main.transform;

        photonView = GetComponent<PhotonView>();

        // Asegúrate de que la lista de armas no esté vacía
        if (weapons.Count > 0)
        {
            // Desactiva todas las armas excepto la primera
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].gameObject.SetActive(i == currentWeaponIndex);
            }
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        // Cambiar de arma al presionar la tecla de cambio (por ejemplo, "Q")
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeapon();
            photonView.RPC("RpcChangeWeapon", RpcTarget.All);
        }

        // Disparar al hacer clic izquierdo
        if (Input.GetButton("Fire1"))
        {
            if (fireRate > Rate)
            {
                fireRate = 0;
                // Obtén la posición y dirección de la cámara
                Vector3 origin = camera.position;
                Vector3 direction = camera.forward;
                weapons[currentWeaponIndex].Shoot_Master(origin, direction);
                // Realiza el disparo a través de RPC
                photonView.RPC("RpcShoot", RpcTarget.All, origin, direction);
            }
        }

        // Detener el disparo al soltar el botón izquierdo del ratón
        if (Input.GetButtonUp("Fire1"))
        {
            weapons[currentWeaponIndex].StopFire();
            photonView.RPC("RpcStopFire", RpcTarget.All);
        }

        fireRate += Time.deltaTime;
    }
    [PunRPC]
    private void RpcChangeWeapon()
    {
        // Desactiva la arma actual
        weapons[currentWeaponIndex].gameObject.SetActive(false);

        // Cambia al siguiente arma
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;

        // Activa la nueva arma
        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }
    [PunRPC]
    private void RpcShoot(Vector3 origin, Vector3 direction)
    {
        // Realiza el disparo
        weapons[currentWeaponIndex].Shoot_RPC(origin, direction);
    }

    [PunRPC]
    private void RpcStopFire()
    {
        // Detiene el disparo en la arma actual
        weapons[currentWeaponIndex].StopFire();
    }

    private void ChangeWeapon()
    {
        // Desactiva la arma actual
        weapons[currentWeaponIndex].gameObject.SetActive(false);

        // Cambia al siguiente arma
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;

        // Activa la nueva arma
        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }
}
