using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
   // classes 
   class Package{
      bool legit;
      GameObject gameObject;
   }
   class Customer{
      
      GameObject gameObject;
      GameObject package;
      GameObject recipt;
      string dialouge;
   }
   class Manager{

   }
   


   // member vars
   List<GameObject> Customers;
   int funds;
   int unhappiness;
   int maxCustomers;
   int days;



    // run once
    void Start() 
    {
       
    }

    // run once per frame
    void Update() 
    {
       // day loop
        for(int i=0; i<days; i++){
           for(int i=0; i<)


        }
    }
}
