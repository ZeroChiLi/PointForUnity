using System.Collections.Generic;
using UnityEngine;

public class SphereArea : AnnulusArea
{
    public override Vector3 GetRandomPositionInArea()
    {
        return GetWorldSpacePosition(Random.onUnitSphere * Random.Range(MinRadius, MaxRadius));
    }

    public override Vector3 GetRandomPositionInEdge()
    {
        return GetWorldSpacePosition(Random.onUnitSphere * MaxRadius);
    }

    public override Vector3 GetRandomPositionInMinEdge()
    {
        return GetWorldSpacePosition(Random.onUnitSphere * MinRadius);
    }

    public override Point GetRandomPointInMinEdge()
    {
        Vector3 pos = GetRandomPositionInMinEdge();
        return new Point(pos, GetAngleByPosition(GetLocalSpacePosition(pos)));
    }

}
