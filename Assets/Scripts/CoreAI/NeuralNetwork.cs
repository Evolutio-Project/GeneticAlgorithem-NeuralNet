using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NeuralNetwork 
{
    
    
    public CustomMatrix[] weights;
    public CustomMatrix[] biases;
    public float learningRate;
    public int layers;
    public float [][] NNShape;

    // _dataIn = list of the networks layers
    // _dataIn[layer] = list of that layers neurons
    // _dataIn[layer][neuron] = list of that neurons weights
    // _dataIn[layer][neron][weight] = that weights value.
    public NeuralNetwork(float [][] _dataIn)
    {
        weights = new CustomMatrix[_dataIn.Length]; //new matrix for each layer
        biases = new CustomMatrix[_dataIn.Length];
        layers = _dataIn.Length;
        learningRate = 0.1f;
        NNShape = _dataIn;

        for (int layer=1; layer< _dataIn.Length; layer++)
        {
        
            weights[layer] = new CustomMatrix(_dataIn[layer].Length,_dataIn[layer-1].Length); //assign neurons to matrix
            weights[layer].Randomize();
            
            biases[layer] = new CustomMatrix(_dataIn[layer].Length,1);
            biases[layer].Randomize(); 
        }
//        Debug.Log("this is a test ");

    }

    // 
    public NeuralNetwork(NeuralNetwork original, float mutationRate = .01f)
    {
        //copy original features
        weights = new CustomMatrix[original.weights.Length]; //new matrix for each layer
        biases = new CustomMatrix[original.biases.Length];
        layers = original.layers;
        learningRate = original.learningRate;      
        
        //change them up by the mutation rate
        if(mutationRate > 0 )
        {
            for(int layer=1; layer<weights.Length; layer++)
            {
                weights[layer] = new CustomMatrix(original.weights[layer]); 
                weights[layer].RandomizeSometimes(mutationRate);
                
                biases[layer] = new CustomMatrix(original.biases[layer]);
                biases[layer].RandomizeSometimes(mutationRate);
            }
        }
    }    

    public float[] FeedForward(float[] _inputs)
    {
        CustomMatrix[] layer = new CustomMatrix[layers];

        layer[0] = CustomMatrix.FromArray(_inputs);

        for(int i=0; i<layer.Length-1; i++)
        {
            
            layer[i+1] = CustomMatrix.Multiply(weights[i+1],layer[i]);
            layer[i+1] = CustomMatrix.Add(layer[i+1],biases[i+1]);

            layer[i+1] = CustomMatrix.Map(layer[i+1],CustomMatrix.sigmoid);
        }
       
        return CustomMatrix.ToArray(layer[layer.Length-1]);
    }

    public void Train(float[] _inputs, float[] _targets)
    {
        
        CustomMatrix[] layer = new CustomMatrix[layers];

        layer[0] = CustomMatrix.FromArray(_inputs);

        for(int i=0; i<layer.Length-1; i++)
        {
            
            layer[i+1] = CustomMatrix.Multiply(weights[i+1],layer[i]);
            layer[i+1] = CustomMatrix.Add(layer[i+1],biases[i+1]);

            layer[i+1] = CustomMatrix.Map(layer[i+1],CustomMatrix.sigmoid);
        }

        CustomMatrix outputs = layer[layer.Length-1];
        //return CustomMatrix.ToArray(outputs);
        //-------------^this is the feed foraward up here^

        CustomMatrix targets = CustomMatrix.FromArray(_targets);
        
     
        //this bit is used to get the first errors from output-target
        CustomMatrix[] errors = new CustomMatrix[layer.Length];
        errors[layer.Length-1] = CustomMatrix.Subtract(targets, outputs);
        //Debug.Log((layer[layer.Length-2].rows-1) +" : "+(layer[layer.Length-2].colums-1));
        for(int i=layer.Length-1; i>0; i--)
        {
   
            CustomMatrix gradients = CustomMatrix.Map(layer[i], CustomMatrix.dsigmoid); // use (delta * error * learningRate) to get gradient 
            gradients = CustomMatrix.Multiply2(gradients,errors[i]);
            gradients = CustomMatrix.Multiply(gradients, learningRate);
            
            CustomMatrix layerT = CustomMatrix.Transpose(layer[i-1]);                   // Transpose layer from (1 col, x rows) to (x col, 1 row) for math
            CustomMatrix weightLayerDelta = CustomMatrix.Multiply(gradients,layerT);    // idk how (gradient * layer) gets the right delta

            weights[i] = CustomMatrix.Add(weights[i], weightLayerDelta);                //adjust weights by deltas
            biases[i] = CustomMatrix.Add(biases[i], gradients);                         //adjust bias by delta, which is just the gradient

            
            CustomMatrix weightT = CustomMatrix.Transpose(weights[i]); 
            errors[i-1] = CustomMatrix.Multiply(weightT,errors[i]);                          //get error for next round of backprop, don't want in first round btw
        }

    }
        
}
