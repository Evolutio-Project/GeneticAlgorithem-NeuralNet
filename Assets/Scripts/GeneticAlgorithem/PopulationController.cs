using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationController : MonoBehaviour
{
    List<GeneticPathfinder> population = new List<GeneticPathfinder>();
    private int _populationSize;
    public int populationSize;
    //public int geneomeLength;
    public float cutoff = .3f;
    //public GameObject creaturePrefab;
    public Transform start; 
    public float bestFitness = 0;
    public bool started;
    IEnumerator evoUpdate;
    // public XorTileGrid xorTileGrid;
    // public NeuralNetDisplay neuralNetDisplay;

    private void Start()
    {
        print("test");
        _populationSize = populationSize;
        evoUpdate = EvoUpdate();
        InitPopulation();
        
        
        
    }
    
    private IEnumerator EvoUpdate()
    {
        while(true)
        {
            yield return new WaitForSeconds(GameManager.instance.updateRate);
            if(GameManager.instance.paused == false)
            {
                if(!HasActive())
                {
                    NextGeneration();
                }
                //print("thing");
                // display new nerual net
                GameManager.instance.UpdateDisplays();
            }
        }
    }
    // public void InitPopulation()
    // {
    //     TurnOff();

    //     for(int i=0; i< _populationSize; i++)
    //     {
    //         GeneticPathfinder go = new GeneticPathfinder();
    //         go.InitCreature();
    //         population.Add(go); 
    //     }
    //     started = true;
    //     StartCoroutine(evoUpdate);
        
        
    // }
    public void InitPopulation(NeuralNetwork otherNN = null)
    {
        TurnOff();

        print("the thingu" + (otherNN == null));
        if(otherNN != null){print("yes");}
        if(otherNN != null){
            print("the thingt");
            GeneticPathfinder g = new GeneticPathfinder();
            g.InitCreature(otherNN);
            population.Add(g); 
            
        }

        for(int i= population.Count; i< _populationSize; i++)
        {
            GeneticPathfinder go = new GeneticPathfinder();
            go.InitCreature();
            population.Add(go); 
            
        }
        started = true;
        StartCoroutine(evoUpdate);
        
    }

    void NextGeneration()
    {
        int survivorCut = Mathf.RoundToInt(_populationSize * cutoff);
        List<GeneticPathfinder> survivors = new List<GeneticPathfinder>();
        
        //pick best creatures
        for(int i=0; i< survivorCut; i++)
        { 
            survivors.Add(GetFittest(true));
            if(i==0){
                
                //print(survivors[i] +" "+started);
                if(survivors[i].fitness() > bestFitness)
                {
                    //print(survivors[i].fitness() +" is better than: "+bestFitness);      
                    bestFitness = survivors[i].fitness();
                }
            }
            
        }
 
      
        population.Clear();

        //make new population

        GeneticPathfinder g = new GeneticPathfinder();
        g.InitCreature(survivors[0].dna);
        population.Add(g); 

        for(int i=0;population.Count < populationSize;i++)
        {
            GeneticPathfinder gol = new GeneticPathfinder();
            gol.InitCreature(new NeuralNetwork (survivors[Random.Range(0,(int)survivorCut)].dna,.01f));
            population.Add(gol); 

           
        }
       
        survivors.Clear();
        //Debug. Break();
    }

    
    public GeneticPathfinder GetFittest(bool forNextGen = false)
    {
        if(started != true){return null;}
        float maxFitness = float.MinValue;
        int index = 0;
        for(int i=0; i< population.Count; i++)
        {
            if(population[i].fitness() > maxFitness)
            {
                maxFitness = population[i].fitness();
                index = i;
            }
            
        }
        //print("end loop");
        GeneticPathfinder fittest;
//        print(population[index]);
        if(index >= populationSize)
        {
            fittest = GetFittest(false);
        }else{
            //print(index);
            fittest = population[index];
        }
        
        if(forNextGen == true){
            population.Remove(fittest);
        }
        return fittest;
    }
    bool HasActive()
    {
       
        return false;
    }
    public void TurnOff()
    {
        started = false;
        population.Clear();
        StopCoroutine(evoUpdate);
        //print("testing 1");
    }
    

}
