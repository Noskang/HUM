using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    public GameObject OptionUI;
    // Start is called before the first frame update
    

    public void OptionOn()
    {
        GameManager.Instance.AudioManager.PlaySfx(AudioManager.Sfx.uiSound);
        OptionUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OptionOff()
    {
        GameManager.Instance.AudioManager.PlaySfx(AudioManager.Sfx.uiSound);
        OptionUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
