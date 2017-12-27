using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

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

    [Header("Neural Network")]
    public int hiddenNeuronsCount = 9;
    public int inputCount = 5;
    public int outputCount = 2;

    [Header("Genetic Algorithm")]
    public int genomeCount = 15;
    [SerializeField] string logFilePath = "E:/UnityProjects/MyProjects/neural-network-car/Assets/Data/fitnessLog.txt";


    [Header("UI")]
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

    public void LogGenomTest(float fitness)
    {
        using (StreamWriter sw = File.AppendText(logFilePath))
        {
            sw.WriteLine(fitness);
        }
    }
}
