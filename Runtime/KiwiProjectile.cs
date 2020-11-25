using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiProjectile : MonoBehaviour {
    Vector3 origin;
    Vector3 direction;
    float position;
    float speed = 100.0f;
    float life = 3.0f; //in seconds
    float precision = 0.9f;
    float shotDelta { get { return Time.fixedDeltaTime * speed; } }
    float spawnOffset = 1.0f;

    private void Start() {
    }

    public void SetShot(Vector3 origin, Vector3 direction) {        
        this.direction = (direction + Random.insideUnitSphere * (1-precision)).normalized;
        this.origin = origin + direction * spawnOffset;
    }

    void MoveShot() {
        // Position
        Vector3 bullet = Vector3.LerpUnclamped(origin, origin + direction, position);
        transform.position = bullet; // TODO: Make bouncable
        RaycastHit hit;
        if(Physics.Raycast(bullet, direction, out hit, shotDelta)) {
            position = Vector3.Distance(hit.point, origin);

            // TODO: Check hit layer
            Destroy(gameObject);
            
            //ILivingThing livingThing;
            //if((livingThing = hit.collider.GetComponentInParent<ILivingThing>()) != null) {
            //    livingThing.GotHit();
            //}

        } else {
            position += shotDelta;
        }        

        // Time
        life -= Time.fixedDeltaTime;
        if(life < 0) {
            Destroy(gameObject);
        }
    }    

    private void FixedUpdate() {
        MoveShot();
    }
}
