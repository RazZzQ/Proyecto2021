using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class PlayerPhotonController : MonoBehaviour
{
    PhotonView photonView;
    public GameObject _camera;
    public TextMeshPro NickName;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if(photonView.IsMine == false)
        {
            CharacterController characterController = GetComponent<CharacterController>();
            CapsuleCollider capsuleCollider = this.gameObject.AddComponent<CapsuleCollider>();
            capsuleCollider.radius = 0.5f;
            capsuleCollider.height = 2;

            if (NickName != null)
            {
                NickName.text = photonView.Controller.NickName;
            }

            Destroy(_camera);
            Destroy(characterController);
            Destroy(GetComponent<FirstPersonController>());

            
        }
        else
        {
            if (NickName != null)
            {
                Destroy(NickName);
            }
        }
    }

    void Update()
    {
        if(photonView.IsMine == false)
        {
            if (NickName != null)
            {
                NickName.transform.LookAt(Camera.main.transform);
            }
        }
    }

}