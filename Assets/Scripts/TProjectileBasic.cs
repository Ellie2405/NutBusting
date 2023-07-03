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
        //rotate the pojectile to send its damage?
        transform.Rotate(0, 0, TurretValues.damage);
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
