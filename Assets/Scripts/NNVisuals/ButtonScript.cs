using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Sprite pauseSprite;
    public Sprite playSprite;
    public Image currentPauseImage;
    public void PauseButton()
    {
        GameManager.instance.paused = !GameManager.instance.paused;
        if(GameManager.instance.paused == true)
        {
            currentPauseImage.sprite = playSprite;
        }else{
            currentPauseImage.sprite = pauseSprite;
        }
    }

    public void ResetButton()
    {
        GameManager.instance.Reset();
    }

    public void ChangeTrainingTypeButton()
    {
        GameManager.instance.ChangeTrainingType(GameManager.instance.TrainingMode%2+1); // the %2 is because I only got 2 modes
    }
}
