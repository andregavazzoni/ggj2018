using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

public class LoginFormHandler : MonoBehaviour {

    private Firebase.Auth.FirebaseAuth _auth;

    private InputField UsernameInput;
    private InputField PasswordInput;

    private void Awake()
    {
        Debug.Log("SignUpFormHandler initialized");
        DataStorage.storage.Load();
        Debug.Log(DataStorage.userId);
        Debug.Log("SignUpFormHandler initialized ended");
    }

    public void SetUsername(InputField username)
    {
        UsernameInput = username;
    }

    public void SetPassword(InputField password)
    {
        PasswordInput = password;
    }

    public void LoginUser()
    {
        var username = (UsernameInput.text ?? "").Trim();
        var password = (PasswordInput.text ?? "").Trim();
        Text usernameError = UsernameInput.gameObject.transform.Find("Error").GetComponent("Text") as Text;
        Text passwordError = PasswordInput.gameObject.transform.Find("Error").GetComponent("Text") as Text;
        usernameError.text = "";
        var hasError = false;

        Debug.Log(username);
        Debug.Log(password);


        if (username == "")
        {
            hasError = true;
            usernameError.text = "E-mail é obrigatório";
        }

        if (password == "")
        {
            hasError = true;
            passwordError.text = "Senha é obrigatório";
        }

        Debug.Log("Has Error: " + hasError.ToString());

        if (!hasError)
        {
            Debug.Log("Calling FirebaseAuth");
            var auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            auth.SignInWithEmailAndPasswordAsync(username, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }

                if (task.IsFaulted)
                {
                    Firebase.FirebaseException firebaseException = task.Exception.InnerExceptions[0] as Firebase.FirebaseException;
                    Debug.LogError("Error Message: " + firebaseException.Message);
                    Debug.LogError("Error Code: " + firebaseException.ErrorCode);


                    //if ((AuthError)firebaseException.ErrorCode == AuthError.InvalidEmail)
                    //{
                    //    usernameError.text = "E-mail inválido.";
                    //}

                    return;
                }

                // Firebase user has been created.
                Firebase.Auth.FirebaseUser user = task.Result;
                Debug.Log(user);
                DataStorage.userId = user.UserId;
                DataStorage.storage.Save();
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                                user.DisplayName, user.UserId);
                Initiate.Fade("Scenes/Levels/Fase1",Color.black, 2);
            });
        }
    }
}
