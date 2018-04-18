using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {

    private string nextScene;
    public float fadeTime = 2.0f;

    void Start()
    {
        nextScene = Config.getInstance().GetNextScene();
    }

    void Update()
    {
        
        if (string.IsNullOrEmpty(nextScene))
        {
            Initiate.Fade("Menu", Color.black, fadeTime);
        }
        else
        {
            Initiate.Fade(nextScene, Color.black, fadeTime);
        }

    }
}
