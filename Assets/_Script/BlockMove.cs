using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor;
using UnityEngine;

public class BlockMove : MonoBehaviour
{

    [SerializeField] Vector2 difference;
    [SerializeField] Vector3 mousePos;
    Vector3 positionBlockStart;
    int SttpositionBlockStart;
    public bool movable = true;
    bool Blockmoving =false; // xac dinh block co dang di chuyen khong, co dang o vi tri ban dau ko
    public bool EndGame = false;
    public GameObject Block;
    private GameController controller;
    private SetlayerBlock setlayerBlock;
    
    

    public Vector3[] transformBlock = new Vector3[10];
    private void Awake()
    {
        controller = FindObjectOfType<GameController>();
        setlayerBlock = FindObjectOfType<SetlayerBlock>();


    }
    // Start is called before the first frame update
    void Start()
    {
        setlayerBlock.OnSetLayerSprite();
        positionBlockStart = transform.position;// lay vi tri start cua block
        if (positionBlockStart == GameController.position1)
        {
            SttpositionBlockStart = 0;
        }
        if (positionBlockStart == GameController.position2)
        {
            SttpositionBlockStart = 1;
        }
        if (positionBlockStart == GameController.position3)
        {
            SttpositionBlockStart = 2;
        }

    }

    // Update is called once per frame
    //Fixed
    private void FixedUpdate()
    {
        //GetTransform();
        if (movable && Blockmoving == false)
        {
            if (checkEndGame())
            {
                GameController.EndGame[SttpositionBlockStart] = true;
                return;
            }
        }
    }
    // function get posotion mouse up screen
    // functioj of MONOBIHAVIOUR
    private void GetMousePos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        if (movable)
        {
            GetMousePos();
            difference = (Vector2)(mousePos - transform.position);
            Blockmoving = true;
        }
        
    }

    private void OnMouseDrag()
    {
        
        
        if (movable)
        {
            GetMousePos();
            transform.position = (Vector2)mousePos - difference;
            transform.localScale = new Vector3(2f, 2f, 1);
            
        }      
    }
    private void OnMouseUp()
    {
        if (movable)
        {
            if (!CheckTransform())
            {
                transform.position = positionBlockStart;
                transform.localScale = new Vector3(1f, 1f, 1);
                Blockmoving = false;
            }
            else // vao dung cho
            {
                // lam cho block tu khit dung voi duong
                transform.position = new Vector3((int)transform.position.x, (int)transform.position.y, 0)
                    + new Vector3(0.5f, 0.5f, 0);
                // them block vao trong cac o vuong tren ban
                AddBlockTrans();
                setlayerBlock.OffSetLayerSprite();
                movable = false;
                controller.CheckBlockComplate();
                controller.ClearBlockComplate();
               
                controller.countBlockInGame--;
                GameController.EndGame[SttpositionBlockStart] = false;
                controller.b();
                //Invoke("cac", 2f);
                
            }

        }
    }
    void cac()
    {
        controller.b();
    }
    // Function of user

    public void GetTransform()
    {
        
    }

  bool checkEndGame()
    {
        bool endgame = false;// ko end game
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                int count = 0;
                int hangngang = 0;
                int hangdoc = 0;
                Vector3 tempBlock = new Vector3(0, 0, 0);
                foreach (Transform subBlock in Block.transform)
                {

                    if (count == 0)
                    {
                        tempBlock = subBlock.position;// lay vi tri dau tien cua block
                        transformBlock[count] = subBlock.position - tempBlock;
                    }
                    else
                    {
                        transformBlock[count] = (subBlock.position - tempBlock) * 2;
                    }
                    hangngang = j+(int)transformBlock[count].x;
                    hangdoc = i+(int)transformBlock[count].y;

                    // check cac phan tu thoa man nam trong ban co
                    if (hangngang < 8 && hangngang >= 0 && hangdoc < 8 && hangdoc >= 0)
                    {
                        //checked vi tri tai day co null ko
                        if (controller.gird[hangngang, hangdoc] != null)
                        {
                            endgame = true;
                            break;// neu co 1 block vi pham , break khoi foreach
                        }else
                        {
                            endgame = false;
                        }
                    }
                    else
                    {
                        endgame = true;
                        break;
                    }
                    count++;
                } // end froeach
                if (endgame == false) 
                {
                    
                    return false;
                }
            }
        }// end duyet luoi
       
        return true;
    }

    public bool CheckTransform()
    {
        
        // check gameobject have out zone gameplay
        foreach (Transform subBlock in Block.transform)
        {
            if ((subBlock.transform.position.x >= 7.8f || subBlock.transform.position.x <= 0.3f) ||
            (subBlock.transform.position.y >= 7.8f || subBlock.transform.position.y <= 0.3f))
            {
                return false; // out gameplay
            }
            else if (controller.gird[(int)subBlock.position.x, (int)subBlock.position.y] != null)
            {
                return false;
            }
        }
            return true;// in gameplay
    }

    private void AddBlockTrans()
    {
        foreach (Transform subBlock in Block.transform)
        {
            controller.gird[(int)subBlock.position.x, (int)subBlock.position.y] = subBlock;
        }
    }


}
