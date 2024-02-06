using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float MaxHP;
    public float currentHP;
    public EnemyMove enemyMove;
    public Slider hpslider;
    // Start is called before the first frame update
    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        enemyMove.spline2D = GameManager.Instance.levelRoad;
        currentHP = MaxHP;
    }

    private void OnEnable()
    {
        //enemyMove.spline2D = GameManager.Instance.levelRoad;
        currentHP = MaxHP;
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();

        // 첫 번째 요소는 자신이므로 건너뜁니다.
        for (int i = 1; i < children.Length; i++)
        {
            // 자식 오브젝트를 비활성화합니다.
            children[i].gameObject.SetActive(true);
        }
        //enemyMove.GetStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHP <= 0)
        {
            Dead();
        }
    }

    private void LateUpdate()
    {
        hpslider.value = currentHP / MaxHP;
    }

    void Dead()
    {
        GameManager.Instance.currentmonster = GameManager.Instance.currentmonster - 1;
        gameObject.SetActive(false);
    }
}
