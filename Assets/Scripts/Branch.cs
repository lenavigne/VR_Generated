using System.Buffers.Text;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Branch : MonoBehaviour
{
    [SerializeField] Material _material;
    private Mesh _mesh;
    public float height = 2f;
    public float radius = 0.2f;
    void Start()
    {
        //CreateBranch(0, 0, 0, 0, 2, 0);
    }
    public void CreateBranch(float radiusB, float radiusT)
    {
        _mesh = new Mesh();
        Vector3[] v = new Vector3[8];
        v[0] = new Vector3( radiusB,   0,      0        );
        v[1] = new Vector3( 0,         0,      radiusB  );
        v[2] = new Vector3( -radiusB,  0,      0        );
        v[3] = new Vector3( 0,         0,      -radiusB );
        v[4] = new Vector3( 0,         height, -radiusT );
        v[5] = new Vector3( radiusT,   height, 0        );
        v[6] = new Vector3( 0,         height, radiusT  );
        v[7] = new Vector3( -radiusT,  height, 0        );

        int[] f = { 
            0, 1, 2,
            0, 2, 3,
            1, 6, 7,
            1, 7, 2,
            2, 7, 4,
            2, 4, 3,
            3, 4, 5,
            3, 5, 0,
            0, 5, 6,
            0, 6, 1,
            4, 6, 5,
            4, 7, 6,
        };

        //Vector3[] v = new Vector3[5];
        //v[0] = new Vector3(radius, 0, 0 );
        //v[1] = new Vector3( 0, 0, radius );
        //v[2] = new Vector3( -radius, 0, 0 );
        //v[3] = new Vector3( 0, 0, -radius);
        //v[4] = new Vector3( 0, height, 0 );

        //int[] f = { 0, 1, 2,
        //            0, 2, 3,
        //            0, 4, 1,
        //            1, 4, 2,
        //            2, 4, 3,
        //            3, 4, 0};

        _mesh.vertices = v;
        _mesh.triangles = f;

        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = _mesh;
        GetComponent<MeshRenderer>().material = _material;
    }
}
