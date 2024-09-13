using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WinScreenController : MonoBehaviour
{
    public TextMeshProUGUI FinalScoreText;
    public TextMeshProUGUI ScorePorcentajeText;
    public GameObject MaxScoreImage;
    public GameObject NotMaxScoreImage;

    private void Start()
    {
        FinalScoreText = GameObject.Find("FinalScoreText").GetComponent<TextMeshProUGUI>();
        ScorePorcentajeText = GameObject.Find("ScorePorcentajeText").GetComponent<TextMeshProUGUI>();

        DisplayResults();
    }

    private void DisplayResults()
    {
        int finalScore = GameManager.Instance.GetFinalScore();
        float scorePorcentaje = GameManager.Instance.GetScorePorcentaje();
        float floatRedondeada = MathF.Round(scorePorcentaje);
        int porRedondeado = (int)floatRedondeada;

        FinalScoreText.text = "" + finalScore;
        ScorePorcentajeText.text = "" + porRedondeado + "%";

        if (GameManager.Instance.MaxScoreObtenido())
        {
            MaxScoreImage.SetActive(true);
            NotMaxScoreImage.SetActive(false);
        }
        else
        {
            MaxScoreImage.SetActive(false);
            NotMaxScoreImage.SetActive(true);
        }
    }
}