using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PathRandomizer : MonoBehaviour
{
    public GameObject[] paths;
    public int closed_amt = 1;

    void Start()
    {
        for (int i = 0; i < closed_amt; i++)
        {
            paths[UnityEngine.Random.Range(0, paths.Length)].SetActive(true);
        }
    }
}
