using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    [SerializeField] Camera playerCamera = null;
    [SerializeField] string wallyTag = "wally";
    [SerializeField] Material defaultMaterial;
    [SerializeField] Text scoreWallyText;
    [SerializeField] Text scoreExtraText;
    [SerializeField] GameObject endGamePopUp;

    // for the pop ups when you finish the game
    /*
    [SerializeField] TMP_Text popupText;
    [SerializeField] GameObject popUpBox;
    [SerializeField] Animator animator;
    */

    HashSet<string> extrasFound = new HashSet<string>();

    private Transform _selection;
    public bool wallyFound = false;


    // bool for the endgame popUp
    public bool endGame = false;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (_selection != null) {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }

        var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0)) {
            var selection = hit.transform;
            if (selection.CompareTag(wallyTag)) {
                var selectionRenderer = selection.GetComponent<Renderer>();
     
                // If wally was not found yet and we want to press it
                if (!wallyFound) {
                    wallyFound = true;
                    scoreWallyText.text = "Ache o Wally (1/1)";
                    scoreWallyText.color = Color.gray;
                } 
                
                // if (selectionRenderer != null) { 
                //     selectionRenderer.material = highlightMaterial;
                // }
                // _selection = selection;
            }


            // Extra 1
            if (selection.CompareTag("extra1")) {
                extrasFound.Add("extra1");
            }
            if (selection.CompareTag("extra2")) {
                extrasFound.Add("extra2");
            }
            if (selection.CompareTag("extra3")) {
                extrasFound.Add("extra3");
            }

            string finalExtraScore = "Ache os cachorros (" + extrasFound.Count.ToString() + "/3)";
            scoreExtraText.text = finalExtraScore;
            if (extrasFound.Count == 3) {
                    scoreExtraText.color = Color.gray;
            } 


        }

        if (Input.GetMouseButtonDown(1)) {
            Restart();
        }

        // must find all the characters and endgame should be false
        if(extrasFound.Count == 3 && wallyFound && !endGame){
            Activate();
        }
        
        if (endGame)
        {
            if (Input.GetKeyDown("r"))
            {
                Restart();
            }

            if (Input.GetKeyDown("q"))
            {
               Application.Quit();
            }

        }

    }

    // Put this somwhere else
    void Restart() {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
    }

    // should activate - suzuki corno
    void Activate(){

        endGamePopUp.SetActive(true);

        endGame = true;

    }
}
