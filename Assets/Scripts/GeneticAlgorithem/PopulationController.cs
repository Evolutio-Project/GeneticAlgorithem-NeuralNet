using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationController : MonoBehaviour
{
      List<GeneticPathfinder> population = new List<GeneticPathfinder>();
    public int populationSize;
    //public int geneomeLength;
    public float cutoff = .3f;
    public GameObject creaturePrefab;
    public Transform start; 
    public float bestFitness = 0;
    public bool started;
    

    private void Start()
    {
        InitPopulation();
        started = true;
    }
    
    private void LateUpdate()
    {
        if(!HasActive())
        {
            NextGeneration();
        }
    }
    void InitPopulation()
    {
        for(int i=0; i< populationSize; i++)
        {
            GameObject go = Instantiate(creaturePrefab, gameObject.transform);
            go.GetComponent<GeneticPathfinder>().InitCreature();
            population.Add(go.GetComponent<GeneticPathfinder>()); 
        }
        
    }

    void NextGeneration()
    {
        int survivorCut = Mathf.RoundToInt(populationSize * cutoff);
        List<GeneticPathfinder> survivors = new List<GeneticPathfinder>();
        
        //pick best creatures
        for(int i=0; i< survivorCut; i++)
        { 
            survivors.Add(GetFittest(true));
            if(i==0){
                
                
                if(survivors[i].fitness() > bestFitness)
                {
                    //print(survivors[i].fitness() +" is better than: "+bestFitness);      
                    bestFitness = survivors[i].fitness();
                }
            }
            
        }
 
        //kill current population
        for (int i=0; i<population.Count; i++)
        {
            Destroy(population[i].gameObject);
        }
        population.Clear();

        //make new population

        GameObject g = Instantiate(creaturePrefab, start.position, Quaternion.identity);
        g.GetComponent<GeneticPathfinder>().InitCreature(survivors[0].dna);
        population.Add(g.GetComponent<GeneticPathfinder>());

       for(int i=0;population.Count < populationSize;i++)
        {
            GameObject go1 = Instantiate(creaturePrefab, gameObject.transform);
            go1.GetComponent<GeneticPathfinder>().InitCreature(new NeuralNetwork (survivors[Random.Range(0,(int)survivorCut)].dna,.01f));
            population.Add(go1.GetComponent<GeneticPathfinder>());

           
        }
        //delete old survivors list 
        for (int i=0; i<survivors.Count; i++)
        {
            Destroy(survivors[i].gameObject);
        }
        //Debug. Break();
    }

    
    public GeneticPathfinder GetFittest(bool forNextGen = false)
    {
        if(started != true){return null;}
        float maxFitness = float.MinValue;
        int index = 1;
        for(int i=0; i< population.Count; i++)
        {
            if(population[i].fitness() > maxFitness)
            {
                maxFitness = population[i].fitness();
                index = i;
            }
            
        }
        //print("end loop");
        GeneticPathfinder fittest = population[index];
        if(forNextGen == true){
            population.Remove(fittest);
        }
        return fittest;
    }
    bool HasActive()
    {
        for (int i=0; i< population.Count; i++)
        {
            if(population[i].hasFinished == false)
            {
                
                return true;
            }
        }
//        print("done");
        return false;
    }
    

}
