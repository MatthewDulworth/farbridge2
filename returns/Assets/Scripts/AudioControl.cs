﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
   [SerializeField] static AudioSource mainTheme;
   [SerializeField] static AudioSource charecterTheme;
   [SerializeField] static AudioSource soundEffect;
   [SerializeField] static int fadeTime;

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
      charecterTheme.clip = customer.theme;
      if(charecterTheme.clip != null)
      {
         FadeOut(mainTheme, fadeTime);
         FadeIn(charecterTheme, fadeTime);
      }
   }

   void Start()
   {
      mainTheme.Play();
   }
   void Update()
   {
      mainTheme.loop = true;

      if(charecterTheme.clip != null){
         if(!charecterTheme.isPlaying){
            FadeIn(mainTheme, fadeTime);
         }
      } else{
         if(!mainTheme.isPlaying){
            FadeIn(mainTheme, fadeTime);
         }
      }
   }
}
