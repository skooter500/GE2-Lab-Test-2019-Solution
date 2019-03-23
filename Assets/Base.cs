using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public int tiberium = 0;

    public GameObject fighterPrefab;

    IEnumerator GainTiberium()
    {
        while (true)
        {
            tiberium++;
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator SpawnFighters()
    {
        while(true)
        {
            if (tiberium > 5)
            {
                GameObject fighter = GameObject.Instantiate<GameObject>(fighterPrefab);
                fighter.transform.position = this.transform.position;
                fighter.GetComponent<FighterController>().myBase = this;
                foreach(Renderer r in fighter.GetComponentsInChildren<Renderer>())
                {
                    r.material.color = GetComponent<Renderer>().material.color;
                }
                tiberium -= 5;
            }
            yield return new WaitForSeconds(2.0f);
        }
    }

    public void OnEnable()
    {
        StartCoroutine(GainTiberium());
        StartCoroutine(SpawnFighters());
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
