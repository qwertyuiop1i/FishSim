using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graphMaker : MonoBehaviour
{
    public GameObject pointPrefab;
    public LayerMask fishLayer;
    public float graphWidth = 10f;
    public float pointSpacing = 0.1f;

    public LineRenderer lr;

    private List<float> rednessData = new List<float>();
    private float elapsedTime = 0f;

    public void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 5f)
        {
            elapsedTime = 0f;
            CalculateAverageRedness();
            PlotPoint();
            Debug.Log("graph tick");
        }
    }

    private void CalculateAverageRedness()
    {
        int fishCount = 0;
        float totalRedness = 0f;

        GameObject[] fishObjects = GameObject.FindGameObjectsWithTag("greyfish");
        foreach (GameObject fish in fishObjects)
        {
            FishBehavior fishBehavior = fish.GetComponent<FishBehavior>();
            if (fishBehavior != null)
            {
                totalRedness += fishBehavior.rednessRatio;
                fishCount++;
            }
        }

        if (fishCount > 0)
        {
            float averageRedness = totalRedness / fishCount;
            Debug.Log(averageRedness);
            rednessData.Add(averageRedness);
        }
    }

    private void PlotPoint()
    {
        lr.positionCount = rednessData.Count;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        float xPosition = 4f;
        float pointSpacing = graphWidth / rednessData.Count;

        for (int i = 0; i < rednessData.Count; i++)
        {
            float yPosition = transform.position.y+3f + rednessData[i] * 5; //
            lr.SetPosition(i, new Vector3(xPosition + i * pointSpacing, yPosition, 0));
        }
    }
}
