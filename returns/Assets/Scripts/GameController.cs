using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
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
   const int UP = -1;
   const int RIGHT = 1;

   int funds;
   int happiness;
   DayState dayState;

   int currentDay;
   int customerIndex;
   public Customer currentCustomer;

   [SerializeField]
   List<Day> days;

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
      int value = (currentCustomer.package.isLegit() ) ? +15 : +10;
      changeHappiness(value);
      // change funds
      
      customerIndex++;
      newCustomer();
   }
   void deny() {
      int value = (currentCustomer.package.isLegit() ) ? -15 : -10;
      changeHappiness(value);

      customerIndex++;
      newCustomer();
   }
   void back(){

   }
   void inspect() {

      // rotate
      int direction = 0;
      if(Input.GetKeyDown(KeyCode.RightArrow)){
         direction += RIGHT;
      } 
      if(Input.GetKeyDown(KeyCode.LeftArrow)){
         direction += UP;
      }
      currentCustomer.package.rotate(direction);
      
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
   void rotate(int direction)
   {
      switch (direction)
      {
         case UP:
            // change the sprite
            break;
         case DOWN:
            // change the sprite
            break;
      }
   }


   void dayBegin() {
      customerIndex = 0;
      currentCustomer = days[currentDay].customers[customerIndex];
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

      if(customerIndex == days[currentDay].customers.Count){
         changeDayState(DayState.DAY_END);
      }
   }
   void dayEnd(){

      changeDayState(DayState.DAY_BEGIN);
   }

   // --- Start --- //
   void Start() {
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