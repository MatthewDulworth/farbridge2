using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
   // --- classes --- //
   [System.Serializable]
   public class Package {

      [SerializeField]
      bool legit;

      [SerializeField]
      string descsription;

      [SerializeField]
      GameObject gameObject;

      public bool isLegit() {
         return legit;
      }
      public bool displayText(){
         return true;
      }
      public void rotate(int direction){
         switch(direction){
            case RIGHT:
               break;
            case LEFT:
               break;
         }
      }
   }
   
   [System.Serializable]
   public class Customer {
      [SerializeField]
      GameObject gameObject;

      [SerializeField]
      GameObject recipt;

      [SerializeField]
      Package package;

      [SerializeField]
      string dialouge;

      public Package getPackage() {
         return package;
      }
   }

   [System.Serializable]
   public class Manager {
      string positiveDialouge;
      string gameEndDialouge;
   }
   enum DayState {
      DAY_BEGIN,
      RETURNS,
      DAY_END,
   }
   enum Action {
      ACCEPT,
      DENY,
      INSPECT,
      BACK,
      NONE
   }


   // --- member vars --- //
   public string acceptKey = "A";
   public string denyKey = "D";
   public string inspectKey = "I";
   public string backKey = "B";
   const int LEFT = -1;
   const int RIGHT = 1;

   int funds;
   int happiness;
   DayState dayState;

   const int days = 3;
   int currentDay;
   int customerIndex;
   public Customer currentCustomer;
   List<Customer>[] customersForDay;

   // --- functions --- //

   // TODO: update happiness icons
   void changeHappiness(int value) {
      happiness += value;
      Debug.LogFormat("current happiness: {0}", happiness);
   }
   void changeDayState(DayState state) {
      dayState = state;
      Debug.LogFormat("game state: {0}", state);
   }
   
   // TODO: make this do something
   void newCustomer(){

   }
   void accept() {
      int value = (currentCustomer.getPackage().isLegit() ) ? +15 : +10;
      changeHappiness(value);
      // change funds
      
      customerIndex++;
      newCustomer();
   }
   void deny() {
      int value = (currentCustomer.getPackage().isLegit() ) ? -15 : -10;
      changeHappiness(value);

      customerIndex++;
      newCustomer();
   }
   void back(){

   }
   void inspect() {

      // rotate
      int rotateDirection = 0;
      if(Input.GetKeyDown(KeyCode.RightArrow)){
         rotateDirection += RIGHT;
      } 
      if(Input.GetKeyDown(KeyCode.LeftArrow)){
         rotateDirection += LEFT;
      }
      currentCustomer.getPackage().rotate(rotateDirection);
      
      // accept/deny/back
      Action selectedAction = Action.NONE;
      if (Input.GetKeyDown(acceptKey)) {
         selectedAction = Action.ACCEPT;
      } 
      else if(Input.GetKeyDown(denyKey)){
         selectedAction = Action.DENY;
      } 
      else if(Input.GetKeyDown(backKey)){
         selectedAction = Action.BACK;
      }

      switch(selectedAction){
         case Action.ACCEPT:
            accept();
            break;
         case Action.DENY:
            deny();
            break;
         case Action.BACK:
            back();
            break;
      }
   }

   void dayBegin() {
      customerIndex = 0;
      currentCustomer = customersForDay[currentDay][customerIndex];
      changeDayState(DayState.RETURNS);
   }
   void returns() {
      Action selectedAction = Action.NONE;

      if (Input.GetKeyDown(acceptKey)) {
         selectedAction = Action.ACCEPT;
      }
      else if (Input.GetKeyDown(denyKey)) {
         selectedAction = Action.DENY;
      }
      // else if (Input.GetKeyDown(inspectKey)) {
      //    selectedAction = Action.INSPECT;
      // }

      switch (selectedAction) {
         case Action.ACCEPT:
            accept();
            break;
         case Action.DENY:
            deny();
            break;
         // case Action.INSPECT:
         //    inspect();
         //    break;
      }

      if(customerIndex == customersForDay[currentDay].Count){
         changeDayState(DayState.DAY_END);
      }
   }
   void dayEnd(){

      changeDayState(DayState.DAY_BEGIN);
   }

   // --- Start --- //
   void Start() {
      customersForDay = new List<Customer>[days];
      for (int i = 0; i < days; i++) {
         customersForDay[i] = new List<Customer>();
      }

      currentDay = 0;
      funds = 100;
      happiness = 100;
      dayState = DayState.DAY_BEGIN;
   }


   // --- Update --- //
   void Update() {
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
