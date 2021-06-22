using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetDisplay : MonoBehaviour
{
    public GameObject NeuronImg;
    public GameObject weightLinePrefab;

    public Color good,bad;

    private LineRenderer[][][] weightLines;
    NeuralNetwork currentNeuralNetwork ;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForStart());
        
    }
    public IEnumerator WaitForStart() {
        while (GameManager.NNStarted == false)
        {
            yield return new WaitForSeconds(.05f);
        }

        SetUpDisplay();
    }

    void SetUpDisplay()
    {
        currentNeuralNetwork = GameManager.instance.CurrentNeuralNetwork();

        print(currentNeuralNetwork.NNShape);
        //set up layer organiztion
        weightLines = new LineRenderer[currentNeuralNetwork.NNShape.Length][][];
        for(int i=1; i<weightLines.Length; i++)
        {
            weightLines[i] = new LineRenderer[currentNeuralNetwork.NNShape[i]][];
            for(int j=0; j<weightLines[i].Length; j++)
            {
                weightLines[i][j] = new LineRenderer[currentNeuralNetwork.NNShape[i-1]];
            }
        }

        Vector2[][] neuronPos = PrettyPos(10,10,currentNeuralNetwork.NNShape);
        for (int i=0; i<currentNeuralNetwork.NNShape.Length; i++)
        {

            //neuronPos[i] = new Vector2[currentNeuralNetwork.NNShape[i].Length];
            for (int j=0; j<currentNeuralNetwork.NNShape[i]; j++)
            {                
                GameObject neuron = Instantiate(NeuronImg, neuronPos[i][j], Quaternion.identity,gameObject.transform);



                // set up visible weight lines
                if(i>0){
                    for(int k=0; k<currentNeuralNetwork.weights[i].colums; k++)
                    {
                        GameObject line = Instantiate(weightLinePrefab, transform.position, Quaternion.identity,neuron.transform);
                        weightLines[i][j][k] = line.GetComponent<LineRenderer>();


                        // set positions
                        Vector3[] positions = new Vector3[2];
                        positions[0] = neuronPos[i-1][k];
                        positions[1] = neuronPos[i][j];

                        line.GetComponent<LineRenderer>().SetPositions(positions);

                        // set weights
                        line.GetComponent<LineRenderer>().startWidth = currentNeuralNetwork.weights[i].data[j][k]/2;
                    }
                }
            }
        }
    }

    Vector2[][] PrettyPos(int width, int height, int[] NNShape) // make the neuron line up evenly spaced
    {
        Vector2[][] result = new Vector2[NNShape.Length][]; 
        for (int i=0; i<NNShape.Length; i++)
        {
            result[i] = new Vector2[NNShape[i]];
            for (int j=0; j<NNShape[i]; j++)
            {
                result[i][j] = new Vector2((i+1)*((float)width/(float)(NNShape.Length+1)), (j+1)*((float)height/(float)(NNShape[i]+1)));
                result[i][j] += (Vector2)transform.position;
            }
        }
        return result;
    }

    
    public void UpdateWeights()
    {
        for (int layer=1; layer<currentNeuralNetwork.NNShape.Length; layer++)
        {
            //neuronPos[layer] = new Vector2[currentNeuralNetwork.NNShape[layer].Length];
            for (int neuron=0; neuron<currentNeuralNetwork.NNShape[layer]; neuron++)
            {                
                for(int weight=0; weight<currentNeuralNetwork.weights[layer].colums; weight++)
                {
                    //set weights
                    weightLines[layer][neuron][weight].startWidth = GameManager.instance.CurrentNeuralNetwork().weights[layer].data[neuron][weight]/10;
                    
                    if(weightLines[layer][neuron][weight].startWidth >0 )
                    {
                        
                        weightLines[layer][neuron][weight].startColor = good;
                        weightLines[layer][neuron][weight].endColor = good;
                    }else {
                        weightLines[layer][neuron][weight].startColor = bad;
                        weightLines[layer][neuron][weight].endColor = bad;
                    }
                }
            }
        }
    }
}
