﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
   enum State {
      DAY_BEGIN,
      RETURNS,
      DAY_END,
   }

   // --- member vars --- //
   const int UP = -1;
   const int DOWN = 1;

   int currentDay;
   int customerIndex;
   GameObject currentCustomerObject;
   GameObject currentPackageObject;
   Customer currentCustomer;
   State dayState;

   // serialized vars
   [SerializeField]
   int funds;
   [SerializeField]
   int happiness;
   [SerializeField]
   List<Day> days;
   
   // --- functions --- //

   // TODO: update happiness icons
   void changeHappiness(int value) {
      happiness += value;
   }
   void changeFunds(int value){
      funds += value;
   }
   void changeDayState(State state) {
      dayState = state;
      Debug.LogFormat("game state: {0}", state);
   }
   
   void newCustomer(){
      currentCustomer = days[currentDay].customers[customerIndex];
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
         changeDayState(State.DAY_END);
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
   // TODO: make this work
   void inspect() {
      Debug.LogFormat("You opened the box, description: {0}", currentCustomer.package.description);
   }

   // TODO: make these work
   void managerPositive(){
      // the manager has some positive dialouge
   }
   void loseGame(){
      // the manager has some negative dialouge
      Debug.LogFormat("You lose");
      UnityEditor.EditorApplication.isPlaying = false;
   }
   void winGame(){
      // the manager has some positive dialouge
      Debug.LogFormat("You win");
      UnityEditor.EditorApplication.isPlaying = false;
   }
   void joeMama(){
      // joe mama ending
   }

   void dayBegin() {
      Debug.LogFormat("Day {0} begin", currentDay);
      customerIndex = 0;

      newCustomer();
      changeDayState(State.RETURNS);
   }
   void returns() {
      if (Input.GetKeyUp(KeyCode.A)) {
         accept();
      } else if (Input.GetKeyUp(KeyCode.D)) {
         deny();
      } else if (Input.GetKeyUp(KeyCode.I)) {
         inspect();
      }
   }
   void dayEnd(){
      Debug.LogFormat("Day {0} end, happiness {1}, funds{2}", currentDay, happiness, funds);

      if(happiness <= 0 || funds <=0) {
         loseGame();
      } 
      else if(currentDay+1 != days.Count) {
         currentDay++;
         managerPositive();
         changeDayState(State.DAY_BEGIN);
      }
      else{
         winGame();
      }
   }

   // --- Start --- //
   void Start() {
      customerIndex = 0;
      newCustomer();

      currentDay = 0;
      funds = 100;
      happiness = 100;
      dayState = State.DAY_BEGIN;
   }

   // --- Update --- //
   void Update() {

      switch (dayState)
      {
         case State.DAY_BEGIN:
            dayBegin();
            break;
         case State.RETURNS:
            returns();
            break;
         case State.DAY_END:
            dayEnd();
            break;
      }
   }
}