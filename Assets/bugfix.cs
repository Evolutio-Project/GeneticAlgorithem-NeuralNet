using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugfix : MonoBehaviour
{

    public GeneticPathfinder geneticPathfinder;
    // Start is called before the first frame update
    void Start()
    {
        print(geneticPathfinder.dna.weights[1].data[0][0]);
        print(geneticPathfinder.dna.FeedForward(new float[]{0,1})[0]);
        print(geneticPathfinder.dna.FeedForward(new float[]{0,0})[0]);
        print(geneticPathfinder.dna.FeedForward(new float[]{1,0})[0]);
        print(geneticPathfinder.dna.FeedForward(new float[]{1,1})[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
