using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonManager : MonoBehaviour
{

    /*
    public Paddle paddle;
    public Ball ball;
    
    GameData gameData= new GameData();
    

    public TrainData CollectData()
    {
        float[] inputs = {paddle.transform.position.x,
                        ball.transform.position.x,
                        ball.transform.position.y,
                        ball.vx,
                        ball.vy};
        float[] outputs = {(paddle.playerInput+1)/2};
        return new TrainData(inputs, outputs);
    }
    private void Update() 
    {
        if(GameManager.playMode == 1)
        {
            writeTrainData(); 
        }    
        
    }
    */


    //  this is what really matters for json stuff
    /*
    void writeTrainData()
    {
        
        gameData.trainData.Add (CollectData());

        string json = JsonUtility.ToJson(gameData);                             //class -> string
        print(Application.dataPath + "/thing.json");
        File.WriteAllText(Application.dataPath + "/thing.json",json);           //string -> json
    }

    public GameData readTrainData()
    {
        string json = File.ReadAllText(Application.dataPath + "/thing.json");   //json -> string
        GameData loadedGameData = JsonUtility.FromJson<GameData>(json);         //string -> class
        Debug.Log(loadedGameData.trainData[12].outputs[0]);
        return loadedGameData;
    }
    */
    
}
