using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
   [SerializeField] AudioSource MainTheme;
   public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
   {
      float startVolume = audioSource.volume;
      while (audioSource.volume > 0)
      {
         audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
         yield return null;
      }
      audioSource.Stop();
   }
   public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
   {
      audioSource.Play();
      audioSource.volume = 0f;
      while (audioSource.volume < 1)
      {
         audioSource.volume += Time.deltaTime / FadeTime;
         yield return null;
      }
   }

   public static void playCharecterTheme(Customer customer)
   {
      if(customer.theme != null)
      {
         FadeOut(MainTheme, 2f);
         FadeIn(customer.theme, 2f);
         customer.theme.play();
      }
   }

   void Start()
   {
      
   }
   void Update()
   {

   }
}
