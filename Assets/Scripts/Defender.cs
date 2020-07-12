using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(LineRenderer))]
public class Defender : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private DefenderData defenderData;
    [SerializeField]
    private int upgradeCost, damage;
    [SerializeField]
    private float reloadTime;
    private float fireDuration;

    private List<Offender> offendersInRange = new List<Offender>();
    [Inject]
    public void InitDefender(DefenderData defenderData)
    {
        lineRenderer = GetComponent<LineRenderer>();
        upgradeCost = defenderData.UpgradeCostBase;
        damage = defenderData.Damage;
        reloadTime = defenderData.ReloadTime;
        fireDuration = defenderData.FireDuration;
        //lineRenderer.SetPositions(new Vector3[] { Vector3.zero, transform.position });
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        //print("Collision enter " + collider.name);
        offendersInRange.Add(collider.GetComponent<Offender>());

        if (offendersInRange.Count == 1)
            RepeatAttack();
    }
    public void OnTriggerExit2D(Collider2D collider)
    {
        offendersInRange.Remove(collider.GetComponent<Offender>());       
    }
    private void RepeatAttack()
    {
        Invoke("Attack", reloadTime);
    }
    private void Attack()
    {
        if (offendersInRange.Count > 0)
        {
            //hit enemy with laserbeam
            Vector3[] positions = new Vector3[] { transform.position, offendersInRange[0].t.position };
            lineRenderer.SetPositions(positions);
            Invoke("LaserTimeout", fireDuration);

            offendersInRange[0].TakeDamage(damage);
            RepeatAttack();
        }
    }
    private void LaserTimeout()
    {
        lineRenderer.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
    }
    
}
