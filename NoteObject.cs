using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool _canBePressed;

    public KeyCode _keyToPress;

    public GameObject _p1Normal, _p1Good, _p1Perfect, _p1Miss, _p2Normal, _p2Good, _p2Perfect, _p2Miss;

    public Sprite _nSprite, _bSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_keyToPress) && gameObject.tag == "P1")
        {
            if (_canBePressed)
            {
                ChangeSprite();

                if(Mathf.Abs(transform.position.y) > 0.25)
                {
                    Debug.Log("P1Hit");
                    GameManager._instance.P1NormalHit();
                    Instantiate(_p1Normal, new Vector3(4, transform.position.y, transform.position.z), _p1Normal.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("P1Good Hit");
                    GameManager._instance.P1GoodHit();
                    Instantiate(_p1Good, new Vector3(4, transform.position.y, transform.position.z), _p1Good.transform.rotation);
                }
                else
                {
                    Debug.Log("P1Perfect");
                    GameManager._instance.P1PerfectHit();
                    Instantiate(_p1Perfect, new Vector3(4, transform.position.y, transform.position.z), _p1Perfect.transform.rotation);
                }
            }
        }

        else if (Input.GetKeyDown(_keyToPress) && gameObject.tag == "P2")
        {
            if (_canBePressed)
            {
                ChangeSprite();

                if (Mathf.Abs(transform.position.y) > 0.25)
                {
                    Debug.Log("P2Hit");
                    GameManager._instance.P2NormalHit();
                    Instantiate(_p2Normal, new Vector3(-4, transform.position.y, transform.position.z), _p2Normal.transform.rotation) ;
                }
                else if (Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("P2Good Hit");
                    GameManager._instance.P2GoodHit();
                    Instantiate(_p2Good, new Vector3(-4, transform.position.y, transform.position.z), _p2Good.transform.rotation);
                }
                else
                {
                    Debug.Log("P2Perfect");
                    GameManager._instance.P2PerfectHit();
                    Instantiate(_p2Perfect, new Vector3(-4, transform.position.y, transform.position.z), _p2Perfect.transform.rotation);
                }

            }
        }
    }

    public void ChangeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _bSprite;
        StartCoroutine("MaskAsset");
    }

    IEnumerator MaskAsset()
    {
        yield return new WaitForSeconds(0.08f);
        gameObject.SetActive(false);
        StopCoroutine("MaskAsset");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            _canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.tag == "P1" && gameObject.activeInHierarchy)
        {
            _canBePressed = false;

            GameManager._instance.P1NoteMissed();
            Instantiate(_p1Miss, new Vector3(4, transform.position.y, transform.position.z), _p1Miss.transform.rotation);
        }

        else if (other.tag == "Activator" && gameObject.tag == "P2" && gameObject.activeInHierarchy)
        {
            _canBePressed = false;

            GameManager._instance.P2NoteMissed();
            Instantiate(_p2Miss, new Vector3(-4, transform.position.y, transform.position.z), _p2Miss.transform.rotation);
        }
    }
}
