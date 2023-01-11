using UnityEngine;

public static class VectorMethods 
{
    public static void OrderVectorByClosestPoint(Vector3[] vectors, Vector3 Point)
    {
        for (var i = 0; i < vectors.Length; i++)
        {
            for (var y = 0; y < vectors.Length; y++)
            {
                if (Vector3.Distance(vectors[i], Point) > Vector3.Distance(vectors[y], Point))
                    (vectors[i], vectors[y]) = (vectors[y], vectors[i]);
            }
        }
    }
}
