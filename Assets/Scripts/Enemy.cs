using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Rigidbody rb;
    public Collider col;
    public Animator anim;
    public float speed = 1.5f;
    public float health = 100f;
    public bool battle = false;
    public GameObject player;
    public bool isDead = false;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        player = GameObject.Find("PlayerController");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Check if the distance is within a certain range
        
        if(distanceToPlayer < 1f)
        {
            //attack
            rb.velocity = Vector3.zero;
            anim.SetBool("Attack", true);
            if (!battle)
            {
                player.GetComponent<SimpleCapsuleWithStickMovement>().enemyCount++;
            }
            battle = true;
        }
        else if (distanceToPlayer < 8f)
        {
            // chase the player
            transform.LookAt(player.transform);
            rb.velocity = transform.forward * speed;
            anim.SetBool("Attack", false);
            anim.SetBool("Chase", true);
            if (!battle)
            {
                player.GetComponent<SimpleCapsuleWithStickMovement>().enemyCount++;
            }
            battle = true;
        }
        else if (distanceToPlayer < 14f)
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("Chase", false);
            anim.SetBool("Alarm", true);
            if (!battle)
            {
                player.GetComponent<SimpleCapsuleWithStickMovement>().enemyCount++;
            }
            battle = true;
        }
        else
        {
            // idle
            anim.SetBool("Alarm", false);
            anim.SetBool("Idle", true);
            if (battle)
            {
                player.GetComponent<SimpleCapsuleWithStickMovement>().enemyCount--;
            }
            battle = false;
        }

    }


    public void Death()
    {
        Destroy(gameObject);
    }


    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Swords")
        {
            health -= 30f;
            slider.value = health / 100f;
            //»÷ÍËÐ§¹û
            rb.velocity = Vector3.zero;
            rb.AddForce(-transform.forward * 500f);
            EEFManager.instance.AttackAudio();

            //player±»»÷ÍË
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().AddForce(transform.forward * 500f);

            //¼õÉÙÎäÆ÷ÄÍ¾Ã¶È
            other.gameObject.GetComponent<durability>().durabilityValue--;

            if (health <= 0)
            {
                battle = false;
                isDead = true;
                anim.SetBool("Attack", false);
                anim.SetBool("Dead", true);
                player.GetComponent<SimpleCapsuleWithStickMovement>().enemyCount--;
                Invoke("Death", 2f);
            }
        }
        else if (other.gameObject.tag == "Penetrable")
        {
            // Ignore collision with walls
            Physics.IgnoreCollision(col, other.collider);
        }
    }
}
