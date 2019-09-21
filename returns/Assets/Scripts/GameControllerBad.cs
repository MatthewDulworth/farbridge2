using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
   // --- classes --- //
   class Package{
      bool legit;
      GameObject gameObject;

      public bool isLegit(){
         return legit;
      }
   }
   class Customer{ 
      GameObject gameObject;
      GameObject recipt;
      Package package;
      string dialouge;

      public Package getPackage(){
         return package;
      }
   }
   class Manager{
      string positiveDialouge;
      string gameEndDialouge;
   }

   // --- member vars --- //
   int funds;
   int unhappiness;
   int days;
   List<Customer>[] customersForDay;

   // functions
   void changeHappiness(int value){
       unhappiness += value;
   }
   
   // --- run once --- //
   void Start() 
   {
   funds = 100;
      unhappiness = 100;
      customersForDay = new List<Customer>[days];
   }

   // --- run once per frame --- //
   void Update() 
   {
      // --- days loop --- //
      for(int i=0; i<days; i++){

         // --- returns loop --- //
         for(int j=0; j<customersForDay[i].Count; j++){
            Customer currentCustomer = customersForDay[i][j];
            int changeValue = 0;

            // accept
            if(Input.GetKeyDown("A")){
               changeValue = (currentCustomer.getPackage().isLegit())? -15 : -10;
            }
            // reject
            else if(Input.GetKeyDown("D")){
               changeValue = (currentCustomer.getPackage().isLegit())? 15 : 10;
            }
            // inspect option
            else if(Input.GetKeyDown("I")){
               
            }
            changeHappiness(changeValue);
         }
         // --- end of day --- //
         

      }
   }
}