using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FindTargetState: State
{
    Vector3 attackPos;
    public override void Enter()
    {
        Base[] bases = GameObject.FindObjectsOfType<Base>();
        Base myBase = owner.GetComponent<Base>();
        Base target = myBase;
        while (target == myBase)
        {
            target = bases[Random.Range(0, bases.Length)];            
        }
        Vector3 toTarget = owner.transform.position - target.transform.position;
        toTarget.Normalize();
        attackPos = target.transform.position + toTarget * (owner.GetComponent<FighterController>().targetDistance);
        owner.GetComponent<Arrive>().targetPosition = attackPos;
        owner.GetComponent<Arrive>().enabled = true;
        owner.GetComponent<FighterController>().targetBase = target;
    }
    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = false;
    }

    public override void Think()
    {
        if (Vector3.Distance(owner.transform.position, attackPos) < 1.0f)
        {
            owner.GetComponent<StateMachine>().ChangeState(new AttackingState());
        }
    }
}

public class AttackingState : State
{
    FighterController fc;
    public override void Enter()
    {
        fc = owner.GetComponent<FighterController>();
    }
    public override void Think()
    {
        if (fc.tiberium > 0)
        {
            Vector3 bulletSpawn = owner.transform.position + owner.transform.forward * 2;
            GameObject b = GameObject.Instantiate<GameObject>(fc.bulletPrefab, bulletSpawn, owner.transform.rotation);

            foreach (Renderer r in b.GetComponentsInChildren<Renderer>())
            {
                r.material.color = owner.GetComponent<Renderer>().material.color;
            }

            fc.tiberium--;
        }
        else
        {
            owner.GetComponent<StateMachine>().ChangeState(new ReturnToBaseState());
        }
    }
}

public class ReturnToBaseState : State
{
    public override void Enter()
    {
        Seek s = owner.GetComponent<Seek>();
        s.target = owner.GetComponent<FighterController>().myBase.transform.position;
        s.enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(owner.transform.position, owner.GetComponent<FighterController>().myBase.transform.position) < 0.5f)
        {
            owner.GetComponent<StateMachine>().ChangeState(new FindTargetState());
            owner.GetComponent<FighterController>().tiberium += 5;
            owner.GetComponent<FighterController>().myBase.GetComponent<Base>().tiberium -= 5;
        }
    }
    public override void Exit()
    {
        owner.GetComponent<Seek>().enabled = false;
    }
}

public class FighterController : MonoBehaviour
{
    public Base myBase;
    public Base targetBase;
    public float targetDistance = 2;
    public GameObject bulletPrefab;
    public int tiberium = 10;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new FindTargetState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
