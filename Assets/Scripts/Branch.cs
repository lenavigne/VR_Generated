using System.Buffers.Text;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Branch : MonoBehaviour
{
    [SerializeField] Material _material;
    private Mesh _mesh;
    void Start()
    {
        //CreateBranch(0, 0, 0, 0, 2, 0);
    }
    public void CreateBranch(float baseX, float baseY, float baseZ, float x, float y, float z)
    {
        _mesh = new Mesh();

        Vector3[] v = new Vector3[5];
        v[0] = new Vector3(baseX + 0.2f, baseY, baseZ);
        v[1] = new Vector3(baseX, baseY, baseZ + 0.2f);
        v[2] = new Vector3(baseX - 0.2f, baseY , baseZ);
        v[3] = new Vector3(baseX, baseY, baseZ - 0.2f);
        v[4] = new Vector3(x, y, z);

        int[] f = { 0, 1, 2,
                    0, 2, 3,
                    0, 4, 1,
                    1, 4, 2,
                    2, 4, 3,
                    3, 4, 0};

        _mesh.vertices = v;
        _mesh.triangles = f;

        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        GetComponent<MeshFilter>().mesh = _mesh;
        GetComponent<MeshRenderer>().material = _material;
    }
}
