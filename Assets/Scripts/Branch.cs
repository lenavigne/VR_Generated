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
    private float speed = 0.05f;
    public bool _isTip = false;
    public bool _isRetracting = false;
    void Start()
    {
        //CreateBranch(0, 0, 0, 0, 2, 0);
    }
    void Update()
    {
        _isTip = (transform.childCount == 0); // si le filament n'a pas d'enfant c'est un bout

        if(_isTip && _isRetracting) //si la branche est un filament de fin et qu'il doit se retracter
        {
            if(!IsRetracted()) //s'il n'est pas complètement rétracté
                Retract();
            else//sinon
            {
                transform.parent.GetComponent<Branch>()._isRetracting = true;//son parent doit se rétracter
            
                Destroy(transform.gameObject);//la branche rétractée doit etre détruite
            }
        }
        
    }
    public void CreateBranch(float radiusB, float radiusT, float height)
    {
        _mesh = new Mesh();
        speed = height / 2;
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

    private void Retract()
    {
        Vector3[] myVertices = _mesh.vertices;
        //myVertices[0] = Vector3.MoveTowards(myVertices[0], Vector3.zero, speed * Time.deltaTime);
        //myVertices[1] = Vector3.MoveTowards(myVertices[1], Vector3.zero, speed * Time.deltaTime);
        //myVertices[2] = Vector3.MoveTowards(myVertices[2], Vector3.zero, speed * Time.deltaTime);
        //myVertices[3] = Vector3.MoveTowards(myVertices[3], Vector3.zero, speed * Time.deltaTime);
        myVertices[4] = Vector3.MoveTowards(myVertices[4], Vector3.zero, speed * Time.deltaTime);
        myVertices[5] = Vector3.MoveTowards(myVertices[5], Vector3.zero, speed * Time.deltaTime);
        myVertices[6] = Vector3.MoveTowards(myVertices[6], Vector3.zero, speed * Time.deltaTime);
        myVertices[7] = Vector3.MoveTowards(myVertices[7], Vector3.zero, speed * Time.deltaTime);

        _mesh.vertices = myVertices;
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        GetComponent<MeshFilter>().mesh = _mesh;
    }

    public bool IsRetracted()
    {
        return _mesh.vertices[7].y == 0;
    }

}
