using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoMainMenu : MonoBehaviour
{

    public float _Time = 62f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("VideoPlaying");
    }

    IEnumerator VideoPlaying()
    {
        yield return new WaitForSeconds(_Time);
        SceneManager.LoadScene("Main Menu");
    }
}
