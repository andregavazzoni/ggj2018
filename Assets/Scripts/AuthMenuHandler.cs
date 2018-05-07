using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthMenuHandler : MonoBehaviour {


    public Button LoginButton;
    public Button SigninButton;
    public GameObject LoginForm;
    public GameObject SigninForm;
    public GameObject AuthMenu;

	void Awake()
	{
        LoginButton.onClick.AddListener(() => {
            SigninForm.SetActive(false);
            LoginForm.SetActive(true);
            AuthMenu.SetActive(false);
        });

        SigninButton.onClick.AddListener(() => {
            LoginForm.SetActive(false);
            SigninForm.SetActive(true);
            AuthMenu.SetActive(false);
        });
	}

    public void BackToMenu()
    {
        LoginForm.SetActive(false);
        SigninForm.SetActive(false);
        AuthMenu.SetActive(true);
    }
}
