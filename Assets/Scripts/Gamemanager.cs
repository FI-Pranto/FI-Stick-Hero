using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform platformPrefab;
    [SerializeField] Transform stickPrefab;
    [SerializeField] Transform startPlatform;
    Transform stickPos;
    Transform currentstick;
    Transform currentPlatform;
    [SerializeField] Transform minX;
    [SerializeField] Transform maxX;

    [SerializeField] GameObject button;
    [SerializeField] TextMeshProUGUI scoreT;
    float posX;

    [Header("ConditionChecker")]
   bool  spawnStick = false;
   bool isPressed=false;
    public bool spawnNow = false;
    [SerializeField] PlayerMove pm;

    [Header("LerpOfStick")]
    Quaternion rot1;
    Quaternion rot2;
    Quaternion rot3;
    float elapsedTime = 0f;
    float desiredDuration = 1f;
    float desiredDuration2 = .5f;
    float percentageComplete = 0f;




   public void Play()
    {

        rot1 = Quaternion.identity;
        rot2 = Quaternion.Euler(0, 0, -90);
        rot3 = Quaternion.Euler(0, 0, -180);
        stickPos = startPlatform.Find("StickPos");
        RandomRange();
        SpawnPlatform();
        SpawnStick();
        button.SetActive(false);
        scoreT.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnNow)
        {
            stickPos = currentPlatform.Find("StickPos");
            RandomRange();
            SpawnPlatform();
            SpawnStick();
            spawnNow = false;

        }


        if (Input.GetMouseButton(0) && spawnStick)
        {
            
            currentstick.localScale = new Vector2(currentstick.localScale.x, currentstick.localScale.y + 0.1f);
            isPressed = true;
        }
        else if (isPressed)
        {
     
            spawnStick = false;
            
            LerpStickRot1();

        }
        else if (pm.isFalling)
        {
            LerpStickRot2();
        }
      



    }
    void RandomRange()
    {
       posX=Random.Range(minX.position.x, maxX.position.x);

    }

    void SpawnPlatform()//problem check
    {
       currentPlatform= Instantiate(platformPrefab,new Vector2(posX,transform.position.y),Quaternion.identity);

        float scaleX = Random.Range(0.3f, 1.5f);
        currentPlatform.localScale=new Vector2(scaleX,8f);
            
    }
    void SpawnStick()//problem
    {
        currentstick= Instantiate(stickPrefab,stickPos.position, Quaternion.identity);
        currentstick.GetComponent<BoxCollider2D>().enabled = false;
        spawnStick = true;
    }



    void LerpStickRot1()
    {
        elapsedTime += Time.deltaTime;
        percentageComplete = elapsedTime / desiredDuration;
        currentstick.rotation = Quaternion.Lerp(rot1, rot2, percentageComplete);
        if (percentageComplete >= 1)
        {
            currentstick.GetComponent<BoxCollider2D>().enabled = true;
            isPressed = false;
            percentageComplete= 0;
            elapsedTime = 0;
            pm.isMoving = true;
           
        }
    }
    void LerpStickRot2()
    {
        elapsedTime += Time.deltaTime;
        percentageComplete = elapsedTime / desiredDuration2;
        currentstick.rotation = Quaternion.Lerp(rot2, rot3, percentageComplete);
        if (percentageComplete >= 1)
        {
         
            percentageComplete = 0;
            elapsedTime = 0;
            pm.isFalling = false;

        }
    }
}
