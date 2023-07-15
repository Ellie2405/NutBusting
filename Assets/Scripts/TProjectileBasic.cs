using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TProjectileBasic : MonoBehaviour
{
    [SerializeField] TPBasicScriptableObject TurretValues;

    void Start()
    {
        //rotate the pojectile to send its damage
        transform.Rotate(0, 0, TurretValues.damage);
        StartCoroutine(Timeout());
    }

    void Update()
    {
        transform.Translate(Time.deltaTime * TurretValues.projectileSpeed * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        //vfx
        Destroy(gameObject);
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
