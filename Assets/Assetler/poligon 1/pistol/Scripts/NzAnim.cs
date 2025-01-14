﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NzBulletLookDev
{

    public class NzAnim : MonoBehaviour
    {
        public float speed = 40;
        public GameObject bullet;
        public Transform barrel;
        public AudioSource audioSource;
        public AudioClip audioClip;

        public void Fire()
        {
            Console.WriteLine("sadasdas");
            GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
            audioSource.PlayOneShot(audioClip);
            Destroy(spawnedBullet, 2);
        }



        public float ExplodeSpeed;
        public bool IsExplode = false;
    
        Vector3 pointA;
        public Vector3 pointB;
    
        private void Start() 
       {
           pointA = transform.position;
           StartCoroutine(MoveObject(transform, pointA, pointB, ExplodeSpeed));
           
       }
      
     
        IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
        {
            var i= 0.0f;
            var rate= 1.0f/time;
            while (i < 1.0f) {
                i += Time.deltaTime * rate;
                thisTransform.position = Vector3.Lerp(startPos, endPos, i);
                yield return null; 
            }
        }
         
    }
}
