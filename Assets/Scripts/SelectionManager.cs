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

        var nextEnabled = false;
        
        for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var scene = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            Color buttonColor = Color.gray;

            Regex regex = new Regex("^Fase[0-9]$", RegexOptions.IgnoreCase);
            if (regex.IsMatch(scene))
            {
                Scenes.Add(scene);

                var button = Instantiate(SelectionButton, SceneList.transform);
                button.GetComponentInChildren<Text>().text = scene;
                button.transform.localScale = Vector3.one;
                button.GetComponent<Button>().enabled = scene == "Fase1" ? true : false;
                //button.GetComponent<Button>().enabled = true;

                if (button.GetComponent<Button>().enabled = scene == "Fase1" || nextEnabled)
                {
                    buttonColor = Color.blue;
                    nextEnabled = false;
                }
                

                Debug.Log("Stages Count: " + DataStorage.Data.stages.Count);

                if (DataStorage.Data.stages.Count > 0)
                {

                    DataStorage.Data.stages.ForEach(x =>
                    {
                        if (x.id == scene && x.completed)
                        {
                            button.GetComponent<Button>().enabled = true;
                            buttonColor = Color.green;
                            nextEnabled = true;
                        }
                    });
                }

                if (button.GetComponent<Button>().enabled)
                {
                    ColorBlock colorBlock = button.GetComponent<Button>().colors;
                    colorBlock.normalColor = buttonColor;
                    colorBlock.highlightedColor = buttonColor;
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
