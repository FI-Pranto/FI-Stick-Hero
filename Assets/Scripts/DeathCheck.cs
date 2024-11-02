using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCheck : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform player;


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x,transform.position.y,transform.position.z); 
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
