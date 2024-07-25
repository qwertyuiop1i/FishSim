using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graphMaker : MonoBehaviour
{
    public GameObject pointPrefab;
    public LayerMask fishLayer;
    public float graphWidth = 10f;
    public float pointSpacing = 0.1f;

    private List<float> rednessData = new List<float>();
    private float elapsedTime = 0f;

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
        // Clear existing points
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        float xPosition = 0f;
        float pointSpacing = graphWidth / rednessData.Count;

        for (int i = 0; i < rednessData.Count; i++)
        {
            float yPosition = transform.position.y + rednessData[i] * 5; //
            GameObject newPoint = Instantiate(pointPrefab, new Vector3(xPosition + i * pointSpacing, yPosition, 0), Quaternion.identity,transform);
        }
    }
}
