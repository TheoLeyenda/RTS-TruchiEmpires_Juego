﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float recursoPiedra;
    public float recursoOro;
    public float recursoAlimento;
    public float recursoMadera;
    public static GameManager instanceGameManager;
    public GeneradorDeNodos generadorNodos;
    public PathGenerator pathGenerator;
    
    private void Awake()
    {
        if (instanceGameManager == null)
        {
            instanceGameManager = this;
        }
        else if (instanceGameManager != null)
        {
            gameObject.SetActive(false);
        }

    }
    // Update is called once per frame
    //void Update()
    //{

    //}
    public Node FindClosestNode(Vector3 pos)
    {
        return generadorNodos.GetClosestNode(pos);
    }
   
}
