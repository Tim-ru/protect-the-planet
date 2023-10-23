using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public ParticleSystem particleEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void makeEffect(ParticleSystem effect, Vector2 position)
    {
        particleEffect.Play();
    }
}
