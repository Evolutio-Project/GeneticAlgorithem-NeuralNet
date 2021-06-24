using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
    public struct TrainData
{
    public float[] inputs;
    public float[] targets;
    
    public TrainData(float[] _input, float[] _target)
    {
        this.inputs = _input;
        this.targets = _target;
    }
}
    
public class GameData 
{
    public static TrainData[] trainingData = new TrainData[]
    {
        new TrainData(new float[]{0,1},new float[]{1}),
        new TrainData(new float[]{1,0},new float[]{1}),
        new TrainData(new float[]{1,1},new float[]{0}),
        new TrainData(new float[]{0,0},new float[]{0}) 
    };

}
