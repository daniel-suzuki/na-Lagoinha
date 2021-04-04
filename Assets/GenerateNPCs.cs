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
    [SerializeField] List<Texture2D> skins;

    
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
            GameObject t;
            t =  (GameObject) Instantiate(NPCs[0], new Vector3(xPosition, 350, zPosition), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            t.transform.GetChild(0).GetComponent<Renderer>().material.mainTexture = skins[Random.Range(0,skins.Count)];
            //yield return new WaitForSeconds(0.1f);
            countOfNPCs += 1;
        }
    }
}
