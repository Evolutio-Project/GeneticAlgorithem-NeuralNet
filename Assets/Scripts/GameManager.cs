using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    const int GENETIC = 1,BACKPROPO = 2;
    public PopulationController populationController;
    public BackPropManager backPropManager;
    public int TrainingMode = GENETIC;

    public static GameManager instance;

    public XorTileGrid xorTileGrid;
    public NeuralNetDisplay neuralNetDisplay;
    public int[] NNShape = {2,4,1};

    
    //private bool _NNStarted;
    public static bool NNStarted;
    [SerializeField]
    public float updateRate = .1f;

    private void Awake() {
        CurrentNeuralNetwork();
    }
    void Start()
    {
        if(GameManager.instance == null)
        {
            instance = this;
        }else
        {
            Destroy(this.gameObject);
        }
        
        //Instantiate(pixelObject, pos, Quaternion.identity,gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public NeuralNetwork CurrentNeuralNetwork()
    {
        if(TrainingMode == GENETIC)
        {
            if(populationController.GetFittest() == null)
            {
                populationController.enabled = true;
                return null;
            }else{
                NNStarted = true;
                return populationController.GetFittest().dna;
            }
        }else if(TrainingMode == BACKPROPO)
        {
            if(backPropManager.enabled == false)
            {
                backPropManager.enabled = true;
                return null;
            }else{
                NNStarted = true;
                return backPropManager.GetNeuralNetwork();;
            }
        }else
        {
            return new NeuralNetwork(new int[0]);
        }
        
    }
    public void UpdateDisplays()
    {
        
        xorTileGrid.UpdateGrid();
        neuralNetDisplay.UpdateWeights();
    }
}
