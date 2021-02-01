using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIslands : MonoBehaviour
{

    public GameObject _ile1 , _ile2 , _ile3, _ile4, _ile5, _ile6, _ile7, _ile8;
    public GameObject _cor1, _cor2;

    // Start is called before the first frame update
    void Start()
    {
        _ile1.SetActive(false);
        _ile2.SetActive(false);
        _ile3.SetActive(false);
        _ile4.SetActive(false);
        _ile5.SetActive(false);
        _ile6.SetActive(false);
        _ile7.SetActive(false);
        _ile8.SetActive(false);

        _cor1.SetActive(false);
        _cor2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager._instance._startPlaying)
        {
            _ile1.SetActive(true);
            _ile2.SetActive(true);
            _ile3.SetActive(true);
            _ile4.SetActive(true);
            _ile5.SetActive(true);
            _ile6.SetActive(true);
            _ile7.SetActive(true);
            _ile8.SetActive(true);

            _cor1.SetActive(true);
            _cor2.SetActive(true);
        }

    }
}
