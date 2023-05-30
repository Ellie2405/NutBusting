using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBasic : MonoBehaviour
{
    [SerializeField] EnemyMeleeScriptableObject EnemyValues;
    int hp;


    private void Start()
    {
        FaceMiddle();
        hp = EnemyValues.maxHP;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * EnemyValues.movementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_PROJECTILE_TAG))
        {
            TakeDamage();
            Debug.Log(other.gameObject.ToString());
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
            Inventory.Instance.ObtainCurrency(8);
            Destroy(gameObject);
        }
    }

    void FaceMiddle()
    {
        transform.LookAt(new Vector3(0, transform.position.y, 0));
    }
}
