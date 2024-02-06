using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventorychuga : MonoBehaviour
{
    public CatandEnemy[] gettingCats;
    public GameObject[] buttons;
    public RectTransform inventory;
    // Start is called before the first frame update
    void Start()
    {
        GetAllChildObjects();
    }

    // Update is called once per frame
    
    public void Getcat()
    {
        int i = Random.Range(0, gettingCats.Length);
        bool completed = false;
        int count = 0;

        while(!completed)
        {
            if (buttons[count].GetComponent<CatSummonButton>().cat == null)
            {
                buttons[count].GetComponent<CatSummonButton>().cat = gettingCats[i];
                buttons[count].GetComponent<CatSummonButton>().ButtonChange();
                completed = true;
            }
            else if(count <= buttons.Length -2)
            {
                count++;
            }
            else
            {
                Debug.Log("다찼어용");
                completed = true;
            }
        }
        
    }

    void GetAllChildObjects()
    {
        // 자식 오브젝트의 수를 가져옴
        int childCount = inventory.childCount;

        // 자식 오브젝트들을 저장할 배열의 크기 설정
        buttons = new GameObject[childCount];

        // 모든 자식 오브젝트들을 배열에 추가
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = inventory.GetChild(i).gameObject;
        }

        Debug.Log(buttons.Length);
    }
}
