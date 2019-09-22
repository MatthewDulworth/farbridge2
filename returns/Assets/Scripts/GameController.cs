using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
   int wins;
   int funds;
   int happiness;
   int totalCustomers;
   Customer currentCustomer;
   State dayState;


   // serialized vars
   [SerializeField] GameObject parentCustomer;
   //[SerializeField] GameObject parentPackage;
   //[SerializeField] GameObject parentRecipt;
   [SerializeField] TextMeshProUGUI descriptionTextBox;
   [SerializeField] TextMeshProUGUI dialougeTextBox;
   [SerializeField] GameObject returnsParent;
   [SerializeField] GameObject inspectParent;
   [SerializeField] List<Day> days;
   [SerializeField] SpriteRenderer packageSprite;
   [SerializeField] SpriteRenderer receiptSprite;
   
   
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
      Instantiate(days[currentDay].customers[customerIndex].gameObject, parentCustomer.transform);

      packageSprite.sprite = days[currentDay].customers[customerIndex].returnedGoods.package;
      receiptSprite.sprite = days[currentDay].customers[customerIndex].returnedGoods.receipt;
   }

   void endCustomer(){
      Debug.LogFormat("happiness {0}, funds {1}", happiness, funds);

      Destroy(parentCustomer.transform.GetChild(0));
      //Destroy(parentPackage.transform.GetChild(0));
      //Destroy(parentRecipt.transform.GetChild(0));

      customerIndex++;
      if(customerIndex != days[currentDay].customers.Count){
         newCustomer();
      } else {
         changeDayState(State.DAY_END);
      }
   }
   
   public void accept() {
      Debug.LogFormat("Package {0} accepted", customerIndex);
      int value = (currentCustomer.returnedGoods.legit) ? 15 : 10;
      changeHappiness(value);
      changeFunds(-10);
      endCustomer();
   }
   public void deny() {
      Debug.LogFormat("package {0} denied", customerIndex);
      int value = (currentCustomer.returnedGoods.legit) ? -15 : -10;
      changeHappiness(value);
      endCustomer();
   }
   // TODO: make this work
   public void inspect() {
      returnsParent.SetActive(false);
      inspectParent.SetActive(true);
      Debug.LogFormat("You opened the box, description: {0}", currentCustomer.returnedGoods.getDescription());
    }
   public void back(){
      returnsParent.SetActive(true);
      inspectParent.SetActive(false);
      Debug.LogFormat("You closed the box");
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
      returnsParent.SetActive(true);
      inspectParent.SetActive(false);
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

      
      if(currentDay+1 != days.Count) {
         currentDay++;
         managerPositive();
         changeDayState(State.DAY_BEGIN);
      }
      else{
         if(wins == totalCustomers) {
            winGame();
         } else{
            loseGame();
         } 
      }
   }

   // --- Start --- //
   void Start() {
      customerIndex = 0;
      currentDay = 0;
      dayState = State.DAY_BEGIN;

      totalCustomers = 0;
      for(int i=0; i<days.Count; i++){
         totalCustomers += days[i].customers.Count;
      } 
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