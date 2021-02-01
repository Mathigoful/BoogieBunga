using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator _porte1, _porte2;

    public AudioSource _miscSound;
    public AudioSource _mainMenu;

    public AudioSource _clicSound;

    public void StartGame()
    {
        _clicSound.Play();

        _mainMenu.Stop();

        _miscSound.Play();

        StartCoroutine("WaitSound");
    }

    public void QuitGame()
    {
        _clicSound.Play();

        Debug.Log("Application Quitted");
        Application.Quit();
    }

    IEnumerator WaitSound()
    {
        _porte1.SetBool("_buttonSelected", true);
        _porte2.SetBool("_buttonSelected", true);
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Start");
    }

}
