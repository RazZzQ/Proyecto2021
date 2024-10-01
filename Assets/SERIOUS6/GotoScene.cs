using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene : MonoBehaviour
{
    public void GotoVoid(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}