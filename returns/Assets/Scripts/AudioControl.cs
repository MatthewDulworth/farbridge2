using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
   [SerializeField] static AudioSource MainTheme;
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
         FadeOut(MainTheme, 2);
         FadeIn(customer.theme, 2);
         customer.theme.Play();
      }
   }

   void Start()
   {
      
   }
   void Update()
   {

   }
}
