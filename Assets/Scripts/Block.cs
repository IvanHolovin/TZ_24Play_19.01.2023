using System;
using Unity.VisualScripting;
using UnityEngine;

 public class Block : MonoBehaviour
 {
     private BlockStackManager _manager;

     public void InitBlock(BlockStackManager manager)
     {
         _manager = manager;
     }
     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("BlockToPick"))
         {
             other.gameObject.SetActive(false);
             _manager.AddBlock();
         }

         if (other.CompareTag("RedBlock"))
         {
             Collider redBlockCollider = other.GetComponent<Collider>();
             redBlockCollider.isTrigger = false;
             _manager.RemoveBlock(this);
         }
     }
 }
