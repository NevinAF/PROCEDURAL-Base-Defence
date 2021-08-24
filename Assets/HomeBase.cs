using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class HomeBase : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        GetComponent<Health>().AddDeathListener(OnBaseDestroy);
        transform.position =
            new Vector3(
                0,
                TerrainGenerator.instance.GetHighestPoint(0, 0) + transform.localScale.y / 3,
                0);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnBaseDestroy()
    {
        // Explode
        // disable all scripts
        // enable endscreen -- listens for scene reload;
    }
}
