using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BAA
{
    public class Elevator : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _starPosition;
        [SerializeField]
        private Vector3 _endPosition;
        
        private void OnTriggerEnter(Collider other)
        {
            StopAllCoroutines();
            StartCoroutine(MoveUp());
        }
        

        private IEnumerator MoveUp()
        {
            int counter = 0;
            while(transform.position != _endPosition)
            {
                counter++;
                if(counter > 1000)
                {
                    Debug.Log("err");
                    StopAllCoroutines();
                }
                transform.position = Vector3.Lerp(_starPosition, _endPosition, Time.deltaTime);
            }
            yield return null;
        }
    }
}
