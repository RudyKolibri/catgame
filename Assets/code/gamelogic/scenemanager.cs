using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanager : MonoBehaviour
{
    musicplayer music;
    public void play()
    {
        Invoke("scenedelay_gameplay", 0.05f);
    }
    private void scenedelay_gameplay()
    {
        SceneManager.LoadScene("gameplay");
    }
    public void credits()
    {
        Invoke("scenedelay_credits", 0.05f);
    }
    private void scenedelay_credits()
    {
        SceneManager.LoadScene("credits");
    }
    public void mainmenu()
    {
        Invoke("scenedelay_mainmenu", 0.05f);
    }
    private void scenedelay_mainmenu()
    {
        SceneManager.LoadScene("mainmenu");
        music = FindObjectOfType<musicplayer>();
        music.changetointro();
    }
}
