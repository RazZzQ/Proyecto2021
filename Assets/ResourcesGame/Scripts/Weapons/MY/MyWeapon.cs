using BehaviorDesigner.Runtime.Tasks.Unity.UnityDebug;
using Unity.VisualScripting;
using UnityEngine;

public class MyWeapon : MonoBehaviour
{
    public TrailRenderer trailRenderer; // Asigna el TrailRenderer en el Inspector
    public ParticleSystem MuzzleFhasParticleSystem; // Asigna el ParticleSystem en el Inspector
    public ParticleSystem ImpactparticleSystem; // Asigna el ParticleSystem en el Inspector
    public float rayDistance = 100f; // Distancia máxima del raycast
    public LayerMask targetLayer; // Asigna el layer objetivo en el Inspector
    public Transform pointShoot;
    public float damage;
    private void Start()
    {
        // Asegúrate de que el TrailRenderer esté desactivado al inicio
        trailRenderer.enabled = false;
    }

    public void Shoot_RPC(Vector3 origin, Vector3 direction)
    {
        // Reproduce el ParticleSystem (opcional)
        if (MuzzleFhasParticleSystem != null)
        {
            MuzzleFhasParticleSystem.Play();
        }

        // Activa el TrailRenderer
        trailRenderer.enabled = true;

         
        Vector3[] posTrail = new Vector3[2];
        posTrail[0] =transform.InverseTransformVector(  pointShoot.position);
        posTrail[1] = transform.InverseTransformVector(pointShoot.position + direction * rayDistance);
        //posTrail[1] = pointShoot.position + direction* rayDistance;
        Debug.DrawLine(origin, origin + direction * rayDistance, Color.red, 1.5f);
        // Variable para almacenar el hit
        RaycastHit hit;

        // Realiza el raycast con el layer específico
        if (Physics.Raycast(origin, direction, out hit, rayDistance, targetLayer))
        {
         
            posTrail[1] = hit.point;
            ImpactparticleSystem.transform.position = hit.point;
            ImpactparticleSystem.transform.rotation = Quaternion.LookRotation(hit.normal); 
            ImpactparticleSystem.Play();
            
            MyHeart heart = hit.collider.GetComponent<MyHeart>();
            if (heart)
            {
                heart.TakeDamage(damage);
                
            }
                

        }


        
        trailRenderer.SetPositions(posTrail);
        // Desactiva el TrailRenderer después de un breve tiempo
        StartCoroutine(DisableTrailAfterTime(0.1f)); // Ajusta el tiempo según lo necesites
    }
    public void StopFire()
    { 
        trailRenderer.enabled = false;
        MuzzleFhasParticleSystem.Stop();
    }
    public void Shoot_Master(Vector3 origin, Vector3 direction)
    {
        // Reproduce el ParticleSystem (opcional)
        if (MuzzleFhasParticleSystem != null)
        {
            MuzzleFhasParticleSystem.Play();
        }

        // Activa el TrailRenderer
        trailRenderer.enabled = true;


        Vector3[] posTrail = new Vector3[2];
        posTrail[0] = transform.InverseTransformVector(pointShoot.position);
        posTrail[1] = transform.InverseTransformVector(pointShoot.position + direction * rayDistance);
        //posTrail[1] = pointShoot.position + direction* rayDistance;
        Debug.DrawLine(origin, origin + direction * rayDistance, Color.red, 1.5f);
        // Variable para almacenar el hit
        RaycastHit hit;

        // Realiza el raycast con el layer específico
        if (Physics.Raycast(origin, direction, out hit, rayDistance, targetLayer))
        {

            posTrail[1] = hit.point;
            ImpactparticleSystem.transform.position = hit.point;
            ImpactparticleSystem.transform.rotation = Quaternion.LookRotation(hit.normal);
            ImpactparticleSystem.Play();
            
             

        }



        trailRenderer.SetPositions(posTrail);
        // Desactiva el TrailRenderer después de un breve tiempo
        StartCoroutine(DisableTrailAfterTime(0.1f)); // Ajusta el tiempo según lo necesites
    }
     
    private System.Collections.IEnumerator DisableTrailAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        trailRenderer.enabled = false;
    }
}
