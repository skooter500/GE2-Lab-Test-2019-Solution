using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    public float tiberium = 0;

    public TextMeshPro text;

    public GameObject fighterPrefab;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            tiberium-= 0.5f;
            Destroy(other.gameObject);
        }
    }

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
            if (tiberium >= 10)
            {
                GameObject fighter = GameObject.Instantiate<GameObject>(fighterPrefab, transform.position, Quaternion.identity);
                fighter.GetComponent<FighterController>().myBase = this;
                foreach(Renderer r in fighter.GetComponentsInChildren<Renderer>())
                {
                    r.material.color = GetComponent<Renderer>().material.color;
                }
                tiberium -= 10;
                fighter.transform.parent = this.transform;
            }
            yield return new WaitForSeconds(1.0f);
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
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + tiberium;
    }
}
