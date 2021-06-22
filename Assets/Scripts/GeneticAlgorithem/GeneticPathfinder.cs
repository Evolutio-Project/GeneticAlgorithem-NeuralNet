using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticPathfinder //: MonoBehaviour //only use this for ingame fitnesses
{
    //public float creatureSpeed;
    //public float pathMultiplier;
    //int pathIndex = 0;
    //public NeuralNetwork dna;
    //[System.Serializable]
    public NeuralNetwork dna;
    public bool hasFinished;
    //bool hasBeenInitialized = false;
    //Vector2 goal;
    //Vector2 nextPoint;


    
    public GeneticPathfinder(NeuralNetwork parent = null)
    {
        
        InitCreature(parent);
    }
    public IEnumerator Duration(){
        
        yield return new WaitForSeconds(.005f);
        hasFinished = true;
        
    }
    public NeuralNetwork InitCreature(NeuralNetwork parent = null)
    {
        if(parent == null)
        {
            int[] neuralNet = GameManager.instance.NNShape;
            
            dna = new NeuralNetwork(neuralNet);
        }
        else
        {
            dna = parent;
        }

        return dna;
    }

   
    
    // error is basicly inverted fitness so:  
    [SerializeField]
    //private float _fitness;  
    public float fitness() /// for some reason this does not change with the weights of my neuralnet
    {
        
        float _fitness = 0;
        for(int i=0; i<4; i++)
        {
            int rnd =  i%4; //Random.Range(0,3);
            _fitness += Mathf.Pow( GameData.trainingData[rnd].targets[0] - dna.FeedForward(GameData.trainingData[rnd].inputs)[0],2);
        }
        //_fitness =   GameData.trainingData[rnd].targets[0] -dna.FeedForward(GameData.trainingData[rnd].inputs)[0];
        //_fitness = Mathf.Abs(_fitness);
        // //print("thing" + _fitness);
        //if(_fitness == 0){_fitness = .00001f;}
        return  1 - _fitness;
        
    }
}
