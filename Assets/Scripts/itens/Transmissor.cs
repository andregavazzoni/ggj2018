using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transmissor : MonoBehaviour, Actionable {


    public float radius = 1f;
    public string nextScene;
    public ScoreRangeInSeconds ScoreRangeInSeconds = new ScoreRangeInSeconds();
    private float _startTime;
    
    public IEnumerable doAction(Player p) {
        if (Config.getInstance().IsAllInfosCollected())
        {
            Debug.Log("All infos collected!!!");
            FeedbackMessage.getInstance().AddMessage("Todas as informações coletadas", 5);
            var completionTime = Time.timeSinceLevelLoad;
            var score = 0;

            if (completionTime <= ScoreRangeInSeconds.ThreeStars)
            {
                score = 3;
            } else if (completionTime <= ScoreRangeInSeconds.TwoStars)
            {
                score = 2;
            } else if (completionTime <= ScoreRangeInSeconds.OneStar)
            {
                score = 1;
            }

            Stage stage = DataStorage.Data.stages.Find(x => x.id == SceneManager.GetActiveScene().name);

            if (stage == null)
            {
                stage = new Stage();
            }

            stage.id = SceneManager.GetActiveScene().name;
            stage.completed = true;
            stage.score = score;

            DataStorage.storage.Save();

            Config.getInstance().SetNextScene(nextScene);
            Initiate.Fade("Victory", Color.black, 2f);
        }
        else
        {
            Debug.Log("Go get the infos !!!");
            FeedbackMessage.getInstance().AddMessage("Volte e pege todas as informações", 5);
        }
        yield return null;
    }

}

[System.Serializable]
public class ScoreRangeInSeconds
{
    public int OneStar;
    public int TwoStars;
    public int ThreeStars;
}
