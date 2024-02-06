using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBase : MonoBehaviour
{
    private Rigidbody2D myRd;
    // Start is called before the first frame update
    void Start()
    {
        myRd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.Defeat();
            Debug.Log("∞‘¿” ∏¡«‘!");
        }
    }
}
