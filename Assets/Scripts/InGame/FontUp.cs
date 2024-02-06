using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FontUp : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(Suicide());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Suicide()
    {
        yield return new WaitForSeconds(0.6f);

        gameObject.SetActive(false);
    }
}
