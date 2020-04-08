using UnityEngine;

namespace Experimental {
    public class PathFinding : MonoBehaviour
    {
        private void Start() {
            FireRayCast();
        }

        private void FireRayCast() {
            RaycastHit hit;
            Physics.Raycast(transform.position, Target.Instance.transform.position - transform.position, out hit, 100);
            // Debug.DrawRay(transform.position, transform.position - hit.point, Color.red);
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.Log(hit.point);
            //if (!hit.collider.CompareTag("Player"))
                
        }
    }
}
