using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanager : MonoBehaviour
{
    public void play()
    {
        Invoke("scenedelay", 0.05f);
    }
    private void scenedelay()
    {
        SceneManager.LoadScene("gameplay");
    }
}
