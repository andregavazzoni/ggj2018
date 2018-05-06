using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour {

    private List<string> Scenes = new List<string>();
    public GameObject SceneList;
    public GameObject SelectionButton;
    public float FadeTime = 2.0f;

	// Use this for initialization
	void Awake () {
        Debug.Log("Scene List");
        DataStorage.storage.Load();
        var Data = DataStorage.Data;
        
        for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var scene = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));


            Regex regex = new Regex("^Fase[0-9]$", RegexOptions.IgnoreCase);
            if (regex.IsMatch(scene))
            {
                Scenes.Add(scene);

                var button = Instantiate(SelectionButton, SceneList.transform);
                button.GetComponentInChildren<Text>().text = scene;
                button.transform.localScale = Vector3.one;
                button.GetComponent<Button>().enabled = scene == "Fase1" ? true : false;
                //button.GetComponent<Button>().enabled = true;

                if (Data.stages != null)
                {
                    foreach (var stage in Data.stages)
                    {
                        if (stage.id == scene && stage.completed)
                        {
                            button.GetComponent<Button>().enabled = true;
                        }
                    }
                }

                if (button.GetComponent<Button>().enabled)
                {
                    ColorBlock colorBlock = button.GetComponent<Button>().colors;
                    colorBlock.normalColor = Color.blue;
                    colorBlock.highlightedColor = Color.blue;
                    button.GetComponent<Button>().colors = colorBlock;
                }

                button.GetComponent<Button>().onClick.AddListener(() =>
                {
                    LoadScene(scene);
                });
            } 
        }
	}

    private void LoadScene(string scene)
    {
        Initiate.Fade(scene, Color.black, FadeTime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
