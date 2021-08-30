using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    public const int GENETIC = 1,BACKPROPO = 2;
    public int TrainingMode = GENETIC;

    public PopulationController populationController;
    public BackPropManager backPropManager;
    
    public static GameManager instance;

    public XorTileGrid xorTileGrid;
    public NeuralNetDisplay neuralNetDisplay;
    public bool paused = false;
    
    
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
            return null;
        }
        
    }
    public void UpdateDisplays()
    {
        
        xorTileGrid.UpdateGrid();
        neuralNetDisplay.UpdateWeights();
    }

    public void Reset(){
        switch (TrainingMode)
        {
            case GENETIC:
                populationController.InitPopulation();
                break;
            case BACKPROPO:
                backPropManager.InitNN();
                break;
            default:
                break;
        }
    }
    public void ChangeTrainingType(int newMode){
        
        
        switch (newMode)
        {
            case GENETIC:
                print("the thingy " + (CurrentNeuralNetwork()==null));
                populationController.InitPopulation(CurrentNeuralNetwork());
                
                backPropManager.enabled = false;
                break;
            case BACKPROPO:
                
                backPropManager.enabled = true;
                backPropManager.InitNN(CurrentNeuralNetwork());
                populationController.TurnOff();
                //populationController.enabled = false;
                break;
            default:
                break;
        }
        TrainingMode = newMode;
    }
}
