using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomisePeg : MonoBehaviour
{
    List<int> usedPositions = new List<int>();
    public GameObject[] pegs;

    float[] positionX = { -10, 0, 10 };
    int iPos;

    float positionY;
    float positionZ;
    float scaleY;

    float scale = .5f;
    float limitZ = 11;
    float minScale = 1.2f;
    float maxScale = 2.2f;
   
    

    void Start()
    {
        

        for (int i = 0; i < positionX.Length; i++)
        {
            //Take a look, try and understand.
            do
            {
                iPos = Random.Range(0, positionX.Length);
            } while (usedPositions.Contains(iPos));

            positionZ = Random.Range(-limitZ, limitZ);
            scaleY = Random.Range(minScale, maxScale);
            positionY = scaleY;
            pegs[i].transform.localScale = new Vector3(scale, scaleY, scale);
            pegs[i].transform.position = new Vector3(positionX[iPos], positionY, positionZ);
            usedPositions.Add(iPos);
        }
        

    }

}
