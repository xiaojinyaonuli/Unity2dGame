/* ==================================================================
* Copyright (c) 2021,@laochai
* All rights reserved.
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions
* are met:
*
* 1. Redistributions of source code must retain the above copyright
* 2. Redistributions in binary form must reproduce the above copyright
* 3.This project can only be used for study and exchange
* and can not be used for commercial purposes without my permission
* You can contact me on the blog:https://blog.csdn.net/m0_46472878
* ===================================================================
* Author:@laochai
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BornDoor_Button : MonoBehaviour
{
   public AudioSource audioSource;

   public AudioClip Buttonmusic;

   public void Buttonmu()
   {
       audioSource.clip=Buttonmusic;
       audioSource.Play();
   }
}
