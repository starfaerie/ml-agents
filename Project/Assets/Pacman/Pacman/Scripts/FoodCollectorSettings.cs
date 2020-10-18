using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;

public class FoodCollectorSettings : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] agents;

    [HideInInspector] public GameObject[] ghosts;
    [HideInInspector]
    public FoodCollectorArea[] listArea;

    public int totalScore;
    public int ghostScore;
    public Text scoreText;

    StatsRecorder m_Recorder;

    public void Awake()
    {
        Academy.Instance.OnEnvironmentReset += EnvironmentReset;
        m_Recorder = Academy.Instance.StatsRecorder;
    }

    void EnvironmentReset()
    {
        ClearObjects(GameObject.FindGameObjectsWithTag("food"));
        ClearObjects(GameObject.FindGameObjectsWithTag("badFood"));

        agents = GameObject.FindGameObjectsWithTag("agent");
        ghosts = GameObject.FindGameObjectsWithTag("ghost");
        listArea = FindObjectsOfType<FoodCollectorArea>();
        foreach (var fa in listArea)
        {
            fa.ResetFoodArea(agents,ghosts);
        }

        totalScore = 0;
        ghostScore = 0;
    }

    void ClearObjects(GameObject[] objects)
    {
        foreach (var food in objects)
        {
            Destroy(food);
        }
    }

    public void Update()
    {
        scoreText.text = $"Score: {totalScore}\nGhosts: {ghostScore}";

        // Send stats via SideChannel so that they'll appear in TensorBoard.
        // These values get averaged every summary_frequency steps, so we don't
        // need to send every Update() call.
        if ((Time.frameCount % 100) == 0)
        {
            m_Recorder.Add("TotalScore", totalScore);
            m_Recorder.Add("GhostScore", ghostScore);
        }
    }
}
