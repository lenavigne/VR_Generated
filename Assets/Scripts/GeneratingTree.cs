using System;
using Unity.VisualScripting;
using UnityEngine;

public class GeneratingTree : MonoBehaviour
{

    //[SerializeField] private Material _material;
    [SerializeField] private GameObject p_branch; 
    void Start()
    {
        Generate("F+FFF", Mathf.PI / 4);
        
        //GameObject b = Instantiate(p_branch);
        //b.GetComponent<Branch>().CreateBranch(0, 0, 0, 0, 2, 0);
    }

    public void Generate(string gram, float angle)
    {
        float currentX = 0;
        float currentY = 0;
        float currentZ = 0;
        float currentA = 0;

        foreach (var c in gram)
        {
            switch (c)
            {
                case 'F':
                    float x2 = 0;
                    float y2 = currentY + Mathf.Sin(angle);
                    float z2 = currentZ + Mathf.Cos(angle);
                    GameObject b = Instantiate(p_branch);
                    b.GetComponent<Branch>().CreateBranch(currentX, currentY, currentZ, x2, y2, z2);
                    currentX = x2;
                    currentY = y2;
                    currentZ = z2;
                    break;
                case '+':
                    currentA += angle;
                    break;
                case '-':
                    currentA -= angle;
                    break;

            }
        }
        
    }
}
