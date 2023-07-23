using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyBasic : MonoBehaviour
{
    public static event Action OnEnemyDeath;

    [SerializeField] EnemyMeleeScriptableObject EnemyValues;
    bool isAlive = true;
    int hp;
    bool inRangeOfTarget = false;
    RingFloor parentFloor = null;
    float GCD;
    float dissolve = 1;

    [SerializeField] Transform feet;
    [SerializeField] Animator animator;
    [SerializeField] Collider c;
    [SerializeField] GameObject lights;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Renderer[] renderers = new Renderer[0];


    //might change later so that on start projectile get a tag of their damage, or maybe a rotation?

    private void Start()
    {
        FaceMiddle();
        hp = EnemyValues.maxHP;
        GCD = EnemyValues.scanCD;
        StartCoroutine(DissolveIn());
        StartCoroutine(CheckLoop());
        StartCoroutine(FloorLoop());
        EnemyValues.SpawnSound.Play(audioSource);
    }

    private void Update()
    {
        if (isAlive && !inRangeOfTarget)
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
        Instantiate(EnemyValues.HitVFX, transform.position + (Vector3.up * 0.5f), Quaternion.identity);
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
        isAlive = false;
        OnEnemyDeath.Invoke();
        c.enabled = false;
        lights.SetActive(false);
        EnemyValues.DeathSound.Play(audioSource);
        StartCoroutine(DissolveOut());
        Destroy(gameObject, 2);
    }

    void CheckRing()
    {
        if (Physics.Raycast(feet.position, Vector3.down, out RaycastHit rayCastHit, 2, EnemyValues.floorLayerMask))
        {
            RingFloor floor = rayCastHit.transform.GetComponent<RingFloor>();
            if (!ReferenceEquals(floor, parentFloor))
            {
                parentFloor = floor;
                transform.SetParent(parentFloor.transform);
                StartCoroutine(Climb(transform.position.y));
            }
        }
    }

    void CheckForPlayer()
    {
        if (isAlive)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit rayCastHit, 1, EnemyValues.playerObjectLayerMask))
            {
                inRangeOfTarget = true;
                GCD = EnemyValues.attackCD;
                StartCoroutine(AttackCo(rayCastHit.transform.GetComponent<TurretAbstract>()));
            }
            else
            {
                inRangeOfTarget = false;
                GCD = EnemyValues.scanCD;
            }
        }
    }

    void Walk()
    {
        transform.Translate(EnemyValues.movementSpeed * Time.deltaTime * Vector3.forward);
    }

    void FaceMiddle()
    {
        transform.LookAt(new Vector3(0, transform.position.y, 0));
    }

    IEnumerator DissolveIn()
    {
        while (dissolve > 0)
        {
            dissolve -= 0.01f;
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.SetFloat("_DissolveAmount", dissolve);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator DissolveOut()
    {
        while (dissolve < 1)
        {
            dissolve += 0.01f;
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.SetFloat("_DissolveAmount", dissolve);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator CheckLoop()
    {
        while (true)
        {
            CheckForPlayer();
            yield return new WaitForSeconds(GCD);
        }
    }

    IEnumerator AttackCo(TurretAbstract target)
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(EnemyValues.attackWindUp);
        EnemyValues.AttackSound.Play(audioSource);
        target.TakeDamage(EnemyValues.damage, this);
    }

    IEnumerator FloorLoop()
    {
        while (true)
        {
            CheckRing();
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator Climb(float startY)
    {
        while (transform.position.y < startY + 0.15)
        {
            transform.Translate(Vector3.up * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    void PlaySound()
    {
    }
}
