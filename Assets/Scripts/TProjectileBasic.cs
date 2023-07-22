using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TProjectileBasic : MonoBehaviour
{
    [SerializeField] TPBasicScriptableObject TurretValues;
    readonly Vector3 AimDirection = new(0, -0.1f, 1);
    float speed;

    void Start()
    {
        SetProjectileSpeed();
        //rotate the pojectile to send its damage
        transform.Rotate(0, 0, TurretValues.damage);
        StartCoroutine(Timeout());
    }

    void Update()
    {
        transform.Translate(Time.deltaTime * speed * AimDirection);
    }

    void SetProjectileSpeed()
    {
        speed = TurretValues.projectileSpeed + (EnemyManager.Instance.waveCounter - 1) * TurretValues.projectileSpeedIncrease;
        if (speed > TurretValues.projectileSpeedCap) speed = TurretValues.projectileSpeedCap;
    }

    private void OnTriggerEnter(Collider other)
    {
        //vfx
        Destroy(gameObject);
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
