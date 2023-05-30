using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TProjectileBasic : MonoBehaviour
{
    [SerializeField] TPBasicScriptableObject TurretValues;

    void Start()
    {
        StartCoroutine(Timeout());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * TurretValues.projectileSpeed);
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
