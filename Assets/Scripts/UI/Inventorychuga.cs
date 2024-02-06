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
                Debug.Log("��á���");
                completed = true;
            }
        }
        
    }

    void GetAllChildObjects()
    {
        // �ڽ� ������Ʈ�� ���� ������
        int childCount = inventory.childCount;

        // �ڽ� ������Ʈ���� ������ �迭�� ũ�� ����
        buttons = new GameObject[childCount];

        // ��� �ڽ� ������Ʈ���� �迭�� �߰�
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = inventory.GetChild(i).gameObject;
        }

        Debug.Log(buttons.Length);
    }
}
