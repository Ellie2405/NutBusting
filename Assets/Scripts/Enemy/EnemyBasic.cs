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


    //might change later so that on start projectile get a tag of their damage, or maybe a rotation?

    private void Start()
    {
        FaceMiddle();
        hp = EnemyValues.maxHP;
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
        Debug.Log("hp left" + hp.ToString());
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
            EnemyManager.enemiesAlive--;
            Destroy(gameObject);
        }
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
            Attack(rayCastHit.transform.GetComponent<TurretAbstract>());
        }
    }

    void Walk()
    {
        transform.Translate(Vector3.forward * EnemyValues.movementSpeed * Time.deltaTime);
    }

    void Attack(TurretAbstract target)
    {
        inRangeOfTarget = target.TakeDamage(EnemyValues.damage, this);
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
            yield return new WaitForSeconds(1);
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
