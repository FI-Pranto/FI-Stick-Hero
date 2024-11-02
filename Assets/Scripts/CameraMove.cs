using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Gamemanager gameManager;


    public  bool setTarget = false;
    [SerializeField] Transform player;
    public  bool moveToTarget = false;
    Vector3 newPos;
    Vector3 currPos;
    float elapsedTime = 0f;
    float desiredDuration = 1f;
    float percentageComplete = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (setTarget)
        {
            newPos = new Vector3(player.position.x + 1.78f, transform.position.y, transform.position.z);
            setTarget = false;
            moveToTarget = true;
            currPos = transform.position;
            gameManager.spawnNow = true;

        }
        else if (moveToTarget)
        {
            elapsedTime += Time.deltaTime;
            percentageComplete=elapsedTime/desiredDuration;
            transform.position = Vector3.Lerp(currPos, newPos, percentageComplete);

            if (percentageComplete >= 1)
            {
                moveToTarget=false;
                elapsedTime = 0f;
                percentageComplete = 0f;

            }


        }
        
    }
}
