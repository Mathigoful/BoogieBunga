using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public AudioSource _theMusic;

    public bool _startPlaying;

    public BeatScroller _beatS;

    public static GameManager _instance;

    public int _p1CurrentScore, _p2CurrentScore;
    public int _scorePerNote = 100;
    public int _scorePerGoodNote = 125;
    public int _scorePerPerfect = 150;

    public int _p1CurrentMultiplier, _p2CurrentMultiplier;
    public int _p1MultiplierTracker, _p2MultiplierTracker;
    public int[] _p1multiplierTresholds, _p2multiplierTresholds;

    public Text _p1Score, _p2Score, _p1Multi, _p2Multi;

    public float _MusicTime = 65;
    public Text _timer;
    public GameObject _p1Win, _p2Win;

    public GameObject _mainCanvas;
    public Animator _pG, _pD;

    public SpriteRenderer _boug1, _boug2, _boug3, _boug4, _boug5, _boug6;
    public Sprite _boug1Down, _boug1Up, _boug2Down, _boug2Up;

    public AudioSource _p1sad, _p1angry, _p1Perfect, _p2sad, _p2angry, _p2Perfect;
    public AudioSource _p1godCoco, _p2godCoco;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        _p1Score.text = "Score: 0";
        _p2Score.text = "Score: 0";

        _p1CurrentMultiplier = 1;
        _p2CurrentMultiplier = 1;

        _mainCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_startPlaying)
        {
            if (Input.anyKeyDown)
            {
                _pG.SetBool("_gameBeginning", true);
                _pD.SetBool("_gameBeginning", true);

                _mainCanvas.SetActive(true);

                _startPlaying = true;
                _beatS._hasStarted = true;

                _theMusic.Play();
                StartCoroutine("TimePassing");
            }
        }

        Timer();
        EndTimer();

    }

    public void P1NoteHit()
    {
        Debug.Log("P1 Hit On Time");

        StartCoroutine("HypeBougsP2");

        if (_p1CurrentMultiplier - 1 < _p1multiplierTresholds.Length)
        {

            _p1MultiplierTracker++;

            if (_p1multiplierTresholds[_p1CurrentMultiplier - 1] <= _p1MultiplierTracker)
            {
                _p1MultiplierTracker = 0;
                _p1CurrentMultiplier++;
            }
        }

        _p1Multi.text = "Multiplier: x" + _p1CurrentMultiplier;

        //_p1CurrentScore += _scorePerNote * _p1CurrentMultiplier;
        _p1Score.text = "Score: " + _p1CurrentScore;
    }

    public void P1NoteMissed()
    {
        if (!_p1sad.isPlaying)
        {
            _p1sad.Play();
        }

        Debug.Log("P1 Missed Note");

        _p1CurrentMultiplier = 1;
        _p1MultiplierTracker = 0;

        _p1Multi.text = "Multiplier: x" + _p1CurrentMultiplier;
    }

    public void P1NormalHit()
    {
        _p1CurrentScore += _scorePerNote * _p1CurrentMultiplier;
        P1NoteHit();
    }

    public void P1GoodHit()
    {
        _p1CurrentScore += _scorePerGoodNote * _p1CurrentMultiplier;
        P1NoteHit();
    }

    public void P1PerfectHit()
    {
        if (!_p1Perfect.isPlaying)
        {
            _p1Perfect.Play();
        }

        if (!_p1godCoco.isPlaying)
        {
            _p1godCoco.Play();
        }

        _p1CurrentScore += _scorePerPerfect * _p1CurrentMultiplier;
        P1NoteHit();
    }

    public void P2NoteHit()
    {
        Debug.Log("P2 Hit On Time");

        StartCoroutine("HypeBougsP1");

        if (_p2CurrentMultiplier - 1 < _p2multiplierTresholds.Length)
        {
            _p2MultiplierTracker++;

            if (_p2multiplierTresholds[_p2CurrentMultiplier - 1] <= _p2MultiplierTracker)
            {
                _p2MultiplierTracker = 0;
                _p2CurrentMultiplier++;
            }
        }

        _p2Multi.text = "Multiplier: x" + _p2CurrentMultiplier;

        _p2CurrentScore += _scorePerNote * _p2CurrentMultiplier;
        _p2Score.text = "Score: " + _p2CurrentScore;
    }

    public void P2NoteMissed()
    {

        if (!_p2sad.isPlaying)
        {
            _p2sad.Play();
        }

        Debug.Log("P2 Missed Note");

        _p2CurrentMultiplier = 1;
        _p2MultiplierTracker = 0;

        _p2Multi.text = "Multiplier: x" + _p2CurrentMultiplier;
    }

    public void P2NormalHit()
    {

        _p2CurrentScore += _scorePerNote * _p2CurrentMultiplier;
        P2NoteHit();
    }

    public void P2GoodHit()
    {

        _p2CurrentScore += _scorePerGoodNote * _p2CurrentMultiplier;
        P2NoteHit();
    }

    public void P2PerfectHit()
    {
        if (!_p2Perfect.isPlaying)
        {
            _p2Perfect.Play();
        }

        if (!_p2godCoco.isPlaying)
        {
            _p2godCoco.Play();
        }

        _p2CurrentScore += _scorePerPerfect * _p2CurrentMultiplier;
        P2NoteHit();
    }

    public void Timer()
    {
        _timer.text = _MusicTime.ToString();
    }

    public void EndTimer()
    {
        if (_MusicTime <= 0)
        {
            Debug.Log("Game Ended");

            _MusicTime = 0;
            _timer.text = ("0");

            if (_p1CurrentScore > _p2CurrentScore)
            {
                _p1Win.SetActive(true);
            }
            else
            {
                _p2Win.SetActive(true);
            }

            StartCoroutine("OpenMainMenu");

        }
    }

    public void ChangeSpriteBoug123()
    {
        if (_boug1)
        {
            _boug1.sprite = _boug1Up;
        }
        if (_boug2)
        {
            _boug2.sprite = _boug1Down;
        }
        if (_boug3)
        {
            _boug3.sprite = _boug1Up;
        }
    }

    public void BaseSpriteBoug123()
    {
        if (_boug1)
        {
            _boug1.sprite = _boug1Down;
        }
        if (_boug2)
        {
            _boug2.sprite = _boug1Up;
        }
        if (_boug3)
        {
            _boug3.sprite = _boug1Down;
        }
    }

    public void ChangeSpriteBoug345()
    {
        if (_boug4)
        {
            _boug4.sprite = _boug2Up;
        }
        if (_boug5)
        {
            _boug5.sprite = _boug2Down;
        }
        if (_boug6)
        {
            _boug6.sprite = _boug2Up;
        }
    }

    public void BaseSpriteBoug345()
    {
        if (_boug4)
        {
            _boug4.sprite = _boug2Down;
        }
        if (_boug5)
        {
            _boug5.sprite = _boug2Up;
        }
        if (_boug6)
        {
            _boug6.sprite = _boug2Down;
        }
    }

    IEnumerator TimePassing()
    {
        yield return new WaitForSeconds(1);
        _MusicTime--;
        StartCoroutine("TimePassing");
    }

    IEnumerator HypeBougsP1()
    {
        Debug.Log("ChangeSpriteP1 Started");

        ChangeSpriteBoug123();
        yield return new WaitForSeconds(1);
        BaseSpriteBoug123();

        StopCoroutine("HypeBougsP1");
    }

    IEnumerator HypeBougsP2()
    {
        Debug.Log("ChangeSpriteP2 Started");

        ChangeSpriteBoug345();
        yield return new WaitForSeconds(1);
        BaseSpriteBoug345();

        StopCoroutine("HypeBougsP2");
    }

    IEnumerator OpenMainMenu()
    {
        Debug.Log("Main Menu Coro Started");

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("Main Menu");
    }
}
