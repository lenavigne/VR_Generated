using Unity.VisualScripting;
using UnityEngine;

public class GeneratingTree : MonoBehaviour
{

    [SerializeField] private Material _material;
    void Start()
    {
        //Generate("F+F-FF", Mathf.PI / 2);
        CreateBranch(0f, 0f, 0f, 2f);
    }

    public void Generate(string gram, float angle)
    {
        float currentX = 0;
        float currentY = 0;
        //float currentZ = 0;
        Quaternion currentA = Quaternion.identity;

        foreach (var c in gram)
        {
            switch (c)
            {
                case 'F':
                    float x2 = currentX + Mathf.Cos(angle);
                    float y2 = currentY - Mathf.Sin(angle);
                    //float z2 = currentZ + Mathf.Cos(angle);
                    CreateBranch(currentX, currentY, x2, y2);
                    break;
                case '+':
                    currentA.z += angle;
                    break;
                case '-':
                    currentA.z -= angle;
                    break;

            }
        }
        
    }

    void CreateBranch(float cX, float cY, float x, float y)
    {
        Mesh m = Instantiate(new Mesh());
        m.AddComponent<MeshFilter>();
        m.AddComponent<MeshRenderer>();
        Vector3[] v = new Vector3[5];
        v[0] = new Vector3(cX, cY + 0.2f, 0);
        v[1] = new Vector3(cX, cY, 0.2f);
        v[2] = new Vector3(cX, cY - 0.2f, 0);
        v[3] = new Vector3(cX, cY, -0.2f);
        v[4] = new Vector3(x, y + 0.2f, 0);

        int[] f = { 0, 1, 2,
                    0, 2, 3,
                    0, 4, 1,
                    1, 4, 2,
                    2, 4, 3,
                    3, 4, 0};
        m.vertices = v;
        m.triangles = f;
        m.RecalculateNormals();
        m.RecalculateBounds();
        m.GetComponent<MeshFilter>().mesh = m;
        m.GetComponent<MeshRenderer>().material = _material;
    }
}
