using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeneratingTree : MonoBehaviour
{

    //[SerializeField] private Material _material;
    [SerializeField] private GameObject p_branch;
    [SerializeField] private float radiusBase = 0.3f;
    //private float minA_X = 10;
    //private float maxA_X = 30;
    //private float minA_Z = 45;
    //private float maxA_Z = 70;
    void Start()
    {
        Generate("F[***F/F/F][+++F-F-F][///F*F*F][---F+F+F]F[**F/F][++F-F][//F*F][--F+F]F[*F][+F][/F][-F]FF", 20, 45);
        //Generate("F", 20, 45);
        
        //GameObject b = Instantiate(p_branch);
        //b.GetComponent<Branch>().CreateBranch(0, 0, 0, 0, 2, 0);
    }

    public void Generate(string gram, float angleX, float angleZ)
    {
        Vector3 baseBranch = new Vector3(0, 0, 0);
        Vector3 rot = new Vector3(0, 0, 0);
        Transform bParent = transform;
        Stack<Transform> sRoot = new();
        float radRed = radiusBase * 0.2f;
        Stack<float> rRoot = new();


        foreach (var c in gram)
        {
            switch (c)
            {
                case 'F':
                    Quaternion angle = Quaternion.Euler(rot);
                    GameObject b = Instantiate(p_branch, bParent.position, angle);
                    b.GetComponent<Branch>().CreateBranch(radiusBase, radiusBase - radRed);
                    b.transform.SetParent(bParent);
                    b.transform.localPosition = baseBranch;
                    float h = b.GetComponent<Branch>().height;
                    Vector3 nextPos = new Vector3(0, h, 0);
                    //Debug.Log("position = " + b.transform.localPosition);//
                    //Debug.Log("nextpos = " + nextPos);//
                    //float x2 = - (b.GetComponent<Branch>().height * Mathf.Sin(angle.z));
                    //float cosY = (angle.x > angle.z) ? Mathf.Cos(angle.x) : Mathf.Cos(angle.z);
                    //float y2 = (b.GetComponent<Branch>().height * cosY);
                    //float z2 = b.GetComponent<Branch>().height * Mathf.Sin(angle.x);
                    //Vector3 nextBase = new Vector3(x2, y2, z2);
                    //currentBase += nextBase;
                    baseBranch = nextPos;
                    bParent = b.transform;
                    radiusBase -= radRed;
                    break;
                case '+':
                    rot.x += angleX;
                    break;
                case '-':
                    rot.x -= angleX;
                    break;
                case '*':
                    rot.z += angleZ;
                    break;
                case '/':
                    rot.z -= angleZ;
                    break;
                case '[':
                    sRoot.Push(bParent); //beginning of the branch, ad the root in the stack
                    rRoot.Push(radiusBase); 
                    break;
                case ']':
                    bParent = sRoot.Pop(); //end of the branch, return to the root 
                    radiusBase = rRoot.Pop(); 
                    break;
                default:
                    break;
            }
        }
        
    }
}
