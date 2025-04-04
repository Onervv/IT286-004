using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caveEntrance : MonoBehaviour
{

    public GameObject spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player) return;
        player.transform.position = spawnPos.transform.position;
    }
}
