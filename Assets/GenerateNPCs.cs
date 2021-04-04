using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNPCs : MonoBehaviour
{
    [SerializeField] List<GameObject> NPCs;
    [SerializeField] float targetXPositionBeginning;
    [SerializeField] float targetXPositionEnd;
    [SerializeField] float targetZPositionBeginning;
    [SerializeField] float targetZPositionEnd;
    [SerializeField] int targetCountOfNPCs;
    float xPosition;
    float zPosition;
    int countOfNPCs;
    // Start is called before the first frame update
    void Start()
    {
        NPCDrop();
    }

    void NPCDrop()
    {
        while(countOfNPCs < targetCountOfNPCs)
        {
            xPosition = Random.Range(targetXPositionBeginning, targetXPositionEnd);
            zPosition = Random.Range(targetZPositionBeginning, targetZPositionEnd);
            Instantiate(NPCs[Random.Range(0, NPCs.Count)], new Vector3(xPosition, 440, zPosition), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            //yield return new WaitForSeconds(0.1f);
            countOfNPCs += 1;
        }
    }
}
