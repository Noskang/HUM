using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CurrentReadyCat : MonoBehaviour
{
    public enum CurrentState
    {
        notReady , buttonReady, summonReady
    }

    public CurrentState state;
    public CatandEnemy currentCat;
    public int currentCatLevel;
    public GameObject currentCatspawnPoint;
    private SpriteRenderer mySR;
    private Transform myTR;
    private Rigidbody2D myRD;
    private Vector3 M_screen_Position = Vector3.zero;
    private Vector3 M_world_Position = Vector3.zero;
    //public TextMeshProUGUI stateMessage;

    public GameObject start;
    public GameObject end;

    public TextMeshPro levelText;
    // Start is called before the first frame update
    void Start()
    {
        myTR = GetComponent<Transform>();
        myRD = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        M_screen_Position = Input.mousePosition;
        M_world_Position = Camera.main.ScreenToWorldPoint(M_screen_Position);
        M_world_Position.z = 0f;

        transform.position = M_world_Position;

        /*if(state == CurrentState.notReady)
        {
            stateMessage.text = "Notready";
        }
        else if (state == CurrentState.buttonReady)
        {
            stateMessage.text = "ButtonReady";
        }
        else if (state == CurrentState.summonReady)
        {
            stateMessage.text = "SummonReady";
        }*/

        //if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        if(Input.GetMouseButtonUp(0))
        {
            if(state == CurrentState.notReady )
            {
                if(start.GetComponent<CatSummonButton>() != null)
                {
                    start.GetComponent<CatSummonButton>().cat = currentCat;
                    start.GetComponent<CatSummonButton>().catLevel = currentCatLevel;
                    start.GetComponent<CatSummonButton>().ButtonChange();
                }
                else if(start.GetComponent<CatCummonPoint>() != null)
                {
                    start.GetComponent<CatCummonPoint>().cat = currentCat;
                    start.GetComponent<CatCummonPoint>().catLevel = currentCatLevel;
                    start.GetComponent<CatCummonPoint>().Summon();
                }
                //Debug.Log("그냥 땠어용");
                //stateMessage.text = "Notouched";
            }
            else if (state == CurrentState.summonReady)
            {
                
                if (!end.GetComponent<CatCummonPoint>().Currentcat)
                {
                    end.GetComponent<CatCummonPoint>().cat = currentCat;
                    end.GetComponent<CatCummonPoint>().catLevel = currentCatLevel;
                    end.GetComponent<CatCummonPoint>().Summon();
                }
                else if (currentCat == end.GetComponent<CatCummonPoint>().cat && currentCatLevel == end.GetComponent<CatCummonPoint>().catLevel)
                {
                    end.GetComponent<CatCummonPoint>().Levelup();
                }
                else if (end.GetComponent<CatCummonPoint>().cat != null)
                {

                    if (start.GetComponent<CatSummonButton>() != null)
                    {
                        start.GetComponent<CatSummonButton>().cat = end.GetComponent<CatCummonPoint>().cat;
                        start.GetComponent<CatSummonButton>().catLevel = end.GetComponent<CatCummonPoint>().catLevel;
                        start.GetComponent<CatSummonButton>().ButtonChange();
                    }
                    else if (start.GetComponent<CatCummonPoint>() != null)
                    {
                        start.GetComponent<CatCummonPoint>().cat = end.GetComponent<CatCummonPoint>().cat;
                        start.GetComponent<CatCummonPoint>().catLevel = end.GetComponent<CatCummonPoint>().catLevel;
                        start.GetComponent<CatCummonPoint>().Summon();
                    }

                    end.GetComponent<CatCummonPoint>().SummonClear();
                    end.GetComponent<CatCummonPoint>().cat = currentCat;
                    end.GetComponent<CatCummonPoint>().catLevel = currentCatLevel;
                    end.GetComponent<CatCummonPoint>().Summon();
                }
                else
                {
                    start.GetComponent<CatSummonButton>().cat = currentCat;
                    start.GetComponent<CatSummonButton>().catLevel = currentCatLevel;
                    start.GetComponent<CatSummonButton>().ButtonChange();
                }
                //Debug.Log("소환 가능할때 땠어용");
                //stateMessage.text = "CummonTouched";
            }
            else if (state == CurrentState.buttonReady)
            {
                if (!end.GetComponent<CatSummonButton>().cat)
                {
                    end.GetComponent<CatSummonButton>().cat = currentCat;
                    end.GetComponent<CatSummonButton>().catLevel = currentCatLevel;
                    end.GetComponent<CatSummonButton>().ButtonChange();
                }
                else if (currentCat == end.GetComponent<CatSummonButton>().cat && currentCatLevel == end.GetComponent<CatSummonButton>().catLevel)
                {
                    end.GetComponent<CatSummonButton>().Levelup();
                }
                else if (end.GetComponent<CatSummonButton>().cat != null)
                {
                    if (start.GetComponent<CatSummonButton>() != null)
                    {
                        start.GetComponent<CatSummonButton>().cat = end.GetComponent<CatSummonButton>().cat;
                        start.GetComponent<CatSummonButton>().catLevel = end.GetComponent<CatSummonButton>().catLevel;
                        start.GetComponent<CatSummonButton>().ButtonChange();
                    }
                    else if (start.GetComponent<CatCummonPoint>() != null)
                    {
                        start.GetComponent<CatCummonPoint>().cat = end.GetComponent<CatSummonButton>().cat;
                        start.GetComponent<CatCummonPoint>().catLevel = end.GetComponent<CatSummonButton>().catLevel;
                        start.GetComponent<CatCummonPoint>().Summon();
                    }

                    end.GetComponent<CatSummonButton>().cat = currentCat;
                    end.GetComponent<CatSummonButton>().catLevel = currentCatLevel;
                    end.GetComponent<CatSummonButton>().ButtonChange();
                }
                else
                {
                    start.GetComponent<CatSummonButton>().cat = currentCat;
                    start.GetComponent<CatSummonButton>().catLevel = currentCatLevel;
                    start.GetComponent<CatSummonButton>().ButtonChange();
                }
                //Debug.Log("버튼에서 땠어용");
                //stateMessage.text = "SummonTouched";
            }

            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    private void OnEnable()
    {
        
        if (start.GetComponent<CatSummonButton>() != null)
        {
            currentCat = start.GetComponent<CatSummonButton>().cat;
            currentCatLevel = start.GetComponent<CatSummonButton>().catLevel;
        }
        else if (start.GetComponent<CatCummonPoint>() != null)
        {
            currentCat = start.GetComponent<CatCummonPoint>().cat;
            currentCatLevel = start.GetComponent<CatCummonPoint>().catLevel;
        }
        StartCoroutine(Dragchange());
        levelText.text = currentCatLevel.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("CatSummon"))
        {
            Debug.Log("고양이에 닿았어용");
            state = CurrentState.summonReady;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CatSummon"))
        {
            Debug.Log("고양이한테서 떨어졌어용");
            state = CurrentState.notReady;
        }
    }

    public void ButtonOn(int i)
    {
        switch(i)
        {
            case 1:
                state = CurrentState.buttonReady;
                break;
            case 2:
                state = CurrentState.summonReady;
                break;
            default:
                break;
        }
        
    }

    public void ButtonOut()
    {
        state = CurrentState.notReady;
    }

    IEnumerator Dragchange()
    {
        yield return new WaitForSeconds(0.02f);
        mySR.sprite = currentCat.CatImage;
    }
}
