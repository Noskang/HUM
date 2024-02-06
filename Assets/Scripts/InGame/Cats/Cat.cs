using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using DG.Tweening;

public class Cat : MonoBehaviour
{
    public Transform target;
    public float range = 17f;
    public int attackpoint = 5;
    private int atk;
    private float shoottimer = 0f;
    public float cooltime = 2.5f;
    private SpriteRenderer mySR;
    public string enemyTag = "Enemy";
    public string bossTag = "Boss";
    public GameObject bullet;
    public GameObject aim;
    public TextMeshPro levelText;
    public int currentLevel;
    public float bulletspeed = 200f;
    public Transform ShootPoint;

    private Animator myAnim;

    // List of bullet pools
    public List<List<GameObject>> bullets = new List<List<GameObject>>();

    private void Start()
    {
        atk = attackpoint;
        myAnim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        UpdateLevel();
        InvokeRepeating("UpdateTraget", 0f, 0.02f);

        // Initialize the bullet pools
        for (int i = 0; i < 10; i++) // Change 10 to the desired size of each pool
        {
            bullets.Add(new List<GameObject>());
        }
    }

    public void UpdateLevel()
    {
        levelText.text = currentLevel.ToString(); 
    }

    public void Levelup()
    {
        //currentLevel++;
        levelText.text = currentLevel.ToString();
        atk = attackpoint * currentLevel;
    }

    // Update is called once per frame
    void Update()
    {
        shoottimer -= Time.deltaTime;

        if (target && shoottimer <= 0)
        {
            ShooTraget();
            shoottimer = cooltime;
        }

    }

    void UpdateTraget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortesDistance = Mathf.Infinity;
        GameObject neareatEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortesDistance)
            {
                shortesDistance = distanceToEnemy;
                neareatEnemy = enemy;
            }
        }

        if (neareatEnemy != null && shortesDistance <= range)
        {
            target = neareatEnemy.transform;
            aim.SetActive(true);
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            aim.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            target = null;
            aim.SetActive(false);
        }
    }

    private void ShooTraget()
    {
        if (target != null)
        {
            myAnim.SetTrigger("attack");
            MoveAndDisappear();
        }
    }

    private void MoveAndDisappear()
    {
        StartCoroutine(MoveToTargetPosition());
    }

    private IEnumerator MoveToTargetPosition()
    {
        GameObject newBullet = Get(); // Get bullet from pool
        GameManager.Instance.AudioManager.PlaySfx(AudioManager.Sfx.attckSound);
        newBullet.transform.position = ShootPoint.position;
        newBullet.transform.rotation = ShootPoint.rotation;
        Vector3 initialPosition = newBullet.transform.position;
        Transform currentTarget = target;
        float elapsedTime = 0f;

        while (elapsedTime < 0.5f)
        {
            newBullet.transform.position = Vector3.Lerp(initialPosition, currentTarget.position, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        newBullet.gameObject.SetActive(false);
        if (currentTarget != null)
        {
            GameObject DamageText = GameManager.Instance.DamageTextManager.Get(0);
            DamageText.transform.position = currentTarget.transform.position;
            DamageText.transform.rotation = currentTarget.transform.rotation;
            DamageText.GetComponent<TextMeshPro>().text = atk.ToString();
            GameManager.Instance.AudioManager.PlaySfx(AudioManager.Sfx.attakingSound);
            currentTarget.GetComponent<Enemy>().currentHP = currentTarget.GetComponent<Enemy>().currentHP - atk;
        }
    }

    private void Attack()
    {
        
    }

    public GameObject Get()
    {
        GameObject select = null;

        // Get bullet from the appropriate pool
        int poolIndex = Random.Range(0, bullets.Count);
        foreach (GameObject item in bullets[poolIndex])
        {
            if (item && !item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            select = Instantiate(bullet, transform);
            bullets[poolIndex].Add(select);
        }

        return select;
    }
}
