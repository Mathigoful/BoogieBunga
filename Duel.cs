using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duel : MonoBehaviour
{

    public GameObject _tempestTrigger;

    public GameObject _buttons;
    public GameObject _NoteHolder;

    public GameObject _timer;

    public GameObject _p1Button, _p2Button;
    public SpriteRenderer _pLeft, _pRight, _p1But, _p2But;

    public Sprite _p1N, _p2N, _p1Pressed, _p2Pressed;
    public GameObject _duelScreen;

    public GameObject _i1, _i2, _i3, _i4, _i5, _i6, _i7, _i8;

    public Animator _gDoor, _dDoor;

    public bool _tempestTriggered = false;

    public int _p1Points, _p2Points;

    public KeyCode _keyDuelP1, _keyDuelP2;

    // Start is called before the first frame update
    void Start()
    {
        _p1Points = 0;
        _p2Points = 0;
        _tempestTrigger.SetActive(false);
        StartCoroutine("WaitForActive");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Escape Pressed");

            //HideUnhideIslands();

            _buttons.SetActive(false);
            _NoteHolder.SetActive(false);
            _timer.SetActive(false);

            GameManager._instance._theMusic.Pause();
            GameManager._instance.StopCoroutine("TimePassing");

            _tempestTrigger.SetActive(false);

            _tempestTriggered = true;
        }

        if (_tempestTriggered)
        {
            _duelScreen.SetActive(true);
            _p1Button.SetActive(true);
            _p2Button.SetActive(true);

            StartCoroutine("TempestDuration");

            if (Input.GetKeyDown(_keyDuelP1))
            {
                _p2But.sprite = _p2Pressed;
                _p1Points++;
            }

            else if (Input.GetKeyDown(_keyDuelP2))
            {
                _p1But.sprite = _p1Pressed;
                _p2Points++;
            }

            if (Input.GetKeyUp(_keyDuelP1))
            {
                _p2But.sprite = _p2N;
            }

            else if (Input.GetKeyUp(_keyDuelP2))
            {
                _p1But.sprite = _p1N;
            }
        }
    }

    public void DuelVisibility()
    {
        _tempestTriggered = false;

        _gDoor.SetBool("_change", true);
        _dDoor.SetBool("_change", true);

        _tempestTrigger.SetActive(false);
        _p1Button.SetActive(false);
        _p2Button.SetActive(false);

        _buttons.SetActive(true);
        _NoteHolder.SetActive(true);
        _timer.SetActive(true);

        //HideUnhideIslands();

        GameManager._instance._theMusic.Play();

        GameManager._instance.StartCoroutine("TimePassing");

        StartCoroutine("WaitForActive");
    }

    IEnumerator WaitForActive()
    {
        Debug.Log("Wait For Active Started");
        yield return new WaitForSeconds(35);
        _tempestTrigger.SetActive(true);

        StopCoroutine("WaitForActive");
    }

    IEnumerator TempestDuration()
    {

        Debug.Log("Tempest Started");

        yield return new WaitForSeconds(8);

        GameManager._instance._p1CurrentScore += _p1Points * 2;
        GameManager._instance._p2CurrentScore += _p2Points * 2;

        GameManager._instance._p1Score.text = "Score: " + GameManager._instance._p1CurrentScore;
        GameManager._instance._p2Score.text = "Score: " + GameManager._instance._p2CurrentScore;

        DuelVisibility();

        StopCoroutine("TempestDuration");
    }
}
