using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Flow : Mechanism
{
    private bool on = false;
    private GameObject lastBullet;
    private AudioSource whereLoopIsPlayed;


    //private Material ripple = Load;

    //public Texture2D m_Ripple;
    //Renderer m_Renderer;
    int? shootSoundKey;

    private BlitController myBlit;
    //private Material _Mat = Resources.Load("Assets/Graphs_Ripple.mat", typeof(Material)) as Material;
    //private float _Percentage;

    public override bool Shoot(GameObject bulletTypePrefab, Transform firePoint, float fireRateinSec, AudioClip shootSound, float amplitudeGain, float damageMultiplier)
    {
        if (on)  //Si esta la sortida oberta
        {
            //_Percentage += Time.deltaTime * 2;
            //_Mat.SetFloat("_Percent", _Percentage);
            //if (_Percentage > 1)
            //{
            //    _Percentage = 0;
            //}

            //m_Renderer.material.SetTexture("_BlitPassTexture", m_Ripple);

            CinemachineShake.Instance.ShakeCamera(5f, .2f);

            if (!Input.GetButton("Shoot")) //Si no estem apretant la tanquem
            {
                on = false;                
                List<ParticleSystem> particlesSystems = lastBullet.GetComponentsInChildren<ParticleSystem>().ToList();
                float maxDuration = 0;
                foreach (var item in particlesSystems)
                {
                    item.Stop();
                    if (item.main.duration > maxDuration)
                    {
                        maxDuration = item.main.duration;
                    }                    
                }
                //whereLoopIsPlayed.Pause();
                GameObject.Destroy(lastBullet, maxDuration);
                //Debug.Log("On1");
                

                return false;
            }
                                                //Si estem apretant nomes actualitzem posicio i rotacio
            
            lastBullet.transform.position = firePoint.position;
            lastBullet.transform.rotation = firePoint.rotation;
            timeLastShoot = Time.time;
           // Debug.Log("On2");

            return true;
            
        }
        else //Si no esta la sortida oberta
        {
            if (Input.GetButton("Shoot"))  //Si estem apretant la obrim
            {
                on = true;
                lastBullet = GameObject.Instantiate(bulletTypePrefab, firePoint.position, firePoint.rotation);
                lastBullet.GetComponent<Bullet>().ApplyMultiplierToDamage(damageMultiplier);
                //AudioManager.Instance.PlaySound(shootSound);
                timeLastShoot = Time.time;
                shootSoundKey = AudioManager.Instance.LoadSound(shootSound, firePoint.position);
                return true;
               // Debug.Log("Off1");
            }                            
                     //Si no estem apretant es queda igual
           // Debug.Log("off2");

            return false;
        }
        

    }

   
    
}
