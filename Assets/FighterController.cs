using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FindTargetState: State
{
    Vector3 attackPos;
    public override void Enter()
    {
        Base[] bases = GameObject.FindObjectsOfType<Base>();
        Base myBase = owner.GetComponent<FighterController>().myBase;
        Base target = myBase;
        while (target == myBase)
        {
            target = bases[Random.Range(0, bases.Length)];            
        }
        Vector3 toOwner = owner.transform.position - target.transform.position;
        toOwner.Normalize();
        attackPos = target.transform.position + toOwner * (owner.GetComponent<FighterController>().targetDistance);
        owner.GetComponent<Arrive>().targetPosition = attackPos;
        owner.GetComponent<Arrive>().enabled = true;
        owner.GetComponent<FighterController>().targetBase = target;
    }
    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = false;
        //owner.GetComponent<Boid>().velocity = Vector3.zero;
    }

    public override void Think()
    {
        if (Vector3.Distance(owner.transform.position, attackPos) < 2.0f)
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
            Vector3 bulletSpawn = owner.transform.position + owner.transform.forward;
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
        Arrive a = owner.GetComponent<Arrive>();
        a.targetPosition = owner.GetComponent<FighterController>().myBase.transform.position;
        a.enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(owner.transform.position, owner.GetComponent<FighterController>().myBase.transform.position) < 2)
        {
            owner.GetComponent<StateMachine>().ChangeState(new RefuelState());
        }
    }
    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = false;
    }
}

public class RefuelState : State
{
    public override void Think()
    {
        if (owner.GetComponent<FighterController>().myBase.tiberium >= 7)
        {
            owner.GetComponent<StateMachine>().ChangeState(new FindTargetState());
            owner.GetComponent<FighterController>().tiberium += 7;
            owner.GetComponent<FighterController>().myBase.GetComponent<Base>().tiberium -= 7;
        }
    }
}

public class FighterController : MonoBehaviour
{
    public Base myBase;
    public Base targetBase;
    public float targetDistance = 2;
    public GameObject bulletPrefab;
    public float tiberium = 10;
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
