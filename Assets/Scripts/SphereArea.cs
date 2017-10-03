using System.Collections.Generic;
using UnityEngine;

public class SphereArea : AnnulusArea
{
    public override Vector3 GetRandomPositionInArea()
    {
        return Random.insideUnitSphere * Random.Range(MinRadius, MaxRadius);
    }

    public override Vector3 GetRandomPositionInEdge()
    {
        return Random.onUnitSphere * MaxRadius;
    }

    public override Vector3 GetRandomPositionInMinEdge()
    {
        return Random.onUnitSphere * MinRadius;
    }

    public override Point GetRandomPointInMinEdge()
    {
        Vector3 pos = GetRandomPositionInMinEdge();
        return new Point(pos, GetAngleByPosition(pos));
    }

}
