using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public int Score = 0;
    public Text ScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreText();
    }

    void AddScore(int Valor)
    {
        Score += Valor;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        ScoreText.text = "Puntuación: " + Score;
    }
}