using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public enum GameState { prepare, gaming}
    public static GameManager Instance;
    public GameState state = GameState.prepare;
    [Header("Managers")]
    public CurrentReadyCat catdragbutton;
    public GameObject catInventory;
    public PoolManager CatPoolManager;
    public PoolManager EnemyPoolManager;
    public PoolManager DamageTextManager;
    public EasySplinePath2D levelRoad;
    public RectTransform VictoryUI;
    public AudioManager AudioManager;
    public TextMeshProUGUI stateMessage;

    [Header("Rounds")]
    public int round = 1;
    public int maxround = 5;
    public int currentmonster;
    public int allmonster;
    public Round[] rounds;
    private Round currentround;

    [Header("Datas")]
    public CatandEnemy[] enemyData;


    // Start is called before the first frame update
    void Start()
    {
        
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        GameStart();
        AudioManager.Init();
        stateMessage.text = "";
        AudioManager.PlayBgm(AudioManager.Bgm.mainBGM);
    }

    private void Update()
    {
        if(state == GameState.gaming)
        {
            if(currentmonster <= 0)
            {
                Victory();
            }
        }

        if(round > maxround)
        {
            PerfectVictory();
        }
    }
    // Update is called once per frame

    public void GameStart()
    {
        StartCoroutine(StartingStart());
        
    }
    public void Victory()
    {
        stateMessage.text = "Victory!";
        round++;
        StartCoroutine(WaitingNextBatte());
    }

    public void Defeat()
    {
        stateMessage.text = "Defeat...";
        EnemyClear();
        StartCoroutine(WaitingNextBatte());
    }

    public IEnumerator roundStart()
    {
        allmonster = rounds[round - 1].monsters.Length;
        currentmonster = allmonster;
        yield return new WaitForSeconds(2f);
        stateMessage.text = $"Round {round}!";
        yield return new WaitForSeconds(1f);
        stateMessage.text = "";
        state = GameState.gaming;
        for (int i = 0; i < rounds[round - 1].monsters.Length; i++)
        {
            GameObject newMonster = GameManager.Instance.EnemyPoolManager.Get(rounds[round - 1].monsters[i]);

            yield return new WaitForSeconds(enemyData[rounds[round - 1].monsters[i]].respawntime);
        }
    }

    public IEnumerator WaitingNextBatte()
    {
        state = GameState.prepare;
        yield return new WaitForSeconds(1.2f);
        stateMessage.text = "";
        currentmonster = 0;
        yield return new WaitForSeconds(3.8f);

        StartCoroutine(roundStart());
    }

    void EnemyClear()
    {
        Transform[] children = EnemyPoolManager.gameObject.GetComponentsInChildren<Transform>();

        // 첫 번째 요소는 자신이므로 건너뜁니다.
        for (int i = 1; i < children.Length; i++)
        {
            // 자식 오브젝트를 비활성화합니다.

            Destroy(children[i].gameObject);
        }
    }

    public IEnumerator StartingStart()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(roundStart());
    }

    public void PerfectVictory()
    {
        stateMessage.text = "";
        VictoryUI.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Save(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
    public float Load(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetFloat(key);
        }
        return -1;
    }

}
