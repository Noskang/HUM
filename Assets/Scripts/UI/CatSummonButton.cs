using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class CatSummonButton : MonoBehaviour, IPointerEnterHandler
{
    private Image catImage;
    public Sprite defaultImage;
    public int catLevel = 0;
    public TextMeshProUGUI levelText;
    public CatandEnemy cat;
    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        catImage = GetComponent<Image>();
        defaultColor = catImage.color;
        ButtonClear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonChange()
    {
        catImage.sprite = cat.buttonImage;
        catImage.color = new Color(1, 1, 1, 1);
        if (catLevel <= 0)
        {
            catLevel = 1;
            levelText.text = "1";
        }
        else if (catLevel >= 1)
        {
            levelText.text = catLevel.ToString();
        }
    }

    public void Levelup()
    {
        catLevel++;
        levelText.text = catLevel.ToString();
    }


    public void ButtonClear()
    {
        catImage.sprite = defaultImage;
        catImage.color = defaultColor;
        levelText.text = string.Empty;
        
    }

    public void MouseOver()
    {
        StartCoroutine(mouseOver());
        //Debug.Log("고양이었습니당");
    }

    public IEnumerator mouseOver()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.catdragbutton.ButtonOn(1);
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
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseOver();
    }

    public void ButtonClick()
    {
        if (cat != null)
        {
            GameManager.Instance.catdragbutton.start = gameObject;
            GameManager.Instance.catdragbutton.gameObject.transform.position = transform.position;
            GameManager.Instance.catdragbutton.currentCat = cat;
            GameManager.Instance.catdragbutton.gameObject.SetActive(true);
            ButtonClear();
            cat = null;
            catLevel = 0;
        }
        
    }

}
