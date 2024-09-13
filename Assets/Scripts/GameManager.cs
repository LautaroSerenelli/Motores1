using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public int Score = 0;
    public TextMeshProUGUI ScoreText;
    public GameObject CienImage;
    private int MaxScore = 0;

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
        if (ScoreText == null)
        {
            ScoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            if (ScoreText == null)
            {
                Debug.LogError("No se encontró el componente TextMeshProUGUI.");
            }
        }

        UpdateScoreText();
    }

    public void AddScore(int Valor)
    {
        Score += Valor;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        ScoreText.text = "Puntuación: " + Score;

        if (Score == MaxScore && MaxScore > 0)
        {
            CienImage.SetActive(true);
        }
    }

    public void AddSpawnerPoints(int points)
    {
        MaxScore += points;
    }

    public int GetFinalScore()
    {
        return Score;
    }

    public float GetScorePorcentaje()
    {
        return (float)Score / MaxScore * 100f;
    }

    public bool MaxScoreObtenido()
    {
        return Score == MaxScore;
    }
}