using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(Suicide());
    }

    // Update is called once per frame
    public IEnumerator Suicide()
    {
        yield return new WaitForSeconds(0.6f);

        gameObject.SetActive(false);
    }
}
