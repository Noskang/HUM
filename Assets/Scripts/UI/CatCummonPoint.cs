using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;
using UnityEngine.EventSystems;

public class CatCummonPoint : MonoBehaviour, IPointerEnterHandler
{
    public Sprite defaultImage;
    public SpriteRenderer mySR;
    public int catLevel;
    public TextMeshProUGUI levelText;
    public CatandEnemy cat;
    public GameObject Currentcat;
    private Color defaultColor;


    // Start is called before the first frame update
    void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
        defaultColor = mySR.color;
        SummonClear();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Summon()
    {
        mySR.color = new Color(0, 0, 0, 0);
        Currentcat = GameManager.Instance.CatPoolManager.Get(cat.id);
        Currentcat.transform.parent = transform;
        //Currentcat = Instantiate(cat.Cat, transform);
        Currentcat.GetComponent<Cat>().currentLevel = catLevel;
        Currentcat.transform.position = gameObject.transform.position;
        Currentcat.transform.rotation = gameObject.transform.rotation;
        Currentcat.transform.localScale = gameObject.transform.localScale;
        Currentcat.GetComponent<Cat>().UpdateLevel();
    }

    public void Levelup()
    {
        catLevel++;
        Currentcat.GetComponent<Cat>().currentLevel = catLevel;
        Currentcat.GetComponent<Cat>().Levelup();
    }

    public void SummonClear()
    {
        if(Currentcat)
        {
            Destroy(Currentcat);
            mySR.color = defaultColor;
            Currentcat = null;
            catLevel = 0;
        }
        mySR.color = defaultColor;
        
    }

    public void MouseOver()
    {
        StartCoroutine(mouseOver());
        //Debug.Log("고양이었습니당");
    }

    public IEnumerator mouseOver()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.catdragbutton.ButtonOn(2);
        GameManager.Instance.catdragbutton.end = gameObject;
        //Debug.Log("고양이었습니당");
    }

    public IEnumerator mouseout()
    {
        yield return new WaitForSeconds(0.01f);
        GameManager.Instance.catdragbutton.ButtonOut();
        GameManager.Instance.catdragbutton.end = null;
        //Debug.Log("고양이었습니당");
    }
    public void OffMouseOver()
    {
        StartCoroutine(mouseout());
        //GameManager.Instance.catdragbutton.ButtonOut();
        //GameManager.Instance.catdragbutton.end = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseOver();
    }

    public void ButtonClick()
    {
        if(cat != null)
        {
            GameManager.Instance.catdragbutton.start = gameObject;
            GameManager.Instance.catdragbutton.gameObject.transform.position = transform.position;
            GameManager.Instance.catdragbutton.currentCat = cat;
            GameManager.Instance.catdragbutton.gameObject.SetActive(true);
            SummonClear();
        }
    }
}
