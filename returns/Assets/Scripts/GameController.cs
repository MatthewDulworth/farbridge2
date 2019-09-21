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
   const int UP = -1;
   const int DOWN = 1;

   int currentDay;
   int customerIndex;
   Customer currentCustomer;
   DayState dayState;

   // serialized vars
   [SerializeField]
   int funds;
   [SerializeField]
   int happiness;
   [SerializeField]
   List<Day> days;

   public string acceptKey = "A";
   public string denyKey = "D";
   public string inspectKey = "I";
   public string backKey = "B";

   
   // --- functions --- //

   // TODO: update happiness icons
   void changeHappiness(int value) {
      happiness += value;
      Debug.LogFormat("current happiness: {0}", happiness);
   }
   void changeFunds(int value){
      funds += value;
   }
   void changeDayState(DayState state) {
      dayState = state;
      Debug.LogFormat("game state: {0}", state);
   }
   
   // TODO: make this do something
   void newCustomer(){
      Instantiate(days[currentDay].customers[customerIndex].gameObject, transform);
      Instantiate(days[currentDay].customers[customerIndex].package.gameObject, transform);
   }
   void accept() {
      int value = (currentCustomer.package.isLegit() ) ? +15 : +10;
      changeHappiness(value);
      changeFunds(-10);

      Debug.LogFormat("package {0} accepted, happiness {1}", customerIndex, happiness);
      customerIndex++;

      // if(customerIndex != days[currentDay].customers.Count){
         
      // } else {
      //    changeDayState(DayState.DAY_END);
      // }
   }
   void deny() {
      int value = (currentCustomer.package.isLegit() ) ? -15 : -10;
      changeHappiness(value);

      Debug.LogFormat("package {0} denied, happiness {1}", customerIndex, happiness);
      // Destroy(days[currentDay].customers[customerIndex].gameObject);

      customerIndex++;

      // if(customerIndex != days[currentDay].customers.Count){
         
      // } else {
      //    changeDayState(DayState.DAY_END);
      // }
   }
   void back(){

   }
   void inspect() {

      // rotate
      int direction = 0;
      if(Input.GetKeyDown(KeyCode.UpArrow)){
         direction += DOWN;
      } 
      if(Input.GetKeyDown(KeyCode.DownArrow)){
         direction += UP;
      }
      
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
      Debug.LogFormat("Day {0} begin", currentDay);
   
      customerIndex = 0;
      currentCustomer = days[currentDay].customers[customerIndex];
      changeDayState(DayState.RETURNS);
   }
   void returns() {
      Action selectedAction = Action.NONE;

      if (Input.GetKeyDown(KeyCode.A)) {
         selectedAction = Action.ACCEPT;
      }
      else if (Input.GetKeyDown(KeyCode.D)) {
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
      Debug.LogFormat("Day {0} end", currentDay);

      if(currentDay == days.Count){
         Debug.LogFormat("Game end");
         UnityEditor.EditorApplication.isPlaying = false;
      }

      currentDay++;
      changeDayState(DayState.DAY_BEGIN);
   }

   // --- Start --- //
   void Start() {
      customerIndex = 0;
      newCustomer();

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