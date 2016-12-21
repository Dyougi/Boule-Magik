using UnityEngine;
using System.Collections.Generic;

public struct Square
{
    public GameObject currentSquare;

    public Vector2 posA;
    public Vector2 posB;
    public Vector2 posC;
    public Vector2 posD;

    public List<Square> A;
    public List<Square> B;
    public List<Square> C;
    public List<Square> D;

    public bool isUsed;
}

public class ColliderGenerator : MonoBehaviour
{
    public GameObject obj;

    List<Square> listSquare;

    void Start()
    {
        listSquare = new List<Square>();
        fillList();
        generateCollider();
    }

    void showList()
    {
        foreach (Square square in listSquare)
        {
            Debug.Log("############### " + square.currentSquare.name + " square.A #####################");
            foreach (Square square2 in square.A)
                Debug.Log(square2.currentSquare.name);
            Debug.Log("############### " + square.currentSquare.name + "  square.B #####################");
            foreach (Square square2 in square.B)
                Debug.Log(square2.currentSquare.name);
            Debug.Log("############### " + square.currentSquare.name + "  square.C #####################");
            foreach (Square square2 in square.C)
                Debug.Log(square2.currentSquare.name);
            Debug.Log("############### " + square.currentSquare.name + "  square.D #####################");
            foreach (Square square2 in square.D)
                Debug.Log(square2.currentSquare.name);
            Debug.Log("isUsed = " + square.isUsed.ToString());
            Debug.Log("############### " + square.currentSquare.name + "  END #####################");
        }
        Debug.Log("\n");
    }

    void fillList()
    {
        foreach (Transform child in obj.transform)
        {
            if (child.gameObject.tag == "Cube")
            {
                Debug.Log(child.gameObject.name);

                Square newSquare = new Square();

                newSquare.currentSquare = child.gameObject;
                newSquare.posA = new Vector2(child.localPosition.x - child.localScale.x * 0.5f, child.localPosition.y - child.localScale.y * 0.5f);
                newSquare.posB = new Vector2(child.localPosition.x + child.localScale.x * 0.5f, child.localPosition.y - child.localScale.y * 0.5f);
                newSquare.posC = new Vector2(child.localPosition.x + child.localScale.x * 0.5f, child.localPosition.y + child.localScale.y * 0.5f);
                newSquare.posD = new Vector2(child.localPosition.x - child.localScale.x * 0.5f, child.localPosition.y + child.localScale.y * 0.5f);
                newSquare.A = new List<Square>();
                newSquare.B = new List<Square>();
                newSquare.C = new List<Square>();
                newSquare.D = new List<Square>();
                listSquare.Add(newSquare);
            }
        }

        foreach (Square square in listSquare)
        {
            foreach (Square square2 in listSquare)
            {
                if (square.currentSquare.name != square2.currentSquare.name)
                {
                    /*if (square2.posB == square.posA || square2.posC == square.posA || square2.posD == square.posA)
                        square.A.Add(square2);
                    if (square2.posA == square.posB || square2.posC == square.posB || square2.posD == square.posB)
                        square.B.Add(square2);
                    if (square2.posA == square.posC || square2.posB == square.posC || square2.posD == square.posC)
                        square.C.Add(square2);
                    if (square2.posA == square.posD || square2.posB == square.posD || square2.posC == square.posD)
                        square.D.Add(square2);*/
                }
            }
        }

        for (int count = 0; count < listSquare.Count; ++count)
        {
            Square newSquare = new Square();
            newSquare = listSquare[count];
            if (listSquare[count].A.Count > 1 && listSquare[count].B.Count > 1 && listSquare[count].C.Count > 1 && listSquare[count].D.Count > 1)
                newSquare.isUsed = true;
            else
                newSquare.isUsed = false;
            listSquare[count] = newSquare;

        }
        showList();
    }

    void generateCollider()
    {
        foreach (Square square in listSquare)
        {
            if (square.A.Count < 3)
            {

            }
        }
    }
}