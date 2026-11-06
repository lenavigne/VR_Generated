using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(GrammarGenerator))]
//[RequireComponent(typeof(BoxCollider))]
public class GeneratingTree : MonoBehaviour
{

    private GrammarGenerator _grammar;
    [SerializeField] private GameObject p_branch;
    [SerializeField] private float widthBaseFilament = 0.03f;
    [SerializeField] private float widthReduction = 0.75f;
    [SerializeField] private float minlengthFilament = 0.2f;
    [SerializeField] private float maxlengthFilament = 0.5f;
    private float minA_X = 15f;
    private float maxA_X = 35f;
    private float minA_Z = 25f;
    private float maxA_Z = 45f;
    private List<Transform> _tips = new List<Transform>();
    public bool ToRetract = false;
    void Start()
    {
        _grammar = GetComponent<GrammarGenerator>();
        GenerateTree(_grammar.GenerateCommand());
        //GenerateTree("F[***F/F/F][+++F-F-F][///F*F*F][---F+F+F]F[**F/F][++F-F][//F*F][--F+F]F[*F][+F][/F][-F]FF", 20, 45);
        //GenerateTree("F", 20, 45);
    }

    public void GenerateTree(string gram)
    {
        Vector3 baseBranch = new Vector3(0, 0, 0);
        Vector3 rot = new Vector3(0, 0, 0);
        Transform bParent = transform;
        Stack<Transform> filamentRoot = new();
        Stack<float> widthRoot = new();
        //Vector3 minBoundsTree = Vector3.zero;
        //Vector3 maxBoundsTree = Vector3.zero;


        foreach (var c in gram)
        {
            switch (c)
            {
                case 'F':
                    float lengthFilament = Random.Range(minlengthFilament, maxlengthFilament);
                    Quaternion angle = Quaternion.Euler(rot);
                    GameObject b = Instantiate(p_branch, bParent.position, angle);
                    b.GetComponent<Branch>().CreateBranch(widthBaseFilament, widthBaseFilament * widthReduction, lengthFilament);
                    b.transform.SetParent(bParent);
                    b.transform.localPosition = baseBranch;
                    Vector3 nextPos = new Vector3(0, lengthFilament, 0);
                    ////Calcul des limites pour le box collider //MARCHE PAS
                    //minBoundsTree = Vector3.Min(minBoundsTree, b.transform.position);
                    //minBoundsTree = Vector3.Min(minBoundsTree, b.transform.position + (angle * Vector3.up * lengthFilament));
                    //maxBoundsTree = Vector3.Max(maxBoundsTree, b.transform.position);
                    //maxBoundsTree = Vector3.Max(maxBoundsTree, b.transform.position + (angle * Vector3.up * lengthFilament));
                    baseBranch = nextPos;
                    bParent = b.transform;
                    widthBaseFilament *= widthReduction;
                    break;
                case '+':
                    rot.x += Random.Range(minA_X, maxA_X);
                    break;
                case '-':
                    rot.x -= Random.Range(minA_X, maxA_X);
                    break;
                case '*':
                    rot.z += Random.Range(minA_Z, maxA_Z);
                    break;
                case '/':
                    rot.z -= Random.Range(minA_Z, maxA_Z);
                    break;
                case '[':
                    filamentRoot.Push(bParent); //beginning of the branch, ad the root in the stack
                    widthRoot.Push(widthBaseFilament); 
                    break;
                case ']':
                    bParent.GetComponent<Branch>()._isTip = true;
                    _tips.Add(bParent);
                    bParent = filamentRoot.Pop(); //end of the branch, return to the root 
                    widthBaseFilament = widthRoot.Pop(); 
                    break;
                default:
                    break;
            }
        }

        //BoxCollider bcollider = GetComponent<BoxCollider>();
        //bcollider.center = (minBoundsTree + maxBoundsTree) / 2f;
        //bcollider.size = (maxBoundsTree - minBoundsTree) /2f;
    }

    public void RetractTree()
    {
        if (!ToRetract)
        {
            ToRetract = true;
            foreach (Transform t in _tips)
            {
                t.GetComponent<Branch>()._isRetracting = true;
            }
        }
    }
}
