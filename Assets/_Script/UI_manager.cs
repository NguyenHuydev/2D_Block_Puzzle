using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_menager : MonoBehaviour
{
    // Start is called before the first frame update
    GameController controller;
    [SerializeField] Text textScore;
    [SerializeField] Text textScorePanelEndgame;


    private void Awake()
    {
        controller = GetComponent<GameController>();
        
    }

    void Start()
    {
        controller.spointScore = 0;
        textScore.text = "" + controller.spointScore * 100;
        
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = "" + controller.spointScore * 100;
        textScorePanelEndgame.text = "" + controller.spointScore * 100;
    }
}
