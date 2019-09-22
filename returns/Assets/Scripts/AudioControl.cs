using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
   [SerializeField] AudioSource MainTheme;
   [SerializeField] AudioSource CharecterTheme;
   [SerializeField] AudioSource SoundEffect;
   [SerializeField] int FadeTime;

   static AudioSource mainTheme;
   static AudioSource charecterTheme;
   static AudioSource soundEffect;
   static int fadeTime;

   public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime){
      float startVolume = audioSource.volume;
      while (audioSource.volume > 0)
      {
         audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
         yield return null;
      }
      audioSource.Stop();
   }
   public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime){
      audioSource.Play();
      audioSource.volume = 0f;
      while (audioSource.volume < 1)
      {
         audioSource.volume += Time.deltaTime / FadeTime;
         yield return null;
      }
   }
   
   public static void playCharecterTheme(Customer customer){
      charecterTheme.clip = customer.theme;
      mainTheme.Stop();
      charecterTheme.Play();
   }

   public static void playItemSoundeffect(Return item){
      soundEffect.clip = item.soundEffect;
      soundEffect.Play();
   }

   void Start()
   {
      mainTheme = MainTheme;
      charecterTheme = CharecterTheme;
      soundEffect = SoundEffect;
      mainTheme.Play();
   }
   void Update()
   {
      mainTheme.loop = true;
      if(!charecterTheme.isPlaying && !mainTheme.isPlaying){
         mainTheme.Play();
      }
   }
}
