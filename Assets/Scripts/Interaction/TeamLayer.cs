using UnityEngine;

public class TeamLayer : MonoBehaviour
{
    public Team team;

    private void Start()
    {
        gameObject.layer = Functions.TeamToLayer(this, (GetComponent<Projectile>() != null));
    }

    public static implicit operator Team(TeamLayer teamLayer)
    {
        return teamLayer.team;
    }
}
