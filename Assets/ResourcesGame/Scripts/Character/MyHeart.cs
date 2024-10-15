using UnityEngine;
using Photon.Pun;

public class MyHeart : MonoBehaviourPunCallbacks
{
    public float healthMax = 100f; // Salud m�xima
    public float health; // Salud actual

    private void Start()
    {
        // Inicializa la salud actual al m�ximo
        health = healthMax;
    }

    // M�todo para recibir da�o
    public void TakeDamage(float damage)
    {
        // Solo permite da�o si no se ha muerto
        if (health > 0)
        {
            photonView.RPC("RpcTakeDamage", RpcTarget.All, damage);
        }
    }

    [PunRPC]
    private void RpcTakeDamage(float damage)
    {
        if (health - damage > 0)
        {
            health -= damage; // Resta el da�o a la salud

            // Aseg�rate de que la salud no baje de 0
            if (health < 0)
            {
                health = 0;
                Die(); // Llama a un m�todo para manejar la muerte (opcional)
            }
        }
    }

    // M�todo opcional para manejar la muerte del personaje
    private void Die()
    {
        Debug.Log("El personaje ha muerto.");
        // Aqu� puedes agregar l�gica adicional, como desactivar el personaje, reproducir una animaci�n, etc.
    }

    // M�todo para obtener la salud actual (opcional)
    public float GetHealth()
    {
        return health;
    }
}
