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
    public float maxHealth = 100f;
    public bool battle = false;
    public GameObject player;
    public bool isDead = false;
    public Slider slider;
    public float damageSum = 0f;

    public float initialScale = 0.8f;
    public float scaleIncreaseRate = 0.005f;

    private float threshold1 = 1.2f;
    private float threshold2 = 8f;
    private float threshold3 = 14f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        player = GameObject.Find("PlayerController");
    }

    float timer = 0f;
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
        
        if(distanceToPlayer < threshold1)
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
        else if (distanceToPlayer < threshold2)
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
        else if (distanceToPlayer < threshold3)
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
        // Increase enemy size over time
        float currentScale = initialScale + (timer * scaleIncreaseRate);
        transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        //collider size
        col.transform.localScale = new Vector3(currentScale, currentScale, currentScale);

        //max health and health increase with size
        maxHealth = 100f * currentScale;
        health = maxHealth - damageSum;
        //slider
        slider.value = health / maxHealth;

        //threshold increase with size
        threshold1 = 1.2f * currentScale;
        threshold2 = 8f * currentScale;
        threshold3 = 14f * currentScale;

        timer += Time.deltaTime;
    }


    public void Death()
    {
        Destroy(gameObject);
    }


    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Swords")
        {
            //根据sword的攻击速度减少enemy的血量
            float damage = other.gameObject.GetComponent<Rigidbody>().velocity.magnitude + 20f;
            damageSum += damage;
            health -= damage;
            slider.value = health / maxHealth;
            //击退效果
            rb.velocity = Vector3.zero;
            rb.AddForce(-transform.forward * 500f);
            EEFManager.instance.AttackAudio();

            //player被击退
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().AddForce(transform.forward * 500f);

            //减少武器耐久度
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
