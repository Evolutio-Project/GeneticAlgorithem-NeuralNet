﻿using System.Collections;
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
    //public Transform end;
    

    private void Start()
    {
        InitPopulation();
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
            GameObject go = Instantiate(creaturePrefab, start.position, Quaternion.identity);
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
            survivors.Add(GetFittest());
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

        //print(" " + population[0].fitness());

       


        for(int i=0;population.Count < populationSize;i++)
        {
            GameObject go1 = Instantiate(creaturePrefab, start.position, Quaternion.identity);
            go1.GetComponent<GeneticPathfinder>().InitCreature(new NeuralNetwork (survivors[Random.Range(0,(int)survivorCut)].dna,.01f));
            population.Add(go1.GetComponent<GeneticPathfinder>());

            //print("new fitnesses: "+ population[i].dna.weights[1]);

            
            
            // if(population[0].fitness() != bestFitness)
            // {
            //     print("added to index: " +population.Count);
            //     print("why: " +population[0].fitness()+ " " +bestFitness);
            //     Debug.Break();
            //     return;
            // }
        }

        // NeuralNetwork oldBest = new NeuralNetwork(population[0].dna);
        // population[0].dna.weights[1].data[1][0] = 12;

        // print("special " + population[0].dna.weights[1].data[1][0] + " | " + oldBest.weights[1].data[1][0]);
        
        // if(population[0].dna.weights[1].data[1][0] != oldBest.weights[1].data[1][0])
        // {
        //     foreach(GeneticPathfinder gp in population)
        //     {
        //         print("last fitnesses: "+gp.fitness());
        //     }
        //     // foreach(GeneticPathfinder gp in survivors)
        //     // {
        //     //     print("last fitnesses: "+gp.fitness());
        //     // }
        //     Debug.Break();
        //     return;
        // }
        


        //delete old survivors list 
        for (int i=0; i<survivors.Count; i++)
        {
            Destroy(survivors[i].gameObject);
        }
        //Debug. Break();
    }

    
    public GeneticPathfinder GetFittest(bool forNextGen = true)
    {
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
