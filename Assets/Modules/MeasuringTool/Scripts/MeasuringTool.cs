using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeasuringTool : MonoBehaviour
{
    [SerializeField] private GameObject SpherePrefab;
    [SerializeField] private GameObject RodPrefab;
    [SerializeField] TextMeshPro SidewaysTMP;
    [SerializeField] TextMeshPro TopBottomTMP;
    private List<measuringRod> rods = new List<measuringRod>();
    
    class measuringRod
    {
        public GameObject[] Spheres;
        public GameObject Rod;
        public TextMeshPro TMP;
        public Vector3 Direction;
        public bool DoubleSided;
    }

    void Start()
    {
        var sidewaysRod = new measuringRod();
        sidewaysRod.Spheres = new[] { Instantiate(SpherePrefab, transform), Instantiate(SpherePrefab, transform) };
        sidewaysRod.Rod = Instantiate(RodPrefab, transform);
        sidewaysRod.TMP = SidewaysTMP;
        sidewaysRod.Direction = Vector3.right;
        sidewaysRod.DoubleSided = true;
        rods.Add(sidewaysRod);
        
        var topBottomRod = new measuringRod();
        topBottomRod.Spheres = new[] { Instantiate(SpherePrefab, transform), Instantiate(SpherePrefab, transform) };
        topBottomRod.Rod = Instantiate(RodPrefab, transform);
        topBottomRod.TMP = TopBottomTMP;
        topBottomRod.Direction = Vector3.down;
        topBottomRod.DoubleSided = false;
        rods.Add(topBottomRod);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
