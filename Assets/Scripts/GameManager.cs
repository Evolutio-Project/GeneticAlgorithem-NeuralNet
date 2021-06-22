using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const int GENETIC = 1,BACKPROPO = 2;
    public PopulationController populationController;
    public int TrainingMode = GENETIC;

    public static GameManager instance;
    
    //private bool _NNStarted;
    public static bool NNStarted;

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
            return null;
        }else
        {
            return new NeuralNetwork(new float[0][]);
        }
        
    }
}
