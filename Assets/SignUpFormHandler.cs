using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUpFormHandler : MonoBehaviour
{

    private Firebase.Auth.FirebaseAuth _auth;

    private InputField UsernameInput;
    private InputField PasswordInput;

    private void Awake()
    {
        
    }

    public void SetUsername(InputField username) {
        UsernameInput = username;
    }

    public void SetPassword(InputField password)
    {
        PasswordInput = password;
    }

    public void SaveUser()
    {
        Debug.Log(UsernameInput.text);
        Debug.Log(PasswordInput.text);

        var auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.CreateUserWithEmailAndPasswordAsync(UsernameInput.text, PasswordInput.text).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }
}
