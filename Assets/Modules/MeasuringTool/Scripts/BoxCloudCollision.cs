
using UnityEngine;

namespace PACE.MeasuringTool
{
    public class BoxCloudCollision : MonoBehaviour
    {
        PointCloudViewer.PointCloudManager pointCloudManager;
        Renderer r;

        private void Start()
        {
            r = GetComponent<Renderer>();
            pointCloudManager = PointCloudViewer.PointCloudManager.instance;
        }
        
        public bool TestCollision()
        {
            return (pointCloudManager.BoundsIntersectsCloud(r.bounds));
        }

        public bool Probe(Vector3 startPosition, Vector3 direction, float jumpDistance, float maxDistance)
        {
            float d = 0;
            transform.localPosition = startPosition;
            Vector3 startPos = startPosition;
            while (d<maxDistance)
            {
                transform.localPosition += direction * jumpDistance;
                d = Vector3.Distance(startPos, transform.localPosition);
                if (TestCollision()) 
                    break;
            }

            return d < maxDistance;
        }
    }
}