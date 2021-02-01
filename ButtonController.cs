using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    private SpriteRenderer _sR;
    public Sprite _defaultImage, _pressedImage;

    public GameObject _tambourEffects;

    public KeyCode _keyToPress1;

    public SpriteRenderer _dieuSR;

    public Sprite _dieuIdle, _dieuGauche, _dieuDroite;

    public AudioSource _gDrum, _dDrum;

    // Start is called before the first frame update
    void Start()
    {
        _sR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_keyToPress1))
        {
            _sR.sprite = _pressedImage;
            _tambourEffects.SetActive(true);

            if(_keyToPress1 == KeyCode.Q || _keyToPress1 == KeyCode.LeftArrow)
            {
                _dieuSR.sprite = _dieuGauche;

                if (!_gDrum.isPlaying)
                {
                    _gDrum.Play();
                }

            }
            else
            {
                _dieuSR.sprite = _dieuDroite;

                if (!_dDrum.isPlaying)
                {
                    _dDrum.Play();
                }

            }

        }

        if (Input.GetKeyUp(_keyToPress1))
        {
            _sR.sprite = _defaultImage;
            _tambourEffects.SetActive(false);

            _dieuSR.sprite = _dieuIdle;
        }
    }
}
