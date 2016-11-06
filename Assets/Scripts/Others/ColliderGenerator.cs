using UnityEngine;
using System.Collections.Generic;

public struct Square
{
    public float point1;
    public float point2;
    public float point3;
    public float point4;
}

public class ColliderGenerator : MonoBehaviour
{

    List<Square> vertexList;

    void Start()
    {
        vertexList = new List<Square>();
        setListSquare();
        foreach (Square s in vertexList)
        {
            Debug.Log("##########################");
            Debug.Log("s.point1 = " + s.point1);
            Debug.Log("s.point2 = " + s.point2);
            Debug.Log("s.point3 = " + s.point3);
            Debug.Log("s.point4 = " + s.point4);
            Debug.Log("##########################");
        }
    }

    void setListSquare()
    {
        MeshFilter[] tab = GetComponentsInChildren<MeshFilter>();
        Debug.LogFormat("Size tab mesh = {0}", tab.Length);
        int i = 0;
        foreach (MeshFilter t in tab)
        {
            Debug.Log("############# " + i + " #############");
            for (int count = 0; count < t.mesh.triangles.Length; count++)
            {
                Debug.LogFormat("it = {0} = {1}", count, t.mesh.triangles[count]);
            }
            Debug.Log("##########################");
            i++;
        }
        for (int count = 0; count < tab.Length; count++)
        {
            Debug.LogFormat("count = {0}", count);
            Debug.LogFormat("size tab[count].mesh.triangles = {0}", tab[count].mesh.triangles.Length);
            Square currentSquare;
            currentSquare.point1 = tab[count].mesh.triangles[0];
            currentSquare.point2 = tab[count].mesh.triangles[1];
            currentSquare.point3 = tab[count].mesh.triangles[2];
            currentSquare.point4 = tab[count].mesh.triangles[4];
            vertexList.Add(currentSquare);
        }
    }
}