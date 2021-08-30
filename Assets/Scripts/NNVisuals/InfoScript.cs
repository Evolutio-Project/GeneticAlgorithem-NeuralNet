using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoScript : MonoBehaviour
{
    public TextMeshProUGUI fitnessText;
    public TextMeshProUGUI TrainingTypeText;
    float currentFitness;

    private void FixedUpdate() {
        switch (GameManager.instance.TrainingMode)
        {
            case GameManager.GENETIC:
                currentFitness = GameManager.instance.populationController.GetFittest().fitness();
                break;
            case GameManager.BACKPROPO:
                currentFitness = GameManager.instance.backPropManager.fitness();
                break;
        }
        fitnessText.text = "Fitness: " +  currentFitness;   
    }

    public void ChangeTrainingTypeName()
    {
        switch (GameManager.instance.TrainingMode)
        {
            case GameManager.GENETIC:
                TrainingTypeText.text = "Genetic";
                break;
            case GameManager.BACKPROPO:
                TrainingTypeText.text = "BackProp";
                break;
        }
    }
}
