using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] CameraMove cmMove;
    public  bool isMoving=false;
    public  bool isFalling=false;
   
    float speed = 5f;

    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;

    public Vector2 boxSize; 
    public float distance;                      
    public LayerMask layerMask;



    private void Awake()
    {
        Application.targetFrameRate = 60;
    }


    // Update is called once per frame
    void Update()
    {
      
        if (isMoving)
        {
           
            
            transform.Translate(Vector2.right*speed*Time.deltaTime);
         
        }
     
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
           
            cmMove.setTarget = true;
            score++;
            scoreText.text=score.ToString();
       
    
           
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stick"))
        {
            isMoving = false;
            CheckFall();
        }
    }

    void CheckFall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0f, Vector2.down, distance, layerMask);
        if (hit.collider == null)
        {
            isFalling = true;
        }
      

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector2)transform.position+Vector2.down*distance, boxSize);
    }

}
