using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TProjectileBasic : MonoBehaviour
{
    [SerializeField] TPBasicScriptableObject TurretValues;
    readonly Vector3 AimDirection = new(0, -0.1f, 1);

    void Start()
    {
        //rotate the pojectile to send its damage
        transform.Rotate(0, 0, TurretValues.damage);
        StartCoroutine(Timeout());
    }

    void Update()
    {
        transform.Translate(Time.deltaTime * TurretValues.projectileSpeed * AimDirection);
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
