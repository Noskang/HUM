using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public enum SoundType { BGM,SFX}
    //유니티에서 선택되는 타입
    public SoundType type;

    //슬라이더 
    Slider slider;
    // Start is called before the first frame update
    private void Awake()
    {
        slider = GetComponent<Slider>(); 
    }
    private void Start()
    {
        switch (type)
        {
            case SoundType.BGM:
                if (GameManager.Instance.Load("BGM") == -1)
                {
                    slider.value = 0.5f;
                }
                else
                {
                    slider.value = GameManager.Instance.Load("BGM");
                }
                break;
            case SoundType.SFX:
                if (GameManager.Instance.Load("SFX") == -1)
                {
                    slider.value = 0.5f;
                }
                else
                {
                    slider.value = GameManager.Instance.Load("SFX");
                }
                break;
        }
    }
    private void LateUpdate()
    {
        if (GameManager.Instance == null)
            return;
        switch (type)
        {
            case SoundType.BGM:
                GameManager.Instance.AudioManager.SaveBGMSound(slider.value);
                break;
            case SoundType.SFX:
                GameManager.Instance.AudioManager.SaveSFXSound(slider.value);
                break;
        }
    }
}
