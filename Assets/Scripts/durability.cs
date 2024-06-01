using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class durability : MonoBehaviour
{
    public int durabilityValue = 8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (durabilityValue <= 0)
        {
            EEFManager.instance.BrokenAudio();
            Destroy(gameObject);
        }
    }
}
