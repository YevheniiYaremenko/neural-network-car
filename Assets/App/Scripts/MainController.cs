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

    public bool trainMode = true;
    [SerializeField] Entity entity;

    [Header("Neural Network")]
    public int hiddenNeuronsCount = 9;
    public int inputCount = 5;
    public int outputCount = 2;

    [Header("Genetic Algorithm")]
    public int genomeCount = 15;
    [SerializeField] string logFilePath = "E:/UnityProjects/MyProjects/neural-network-car/Assets/Data/fitnessLog.txt";

    [Header("UI")]
    [SerializeField] Text status;
    [TextArea] [SerializeField] string statusFormat = "Current Fitness: {0:0.00}; \nBest Fitness: {1:0.00}; \nGenome: {2}/{3} \nGeneration: {4}";

    public void SaveData()
    {
        var weights = entity.genAlg.GetGenome(entity.genAlg.currentGenome).weights;
        for (int i = 0; i < weights.Count; i++)
        {
            PlayerPrefs.SetFloat(string.Format("Genome_{0}[{1}]", hiddenNeuronsCount, i), weights[i]);
        }
    }

    public List<float> GetData()
    {
        int totalWeights = (hiddenNeuronsCount + 1) * (inputCount + outputCount);
        var weights = new List<float>();

        for (int i = 0; i < totalWeights; i++)
        {
            string key = string.Format("Genome_{0}[{1}]", hiddenNeuronsCount, i);
            weights.Add(PlayerPrefs.GetFloat(key, 0));
        }
        return weights;
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
