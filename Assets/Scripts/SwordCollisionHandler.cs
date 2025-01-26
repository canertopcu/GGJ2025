using UnityEngine;

public class SwordCollisionHandler : MonoBehaviour
{
    public Transform Sword;
    public float knockbackDistance = 1f;
    public int PlayerID = 1;
 
    private void Start()
    { 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balloon"))
        {
            Balloon b=other.GetComponent<Balloon>();
            if (b.PlayerID != PlayerID)
            {
                GameManager.Instance.PlayerKilled(b.PlayerID);
                DestroyBalloon(b);
            }
        }
        else if (other.CompareTag("Shield"))
        {
            if (other.GetComponent<Shield>().PlayerID != PlayerID)
            {
                GetComponentInParent<IKnockback>().KnockBack();  
            }
        }
    }

    private void DestroyBalloon(Balloon balloon)
    { 
            balloon.PlayParticle(); 
            balloon.gameObject.SetActive(false);
            Debug.Log("Balon patladı!");
         
    }
 
}
