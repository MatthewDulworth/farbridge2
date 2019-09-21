using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCo : MonoBehaviour
{
   // --- classes --- //
   class Package
   {
      bool legit;
      GameObject gameObject;

      public bool isLegit()
      {
         return legit;
      }
   }
   class Customer
   {
      GameObject gameObject;
      GameObject recipt;
      Package package;
      string dialouge;

      public Package getPackage()
      {
         return package;
      }
   }
   class Manager
   {
      string positiveDialouge;
      string gameEndDialouge;
   }
   enum DayState
   {
      DAY_BEGIN,
      RETURNS,
      DAY_END,
   }
   enum Action
   {
      ACCEPT,
      DENY,
      INSPECT,
      NONE
   }


   // --- member vars --- //
   int funds;
   int happiness;
   DayState dayState;


   int days;
   int currentDay;
   int customerIndex;
   Customer currentCustomer;
   List<Customer>[] customersForDay;


   // --- functions --- //
   void changeHappiness(int value)
   {
      happiness += value;
   }
   void changeDayState(DayState state)
   {
      dayState = state;
   }

   void newCustomer(){

   }
   void accept() {
      int value = (currentCustomer.getPackage().isLegit() ) ? +15 : +10;
      changeHappiness(value);
      
      
      newCustomer();
   }
   void deny() {

      newCustomer();
   }
   void inspect() {

   }

   void dayBegin() {
      customerIndex = 0;
      currentCustomer = customersForDay[currentDay][customerIndex];
      changeDayState(DayState.RETURNS);
   }
   void returns() {
      Action selectedAction = Action.NONE;

      if (Input.GetKeyDown("A")) {
         selectedAction = Action.ACCEPT;
      }
      else if (Input.GetKeyDown("D")) {
         selectedAction = Action.DENY;
      }
      else if (Input.GetKeyDown("I")) {
         selectedAction = Action.INSPECT;
      }

      switch (selectedAction) {
         case Action.ACCEPT:
            accept();
            break;
         case Action.DENY:
            deny();
            break;
         case Action.INSPECT:
            inspect();
            break;
      }
   }
   void dayEnd()
   {

      changeDayState(DayState.DAY_BEGIN);
   }
   

   // --- Start --- //
   void Start()
   {
      customersForDay = new List<Customer>[days];
      for (int i = 0; i < days; i++)
      {
         customersForDay[i] = new List<Customer>();
      }

      currentDay = 0;
      funds = 100;
      happiness = 100;
      dayState = DayState.DAY_BEGIN;
   }


   // --- Update --- //
   void Update()
   {
      switch (dayState)
      {
         case DayState.DAY_BEGIN:
            dayBegin();
            break;
         case DayState.RETURNS:
            returns();
            break;
         case DayState.DAY_END:
            dayEnd();
            break;
      }
   }
}
