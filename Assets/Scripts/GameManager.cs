using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public Player player;
   public Enemy enemy;
   public Character character;
   [SerializeField] private TMP_Text playerNameText, playerHeathText, enemyNameText, enemyHeathText;

   void Start()
   {
      playerNameText.text = player.CharName;
      enemyNameText.text = enemy.name;
      playerHeathText.text=player.health.ToString();
      enemyHeathText.text=enemy.health.ToString();
   }

   public void DoRound()
   {
      int playerDamage = player.Attack();
      enemy.GetHit(playerDamage);
      int enemyDamage = enemy.Attack();
      player.GetHit(enemy.ActiveWeapon);
      playerHeathText.text=player.health.ToString();
      enemyHeathText.text=enemy.health.ToString();
   }
}
