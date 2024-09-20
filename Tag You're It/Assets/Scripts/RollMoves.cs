using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollMoves : MonoBehaviour
{
    public List<Transform> childs;
    public int pointsToVisit;
    public Transform pointer;
    public GameObject ouijaBoard;

    private Transform[] currentPoints;
    private int pointIndex;
    

    // Start is called before the first frame update
    void Start()
    {
        ResetRoll();
        StartCoroutine("RollDice");
    }

    /*void Update()
    {
        if(pointer.gameObject.activeInHierarchy && pointer.position != currentPoints[pointIndex].position)
        {
            pointer.position = Vector3.MoveTowards(pointer.position, currentPoints[pointIndex].position, .01f);
        }
        else
        {
            pointIndex++;
            
            if(pointIndex == currentPoints.Length)
            {
                pointer.gameObject.SetActive(false);
                ouijaBoard.SetActive(false);
            }
        }
    }*/

    private void ResetRoll()
    {
        pointIndex = 0;
        foreach (Transform child in transform)
        {
            childs.Add(child);
        }

        currentPoints = new Transform[pointsToVisit];

        for(int i = 0; i < currentPoints.Length; i++)
        {
            currentPoints[i] = childs[Random.Range(0, childs.Count-1)];
            if(i == currentPoints.Length-1 && currentPoints[i].GetComponent<Number>() == null)
                i--;
        }
    }

    IEnumerator RollDice()
    {
        while(pointIndex < currentPoints.Length)
        {
            if(pointer.gameObject.activeInHierarchy && pointer.position != currentPoints[pointIndex].position)
            {
                pointer.position = Vector3.MoveTowards(pointer.position, currentPoints[pointIndex].position, .01f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
                pointIndex++;
            }

            yield return new WaitForSeconds(.001f);
        }

        pointer.gameObject.SetActive(false);
        ouijaBoard.SetActive(false);
    }

}
