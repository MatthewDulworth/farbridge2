using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
   enum DayState {
      DAY_BEGIN,
      RETURNS,
      INSPECT,
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
   GameObject currentCustomerObject;
   GameObject currentPackageObject;
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
      currentCustomerObject = Instantiate(days[currentDay].customers[customerIndex].gameObject, transform);
      currentPackageObject = Instantiate(days[currentDay].customers[customerIndex].package.gameObject, transform);
   }
   void endCustomer(){
      Debug.LogFormat("happiness {0}, funds {1}", happiness, funds);

      Destroy(currentCustomerObject);
      Destroy(currentPackageObject);

      customerIndex++;
      if(customerIndex != days[currentDay].customers.Count){
         newCustomer();
      } else {
         changeDayState(DayState.DAY_END);
      }
   }
   void accept() {
      Debug.LogFormat("Package {0} accepted", customerIndex);
      int value = (currentCustomer.package.isLegit() ) ? +15 : +10;
      changeHappiness(value);
      changeFunds(-10);
      endCustomer();
   }
   void deny() {
      Debug.LogFormat("package {0} denied", customerIndex);
      int value = (currentCustomer.package.isLegit() ) ? -15 : -10;
      changeHappiness(value);
      endCustomer();
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
         accept();
      } else if (Input.GetKeyDown(KeyCode.D)) {
         deny();
      } else if (Input.GetKeyDown(inspectKey)) {
         changeDayState(DayState.INSPECT);
      }
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

      if(Input.GetKeyDown(KeyCode.B)){
         changeDayState(DayState.RETURNS);
      }
   }
   void dayEnd(){
      Debug.LogFormat("Day {0} end", currentDay);

      if(currentDay+1 != days.Count){
         currentDay++;
         changeDayState(DayState.DAY_BEGIN);
      }else{
         Debug.LogFormat("Game end");
         UnityEditor.EditorApplication.isPlaying = false;
      }
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
         case DayState.INSPECT:
            inspect();
            break;
         case DayState.DAY_END:
            dayEnd();
            break;
      }
   }
}