using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PACE.MeasuringTool
{

    public class MeasuringTool : MonoBehaviour
    {
        [SerializeField] private GameObject SpherePrefab;
        [SerializeField] TextMeshPro SidewaysTMP;
        [SerializeField] TextMeshPro TopBottomTMP;
        [SerializeField] float ProbeJumpDistance = 0.05f;
        [SerializeField] float ProbeMaxDistance = 10;
        [SerializeField] Material LineMaterial;
        private List<measuringProbe> probes = new List<measuringProbe>();

        class measuringProbe
        {
            public BoxCloudCollision[] Spheres = new BoxCloudCollision[2];
            public TextMeshPro TMP;
            public Vector3 Direction;
            public bool DoubleSided;


            private LineRenderer lr;

            public void Setup(Material lineMaterial, Color lineColor, float lineWidth)
            {

                lr = Spheres[0].gameObject.AddComponent<LineRenderer>();
                lr.material = new Material(lineMaterial); 
                lr.positionCount = 2;
                lr.useWorldSpace = true;
                
                
                lr.startColor = lineColor;
                lr.endColor   = lineColor;
                lr.startWidth = lineWidth;
                lr.endWidth   = lineWidth;
            }
            public void Probe(Vector3 startPos, float jumpDistance, float maxDistance)
            {
                bool r1 = true;
                if (DoubleSided) r1 = Spheres[0].Probe(startPos, -Direction, jumpDistance, maxDistance);
                bool r2 = Spheres[1].Probe(startPos, Direction, jumpDistance, maxDistance);
                TMP.text = (int)(Vector3.Distance(Spheres[0].transform.position, Spheres[1].transform.position)*100) + "cm";

                lr.enabled = r1 && r2;
                lr.SetPosition(0, Spheres[0].transform.position);
                lr.SetPosition(1, Spheres[1].transform.position);
            }
        }

        void Awake()
        {
            var sidewaysRod = new measuringProbe();
            for (int i = 0; i < 2; i++)
                sidewaysRod.Spheres[i] = Instantiate(SpherePrefab, transform).GetComponentInChildren<BoxCloudCollision>();
            sidewaysRod.TMP = SidewaysTMP;
            sidewaysRod.Direction = Vector3.right;
            sidewaysRod.DoubleSided = true;
            sidewaysRod.Setup(LineMaterial, Color.green, 0.02f);
            probes.Add(sidewaysRod);

            var topBottomRod = new measuringProbe();
            for (int i = 0; i < 2; i++)
                topBottomRod.Spheres[i] = Instantiate(SpherePrefab, transform).GetComponentInChildren<BoxCloudCollision>();
            topBottomRod.TMP = TopBottomTMP;
            topBottomRod.Direction = Vector3.down;
            topBottomRod.DoubleSided = false;
            topBottomRod.Setup(LineMaterial, Color.green, 0.02f);
            probes.Add(topBottomRod);

        }

        private Vector3 lastPos;
        void Update()
        {
            if (lastPos != transform.position)
            {
                Probe();
            }

            transform.position += Movement * Time.deltaTime;

        }

        public void Probe()
        {
            lastPos = transform.position;
            foreach (var p in probes)
            {
                p.Probe(Vector3.zero, ProbeJumpDistance, ProbeMaxDistance);
            }
        }
        public Vector3 Movement = Vector3.zero;
        public void Move()
        {
            Movement = new Vector3(0, 0, 0.5f);
        }
    }
}