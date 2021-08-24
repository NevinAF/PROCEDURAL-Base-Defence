using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : BuildObject
{
    public override float Width { get { return 3; } }
    public override float Depth { get { return 3; } }
    public override float Height { get { return 3; } }

    public override Vector3 GetBuildPlacementPosition(BuildObject buildObject, Vector3 position)
    {
        Vector3 direction = (transform.position - position).normalized;
        if (buildObject.GetComponent<Floor>())
        {
            Vector2[] snapPoints = new Vector2[]
            {
                new Vector2(-1, -1),
                new Vector2(-1, 0),
                new Vector2(-1, 1),
                new Vector2(0, -1),
                new Vector2(0, 1),
                new Vector2(1, -1),
                new Vector2(1, 0),
                new Vector2(1, 1)
            };
            Vector2 snapPoint = Functions.FindClosestPoint(new Vector2(direction.x, direction.z), snapPoints);
            Vector3 snapPointV3 = new Vector3(snapPoint.x, 0, snapPoint.y);
            
            Vector3 thisOffset = Vector3.Scale(-snapPointV3, this.transform.localScale) / 2;
            Vector3 otherOffset = Vector3.Scale(-snapPointV3, buildObject.transform.localScale) / 2;
            return this.transform.position + thisOffset + otherOffset;
        }


        throw new System.ArgumentException("BuildObject.GetBuildPlacementPosition() -- unreconized/unimplemented buildObject");
    }

    public override void OnCursorEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnCursorExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnCursorStay()
    {
        throw new System.NotImplementedException();
    }
}
