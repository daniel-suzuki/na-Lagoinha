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
    
    [SerializeField] GameObject endGamePopUp;
    [SerializeField] Text timer;
    [SerializeField] Text scoreWallyText;
    [SerializeField] Text scoreExtraText;

    [SerializeField] Text timer2;
    [SerializeField] Text scoreWallyText2;
    [SerializeField] Text scoreExtraText2;
    [SerializeField] Text record;


    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject mainMenu;

    // for the pop ups when you finish the game
    /*
    [SerializeField] TMP_Text popupText;
    [SerializeField] GameObject popUpBox;
    [SerializeField] Animator animator;
    */

    HashSet<string> extrasFound = new HashSet<string>();

    private Transform _selection;
    public bool wallyFound = false;
    public bool gameStarted = false;

    float time = 0f;

    // bool for the endgame popUp
    public bool endGame = false;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
                {
                Application.Quit();
                }
        if (!gameStarted) {
            if(Input.anyKey) {
                mainMenu.SetActive(false);
                gameStarted = true;
            }
        }
        else {
            if (_selection != null) {
                var selectionRenderer = _selection.GetComponent<Renderer>();
                selectionRenderer.material = defaultMaterial;
                _selection = null;
            }

            // Update Timer 

            
            if (!wallyFound) {
                time += Time.deltaTime;
                timer.text = string.Format("{0:0.00} s", time);
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
                        timer2.text = string.Format("{0:0.00} s", time);
                        float rec = -1f;
                        if(PlayerPrefs.HasKey("record")) {
                            rec = PlayerPrefs.GetFloat("record");
                        }

                        if (rec == -1f || time < rec) {
                            PlayerPrefs.SetFloat("record", time);
                            rec = time;
                        }
                        if (rec == time) {
                            record.text = string.Format("{0:0.00} s", rec) + " - Novo Recorde!";
                        } else
                        {
                            record.text = string.Format("{0:0.00} s", rec);
                        }
                        timer2.text = string.Format("{0:0.00} s", time);

                        // scoreWallyText.text = "Ache o Wally";
                        scoreWallyText.color = Color.gray;
                        scoreWallyText2.color = Color.gray;
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

                string finalExtraScore = "Ache os zumbis e o ciborgue (" + extrasFound.Count.ToString() + "/3)";
                scoreExtraText.text = finalExtraScore;
                scoreExtraText2.text = finalExtraScore;
                if (extrasFound.Count == 3) {
                        scoreExtraText.color = Color.gray;
                        scoreExtraText2.color = Color.gray;
                } 


            }

            // if (Input.GetMouseButtonDown(1)) {
            //     Restart();
            // }

            // must find all the characters and endgame should be false
            if(wallyFound && !endGame){
                Activate();
            }
            
            if (endGame)
            {
                if (Input.GetKeyDown("q"))
                {
                    Restart();
                }

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
        gamePanel.SetActive(false);

        endGame = true;

    }
}
