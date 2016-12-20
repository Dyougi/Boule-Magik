using UnityEngine;
using System.Collections.Generic;

public struct Square
{
    public GameObject currentSquare;

    float posA;
    float posB;
    float posC;
    float posD;

    public List<Square> A;
    public List<Square> B;
    public List<Square> C;
    public List<Square> D;
}

public class ColliderGenerator : MonoBehaviour
{
    public GameObject obj;

    List<Square> listSquare;

    void Start()
    {
        listSquare = new List<Square>();
        foreach (Transform child in obj.transform)
        {
            Debug.Log("Cube " + child.gameObject.name);
            Debug.Log("vector3 = " + (child.localPosition.x - child.localScale.x * 0.5));
            Debug.Log("vector3 = " + (child.localPosition.y - child.localScale.y * 0.5));

            Debug.Log("vector3 = " + (child.localPosition.x + child.localScale.x * 0.5));
            Debug.Log("vector3 = " + (child.localPosition.y - child.localScale.y * 0.5));

            Debug.Log("vector3 = " + (child.localPosition.x + child.localScale.x * 0.5));
            Debug.Log("vector3 = " + (child.localPosition.y + child.localScale.y * 0.5));

            Debug.Log("vector3 = " + (child.localPosition.x - child.localScale.x * 0.5));
            Debug.Log("vector3 = " + (child.localPosition.y + child.localScale.y * 0.5));

            Square newSquare = new Square();

            newSquare.currentSquare = child.gameObject;
            newSquare.A = new List<Square>();
            newSquare.B = new List<Square>();
            newSquare.C = new List<Square>();
            newSquare.D = new List<Square>();
            //listSquare.Add();
        }
    }

    void Fill(List<Square> newList, float val)
    {

    }
}