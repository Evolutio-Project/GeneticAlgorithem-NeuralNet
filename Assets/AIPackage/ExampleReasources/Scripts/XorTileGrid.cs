using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XorTileGrid : MonoBehaviour
{
    private const int V = 0;
    public GameObject pixelObject;
    public int size;

    public PopulationController population;
    public GeneticPathfinder geneticPathfinder;

   
   GameObject[][] grid;
    public void Start()
    {
        
        //print(size);
        grid = new GameObject[size][];
//        print(grid.Length);
        
        for (int i=0; i<grid.Length; i++)
        {
           grid[i] = new GameObject[size];

           for (int j=0; j<grid[i].Length; j++)
            {
                
                Vector2 pos = new Vector2((i*1)-size/2,(j*1)-size/2);
                grid[i][j] =  Instantiate(pixelObject, pos, Quaternion.identity,gameObject.transform);
            } 
        }
        print(grid.Length);
        StartCoroutine(thing());
    }
    public IEnumerator thing() {
        while (true){
            FeedForwardUpdate(population.GetFittest(false).dna);
            //print("test");
            yield return new WaitForSeconds(.05f);
            
        }

    }
    public void FeedForwardUpdate(NeuralNetwork nn) 
    {

        
        for (int i=0; i<grid.Length; i++)
        {
           
           for (int j=0; j<grid[i].Length; j++)
            {
                float x = i/((float)grid.Length-1);
                float y = j/((float)grid[i].Length-1);
                float[] input = {x,y};
//                print("this: " +geneticPathfinder);
                float c = nn.FeedForward(input)[0];
                grid[i][j].GetComponent<SpriteRenderer>().color = new Color(c,c,c);
            } 
        }
    }
}
