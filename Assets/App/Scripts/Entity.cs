using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour {

	Agent testAgent;
	private List<Agent> agents;
	public float currentAgentFitness;
	public float bestFitness;
	private float currentTimer;
	private int checkPointsHit;

	public NNet neuralNet;

	public GA genAlg;
	public int checkpoints;
	public GameObject[] CPs;
	public Material normal;

	private Vector3 defaultpos;

    MainController controller;

	hit hit;
    
	void Start ()
    {
        controller = MainController.Instance;

        genAlg = new GA ();
        int totalWeights = (controller.hiddenNeuronsCount + 1) * (controller.inputCount + controller.outputCount);
		genAlg.GenerateNewPopulation (controller.genomeCount, totalWeights);
		currentAgentFitness = 0.0f;
		bestFitness = 0.0f;

		neuralNet = new NNet ();
		neuralNet.CreateNet (1, controller.inputCount, controller.hiddenNeuronsCount, controller.outputCount);
		Genome genome = genAlg.GetNextGenome ();
		neuralNet.FromGenome (genome, controller.inputCount, controller.hiddenNeuronsCount, controller.outputCount);

		testAgent = gameObject.GetComponent<Agent>();
		testAgent.Attach (neuralNet);

		hit = gameObject.GetComponent<hit> ();
		checkpoints = hit.checkpoints;
		defaultpos = transform.position;
	}
    
	void Update ()
    {
		checkpoints = hit.checkpoints;
		if (testAgent.hasFailed)
        {
			if(genAlg.GetCurrentGenomeIndex() == controller.genomeCount - 1)
            {

				EvolveGenomes();
				return;
			}
            NextTestSubject();
		}
		currentAgentFitness = testAgent.dist;
		if (currentAgentFitness > bestFitness)
        {
			bestFitness = currentAgentFitness;
		}

        controller.UpdateStatus(
            currentAgentFitness,
            bestFitness,
            genAlg.currentGenome,
            genAlg.totalPopulation,
            genAlg.generation);
    }

	public void NextTestSubject()
    {
		genAlg.SetGenomeFitness (currentAgentFitness, genAlg.GetCurrentGenomeIndex ());
		currentAgentFitness = 0.0f;
		Genome genome = genAlg.GetNextGenome ();

		neuralNet.FromGenome (genome, controller.inputCount, controller.hiddenNeuronsCount, controller.outputCount);

		transform.position = defaultpos;
		transform.eulerAngles = controller.clockwise ? controller.clockwiseRotation : controller.inverseClockwiseRotation;

		testAgent.Attach (neuralNet);
		testAgent.ClearFailure ();

		//reset the checkpoints
		CPs = GameObject.FindGameObjectsWithTag ("Checkpoint");

		foreach (GameObject c in CPs)
        {
			Renderer tmp = c.gameObject.GetComponent<Renderer>();
			tmp.material = normal;
			Checkpoint p = c.gameObject.GetComponent<Checkpoint>();
			p.passed = false;
		}
	}

	public void BreedNewPopulation()
    {
		genAlg.ClearPopulation ();
        int totalWeights = (controller.hiddenNeuronsCount + 1) * (controller.inputCount + controller.outputCount);
        genAlg.GenerateNewPopulation(controller.genomeCount, totalWeights);
    }

	public void EvolveGenomes()
    {
		genAlg.BreedPopulation ();
		NextTestSubject ();
	}

	public int GetCurrentMemberOfPopulation()
    {
		return genAlg.GetCurrentGenomeIndex ();
	}
}
