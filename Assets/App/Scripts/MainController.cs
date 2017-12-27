using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    static MainController instance;
    public static MainController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MainController>();
            }
            return instance;
        }
    }

    [SerializeField] Button trainButton;
    [SerializeField] Button testButton;
    [SerializeField] Text status;
    [TextArea] [SerializeField] string statusFormat = "Current Fitness: {0:0.00}; \nBest Fitness: {1:0.00}; \nGenome: {2}/{3} \nGeneration: {4}";

    public void Train()
    {
        //TODO
        trainButton.interactable = false;
        testButton.interactable = true;
    }

    public void Test()
    {
        //TODO
        trainButton.interactable = true;
        testButton.interactable = false;
    }

    public void SaveData()
    {
        //TODO
    }

    public void UpdateStatus(
        float currentFitness,
        float bestFitness,
        int currentGenome,
        int maxGenome,
        int currentGeneration
        )
    {
        status.text = string.Format(
            statusFormat,
            currentFitness,
            bestFitness,
            currentGenome,
            maxGenome,
            currentGeneration);
    }
}
