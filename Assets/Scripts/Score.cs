using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    #region Fields

    [SerializeField]
    GameObject scoreTextGameObject;

    const string ScorePrefix = "Score: ";
    int score = 0;
    Text scoreText;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        scoreText = scoreTextGameObject.GetComponent<Text>();
        scoreText.text = ScorePrefix + score.ToString();

        EventManager.AddPointsAddedListener(AddPoints);

        List<int> classx = new List<int>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddPoints(int points)
    {
        score += points;
        scoreText.text = ScorePrefix + score.ToString();
    }
}
