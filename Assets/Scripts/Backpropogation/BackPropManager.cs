using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPropManager : MonoBehaviour
{
    private NeuralNetwork nn;
    public XorTileGrid xorTileGrid;

    void Start()
    {
       InitNN();
    }

    public void InitNN(NeuralNetwork otherNN = null)
    {
         // quickly put together 2D array for shape of NeralNet
        int[] neuralNet = GameManager.instance.NNShape;
        
        if(otherNN == null){
            nn = new NeuralNetwork(neuralNet);
        }else{
            nn = otherNN;
        }

        TrainData data = GameData.trainingData[0];
        nn.Train(data.inputs,data.targets);

//        print("thing: "+ data.targets[0]);
        
        //print(nn.FeedForward(new float[]{0,1})[0]);
        //xorTileGrid.FeedForwardUpdate(nn);
    }
    

    
    void Update()
    {
        if(GameManager.instance.paused == false)
        {
            Play();
        }
    }
    void Play()
    {
        for(int i=0;i<1000; i++)
        {
            TrainData data = GameData.trainingData[Random.Range(0,4)];
            nn.Train(data.inputs,data.targets);
        }
        
        // update displays
        GameManager.instance.UpdateDisplays();
        
    }
    public NeuralNetwork GetNeuralNetwork()
    {
        return nn;
    }

    public float fitness() // I literally just copy pasted this from the genetic pathfinder, too lazy to do it cleaner
    {
        
        float _fitness = 0;
        for(int i=0; i<4; i++)
        {
            int rnd =  i%4; //Random.Range(0,3);
            _fitness += Mathf.Pow( GameData.trainingData[rnd].targets[0] - nn.FeedForward(GameData.trainingData[rnd].inputs)[0],2);
        }
        
        return  1 - _fitness;
        
    }
}
