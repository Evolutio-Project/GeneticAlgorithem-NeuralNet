using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPropManager : MonoBehaviour
{
    private NeuralNetwork nn;
    public XorTileGrid xorTileGrid;

    void Start()
    {
        // quickly put together 2D array for shape of NeralNet
        int[] neuralNet = GameManager.instance.NNShape;
        

        nn = new NeuralNetwork(neuralNet);


        

        TrainData data = GameData.trainingData[0];
        nn.Train(data.inputs,data.targets);

        print("thing: "+ data.targets[0]);
        //print(nn.FeedForward(new float[]{0,1})[0]);
//        xorTileGrid.FeedForwardUpdate(nn);
    }

    // Update is called once per frame
    void Update()
    {
        Play();
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
}
