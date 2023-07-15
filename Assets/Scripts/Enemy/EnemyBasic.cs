using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBasic : MonoBehaviour
{
    [SerializeField] EnemyMeleeScriptableObject EnemyValues;
    int hp;
    bool inRangeOfTarget = false;
    RingFloor parentFloor = null;
    float GCD;


    //might change later so that on start projectile get a tag of their damage, or maybe a rotation?

    private void Start()
    {
        FaceMiddle();
        hp = EnemyValues.maxHP;
        GCD = EnemyValues.scanCD;
        StartCoroutine(CheckLoop());
        StartCoroutine(FloorLoop());
    }

    private void Update()
    {
        if (!inRangeOfTarget)
            Walk();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_PROJECTILE_TAG))
        {
            TakeDamage(Mathf.RoundToInt(other.transform.eulerAngles.z));
            //Debug.Log(other.transform.eulerAngles.z.ToString());
        }

    }

    public void TakeDamage(int i)
    {
        hp -= i;
        CheckHP();
    }

    public void TakeDamage()
    {
        hp -= 1;
        CheckHP();
    }

    void CheckHP()
    {
        if (hp < 1)
        {
            Inventory.Instance.ObtainCurrency(EnemyValues.currencyDrop);
            Die();
        }
    }

    public void Die()
    {
        EnemyManager.enemiesAlive--;
        Destroy(gameObject);
    }

    void CheckRing()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit rayCastHit, 2, EnemyValues.floorLayerMask))
        {
            parentFloor = rayCastHit.transform.GetComponent<RingFloor>();
            transform.SetParent(parentFloor.transform);
        }
    }

    void CheckForPlayer()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit rayCastHit, 1, EnemyValues.playerObjectLayerMask))
        {
            GCD = EnemyValues.attackCD;
            Attack(rayCastHit.transform.GetComponent<TurretAbstract>());
        }
    }

    void Walk()
    {
        transform.Translate(EnemyValues.movementSpeed * Time.deltaTime * Vector3.forward);
    }

    void Attack(TurretAbstract target)
    {
        inRangeOfTarget = target.TakeDamage(EnemyValues.damage, this);
        if (!inRangeOfTarget) GCD = EnemyValues.scanCD;
        //animation
    }

    void FaceMiddle()
    {
        transform.LookAt(new Vector3(0, transform.position.y, 0));
    }

    IEnumerator CheckLoop()
    {
        while (true)
        {
            CheckForPlayer();
            yield return new WaitForSeconds(GCD);
        }
    }
    IEnumerator FloorLoop()
    {
        while (true)
        {
            CheckRing();
            yield return new WaitForSeconds(1);
        }
    }
}
