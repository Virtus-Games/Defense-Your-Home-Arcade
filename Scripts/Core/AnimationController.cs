using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
     private Animator _anim;
     private Manager _manager;

     private const string _die = "die";
     private const string _moving = "moving";
     private const string _idle = "idle";
     private const string _balista = "balista";

     void Start()
     {
          _anim = GetComponent<Animator>();
          _manager = GetComponent<Manager>();
     }

     public void Attack(string val) => _anim.SetTrigger(val);
     public void Moving(bool val)
     {
          _anim.SetBool(_idle, !val);
          _anim.SetBool(_moving, val);
     }

     public void BalistaShoot(bool val)
     {
          _anim.SetBool(_moving, false);
          _anim.SetBool(_idle, false);
          _anim.SetBool(_balista, val);
     }

     string lastAnim = "";
     public void BoolAnim(string val)
     {
          
          if (lastAnim != "")
               _anim.SetBool(lastAnim, false);

          _anim.SetBool(val, true);
          lastAnim = val;
     }

     public void Die() => _anim.SetBool(_die, true);

     private const string carrying = "carrying";
     public void Carrying() => BoolAnim(carrying);


}
