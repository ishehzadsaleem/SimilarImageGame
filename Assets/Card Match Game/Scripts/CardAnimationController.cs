using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    public Image cardSprite;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AnimationComplete()
    {
        cardSprite.enabled = true;
        GetComponent<Image>().color = Color.white;
        GetComponent<Button>().enabled = false;
    }
    public void AnimationComplete_SpriteOff()
    {
        cardSprite.enabled = false;
        GetComponent<Image>().color = Color.gray;
        GetComponent<Button>().enabled = true;
    }
}
