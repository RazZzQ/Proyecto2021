using UnityEngine;
using Photon.Pun;

public class MyHeart : MonoBehaviourPunCallbacks
{
    public float healthMax = 100f; // Salud máxima
    public float health; // Salud actual

    private void Start()
    {
        // Inicializa la salud actual al máximo
        health = healthMax;
    }

    // Método para recibir daño
    public void TakeDamage(float damage)
    {
        // Solo permite daño si no se ha muerto
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
            health -= damage; // Resta el daño a la salud

            // Asegúrate de que la salud no baje de 0
            if (health < 0)
            {
                health = 0;
                Die(); // Llama a un método para manejar la muerte (opcional)
            }
        }
    }

    // Método opcional para manejar la muerte del personaje
    private void Die()
    {
        Debug.Log("El personaje ha muerto.");
        // Aquí puedes agregar lógica adicional, como desactivar el personaje, reproducir una animación, etc.
    }

    // Método para obtener la salud actual (opcional)
    public float GetHealth()
    {
        return health;
    }
}
