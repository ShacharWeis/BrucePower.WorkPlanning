using UnityEngine;

namespace PACE.MeasuringTool
{
    public class MeasuringProbUtils
    {
        /// <summary>
        /// Stretches a GameObject between two points in 3D space.
        /// The object is positioned at the midpoint, rotated to face the end point,
        /// and scaled along its local Z-axis to span the distance.
        /// </summary>
        /// <param name="obj">The GameObject to stretch (e.g. a Cylinder or Cube)</param>
        /// <param name="start">The start point in world space</param>
        /// <param name="end">The end point in world space</param>
        /// <param name="axis">Local axis to stretch along (default: Vector3.forward / Z-axis)</param>
        public static void StretchBetweenPoints(GameObject obj, Vector3 start, Vector3 end, Vector3 axis = default)
        {
            if (obj == null) return;
            if (axis == default) axis = Vector3.forward;

            Vector3 direction = end - start;
            float distance = direction.magnitude;

            if (distance < Mathf.Epsilon) return; // Avoid zero-length stretch

            // 1. Position: place the object at the midpoint
            obj.transform.position = (start + end) / 2f;

            // 2. Rotation: align the chosen local axis toward the end point
            obj.transform.rotation = Quaternion.FromToRotation(axis, direction.normalized);

            // 3. Scale: stretch the object along the chosen axis to match the distance
            Vector3 originalScale = obj.transform.localScale;
            float originalAxisLength = GetAxisLength(originalScale, axis);
            float scaleFactor = (originalAxisLength > Mathf.Epsilon) ? distance / originalAxisLength : distance;

            obj.transform.localScale = GetScaledVector(originalScale, axis, scaleFactor);
        }

// --- Helpers ---

        private static float GetAxisLength(Vector3 scale, Vector3 axis)
        {
            if (axis == Vector3.forward || axis == Vector3.back) return scale.z;
            if (axis == Vector3.right || axis == Vector3.left) return scale.x;
            if (axis == Vector3.up || axis == Vector3.down) return scale.y;
            return scale.z; // fallback
        }

        private static Vector3 GetScaledVector(Vector3 scale, Vector3 axis, float newLength)
        {
            if (axis == Vector3.forward || axis == Vector3.back) return new Vector3(scale.x, scale.y, newLength);
            if (axis == Vector3.right || axis == Vector3.left) return new Vector3(newLength, scale.y, scale.z);
            if (axis == Vector3.up || axis == Vector3.down) return new Vector3(scale.x, newLength, scale.z);
            return new Vector3(scale.x, scale.y, newLength); // fallback
        }
    }
}