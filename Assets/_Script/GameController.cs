using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] static int width = 8; 
    [SerializeField] static int height = 8;
    [SerializeField]  GameObject[] Blocks;
    [SerializeField] GameObject UIPanelEndgame;
    [SerializeField] GameObject UISocre;
    [SerializeField] public static Vector3 position1;
    [SerializeField] public static Vector3 position2;
    [SerializeField] public static Vector3 position3;
    bool [] widthComplate = new bool[8];
    bool[] heightComplate = new bool[8];
    
   
    public int spointScore;
    
    public int countBlockInGame;
    public static bool[] EndGame = new bool[3];
    public Transform[,] gird = new Transform[width,height];
    BlockMove blockmove;




    private void Awake()
    {
        blockmove = FindObjectOfType<BlockMove>();
        LoadUIPanelEndgame();
    }
    void Start()
    {
        
        countBlockInGame = 3;
        
        // position of block spawn
        position1 = new Vector3(11f, 7f, 0);
        position2 = new Vector3(11f, 4, 0);
        position3 = new Vector3(11f, 1f, 0);
       

        SpawnBlock(position1);
        SpawnBlock(position2);
        SpawnBlock(position3);
        EndGame[0] = false;
        EndGame[1] = false;
        EndGame[2] = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(countBlockInGame == 0 )
        {
            SpawnBlock(position1);
            SpawnBlock(position2);
            SpawnBlock(position3);      
            countBlockInGame = 3;
            EndGame[0] = false;
            EndGame[1] = false;
            EndGame[2] = false;
            b();
        }
    }
    private void LoadUIPanelEndgame()
    {
        if (UIPanelEndgame == null) Debug.LogWarning("UIPanelEndgame of scrpit UI_menager NULL");
        if (UISocre == null) Debug.LogWarning("UISocre of scrpit UI_menager NULL");
    }

    public bool a()
    {
        int count = 0;
        for(int i= 2; i>=0; i--)
        {
            if ((EndGame[i]))
            {
                count++;
            }
        }
        if (count == countBlockInGame && countBlockInGame!=0) return true;
        return false;
    }
    public void b()
    {
        if (a() == true)
        {  
            Debug.Log("end gamem sau moiw");
            Invoke("ShowPanelEndgame", 3f);
        }
    }
    private void ShowPanelEndgame()
    {
        UIPanelEndgame.gameObject.SetActive(true);
        UISocre.gameObject.SetActive(false);
    }
    public void StartGame()
    {
        Debug.Log("da bawts dau game");
        //SceneManager.LoadScene(0);
        SceneManager.LoadScene("StartScene");
        //
    }

    public void SpawnBlock(Vector3 positionGameob)
    {
        int tempBlock = Random.Range(0, Blocks.Length);
        int tempRotate = Random.Range(0, 50);
        transform.rotation = Quaternion.AngleAxis(tempRotate * 90, Vector3.forward);
        Instantiate(Blocks[tempBlock], positionGameob, transform.rotation);
    }

    public void CheckBlockComplate()
    { 
        // mac dinh ban dau, cac dong va cot deu la hoan thanh
        for(int i=0; i<8; i++) // duyet tung dong : gia tri i==y
        {
            widthComplate[i] = true;
            for (int j = 0; j < 8; j++) //tung phan tu tren dong j==x
            {
                if (i == 0)
                {
                    heightComplate[j] = true; 
                }  
                if (gird[j,i] == null)
                {
                    widthComplate[i] = false;
                    heightComplate[j] = false;
                }

            }
        }
    }

    // clear block

    public void ClearBlockComplate()
    {
        int countpointline = 0;
        int countpointCol = 0;
        for (int i = 0; i < 8; i++)
        {
            if (widthComplate[i] == true)
            {
                ClearLineBlockComplate(i);
                countpointline++;
                spointScore += 1;
            }
            if (heightComplate[i] == true)
            {
                ClearColBlockComplate(i);
                countpointCol++;
                spointScore += 1;
            }

        }
        if(countpointline == 2 || countpointCol == 2)
        {
            spointScore += 1;
        }
        else if(countpointline == 3 || countpointCol == 3)
        {
            spointScore += 2;
        }
        countpointline = 0;
        countpointCol = 0;
    }

    private void ClearLineBlockComplate(int line)
    {

        for( int i=0; i < 8; i++)
        {
            if(gird[i, line] != null)
            {
                Destroy(gird[i, line].gameObject, 0.2f);
                //DestroyObject(gird[i, line].gameObject);
            }           
        }
    }

    private void ClearColBlockComplate(int col)
    {

        for (int i = 0; i < 8; i++)
        {   if(gird[col, i] != null)
            {
                Destroy(gird[col, i].gameObject, 0.2f);
                //DestroyObject(gird[i, line].gameObject);
            }
        }
    }


}
