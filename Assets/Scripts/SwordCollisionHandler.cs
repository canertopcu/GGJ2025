using UnityEngine;

public class SwordCollisionHandler : MonoBehaviour
{
    public Transform Sword;  // Sword referansı
    public float knockbackDistance = 1f;  // Kalkan çarptığında geri gitme mesafesi
    public int PlayerID = 1;
    private void OnTriggerEnter(Collider other)
    {
        // Balona çarparsa
        if (other.CompareTag("Balloon"))
        {
           if( other.GetComponent<Balloon>().PlayerID != PlayerID)
            {
                other.GetComponent<Balloon>().PlayParticle();

                DestroyBalloon(other.gameObject);
            }
        }
        // Kalkana çarparsa
        else if (other.CompareTag("Shield"))
        {
           if(other.GetComponent<Shield>().PlayerID != PlayerID)
            { 
                Knockback();
            }
        }
    }

    private void DestroyBalloon(GameObject balloon)
    {
        if (!GameManager.Instance.isGameFinalized) {
            GameManager.Instance.isGameFinalized = true;
            // Balon yok et
            Destroy(balloon);

            // Balon patlama efekti (isteğe bağlı)
            Debug.Log("Balon patladı!");
        } 
    }  

    public void Knockback()
    {
        // Sword'u geri hareket ettir
        Vector3 knockback = new Vector3(-knockbackDistance, 0, 0);
        Sword.localPosition += knockback;

        // Geri gitme limiti (isteğe bağlı)
        if (Sword.localPosition.x < 0)
        {
            Sword.localPosition = new Vector3(0, Sword.localPosition.y, Sword.localPosition.z);
        }

        Debug.Log("Kalkan çarptı, geri itildi!");
    }
}