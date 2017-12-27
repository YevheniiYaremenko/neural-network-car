using UnityEngine;
using System.Collections;

public class hit : MonoBehaviour
{
	public int checkpoints;
	public Material Passed;
	public Vector3 init_pos;
	public Quaternion init_rotation;
	public bool crash;

	Agent agent;

	void Start ()
    {
		agent = gameObject.GetComponent<Agent> ();
		crash = false;
		checkpoints = 0;
		init_pos = transform.position;
		init_rotation = transform.rotation;
	}

	void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Checkpoint")
        {
			Renderer tmp = other.gameObject.GetComponent<Renderer> ();
			Checkpoint t = other.gameObject.GetComponent<Checkpoint>();
			bool p = t.passed;
            if (!p)
            {
                t.SetBool(true);
                tmp.material = Passed;
                checkpoints++;
                agent.dist += 1.0f;
            }
		}
        else
        {
			crash = true;
            MainController.Instance.LogGenomTest(GetComponent<Entity>().currentAgentFitness);
		}
	}
}
