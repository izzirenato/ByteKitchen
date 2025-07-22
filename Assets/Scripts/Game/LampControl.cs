using UnityEngine;

public class LampControl : MonoBehaviour
{
    public Sprite lampOn;
    public Sprite lampOff;

    private SpriteRenderer _sr;

    void Awake()
    {
        _sr = gameObject.GetComponent<SpriteRenderer>();
        if (Consts.GameData.isLampOn)
        {
            _sr.sprite = lampOn;
        }
        else
        {
            _sr.sprite = lampOff;
        }

    }

    public void UpdateLampState()
    {
        GetComponent<AudioSource>().Play();
        if (Consts.GameData.isLampOn)
        {
            _sr.sprite = lampOff;
            Consts.GameData.isLampOn = false;
        }
        else
        {
            _sr.sprite = lampOn;
            Consts.GameData.isLampOn = true;
        }
    }
}
