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
    

   public List<TrainData> trainData = new List<TrainData>();

   

   
   
}
