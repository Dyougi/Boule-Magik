using UnityEngine;

public class ColliderEditor : MonoBehaviour {

    // Draws the Light bulb icon at position of the object.
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Light Gizmo.tiff");
    }

}
