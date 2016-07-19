using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{

  //  ??????????????????    ?????????????????????????


  // Describe the "Monster Class" and it's purpose, etc.


  //  ??????????????????    ?????????????????????????


  public class Monster
  {
    public string name;                              // the "name" of this monster, ex: "dragon" or "vampire"
    public int    health;                            // A quantifiable amount of health; if it goes to zero, the monster dies! :)

    public Room   location;                          // the current room the monster is in.



    // Monster constructor details
    public Monster(string strName, Room CurrentLocation)
    {
      name     = strName;
      health   = 30;
      location = CurrentLocation;
    }



  // Monster methods

      
  // ===================================================================================================
  // == Function: "OnMonsterRandomMove"
  // == 
  // == Description:  The monster stays in the same location as the player if they're both in the same
  // ==               location.  Otherwise, randomly move the monster by choosing a random number from
  // ==               0 to 1. Each room always has two, and only two exits. So choose the first valid
  // ==               exit if 0, and the second valid exit if 1.
  // == 
  // == Invoked by       :  "TakeTurn"
  // == Functions called :  <none>
  // == 
  // == Input : PlayersCurLocation  -- the players current location/room.
  // == Output: <none>
  // ==
  // ===================================================================================================

  public void OnMonsterRandomMove(Room PlayersCurLocation)
  {

    int  iRandomDir   = 0;
    int  dirCounter   = 0;
    bool monsterMoved = false;

    Random rand = new Random();

    // GO BACK AND MAKE THIS BETTER!!!!

    if (location == PlayersCurLocation)
    { // do nothing, the monster stays in the room if the player is in the same room! 
    }
    else
    {
      //srand((unsigned int)time(null));   //  initialize random seed -- need to cast as "(unsigned int)" to avoid annoying warning. =/

      iRandomDir = rand.Next(2);  // Generate a random number from 0 to 1

      if (location.north != null && monsterMoved == false)
      {
        if (dirCounter == iRandomDir)
        {
          location = location.north;
          monsterMoved = true;
        }
        else
        {
          dirCounter++;
        }
      }

      if (location.south != null && monsterMoved == false)
      {
        if (dirCounter == iRandomDir)
        {
          location = location.south;
          monsterMoved = true;
        }
        else
        {
          dirCounter++;
        }
      }

      if (location.east != null && monsterMoved == false)
      {
        if (dirCounter == iRandomDir)
        {
          location = location.east;
          monsterMoved = true;
        }
        else
        {
          dirCounter++;
        }
      }

      if (location.west != null && monsterMoved == false)
      {
        if (dirCounter == iRandomDir)
        {
          location = location.west;
          monsterMoved = true;
        }
        // don't need to increment "dirCounter" because it isn't looked at anymore
      }

      if (location == PlayersCurLocation)
      {
        // if after moving the monster is in the same room, then announce that the monster entered the room!
        Console.WriteLine("A {0} has entered the {1}!", name, location.name);
      }

    } // end "else" (monster was in a different location than the player)
  }




  // ===================================================================================================
  // == Function: "Attack"
  // == 
  // == Description:  Determines a random number from 5 to 10 and then calls "Person::TakeDamage" to
  // ==               to adjust the player's health appropriately.
  // == 
  // == Invoked by       :  "TakeTurn"
  // == Functions called :  "Person::TakeDamage"
  // == 
  // == Input : *player -- pointer to the player (an instance of the "Person" object).
  // ==         name    -- the name of the monster that's attacking the player.
  // ==
  // == Output: <none>
  // ==
  // ===================================================================================================

  public void Attack(Person player, string name)
  {
    int randDamage;

    Random rand = new Random();

    randDamage = rand.Next(6) + 5; // Generate a random number from 8 to 16 dmg

    player.TakeDamage(randDamage, name);
  }




  // =======================================================================================================
  // == Function: "TakeDamage"
  // == 
  // == Description:  Adjusts the monster's health by the amount of damage caused when the player attacked.
  // == 
  // == Invoked by       :  "Person::Attack"
  // == Functions called :  <none>
  // == 
  // == Input : damage -- the amount of damage done to the monster by the player.
  // == Output: <none>
  // ==
  // =======================================================================================================

  public void TakeDamage(int damage)
  {
    health = health - damage;

    Console.WriteLine("You hit the {0} for {1} points of damage!  The {2}'s remaining health = {3}.", name, damage, name, health);
  }




    // =====================================================================================================
    // == Function: "IsDead"
    // == 
    // == Description:  Evaluates the health of the monster object, and determines if it is "dead" or not.
    // == 
    // == Invoked by       :  "TextAdventure::Update", "TextAdventure::PlayerTriesToAttack"
    // == Functions called :  <none>
    // == 
    // == Input : <none>
    // == Output: returns a boolean value indicating if the monster is dead or not.
    // ==
    // =====================================================================================================

    public bool IsDead()
    {
      if (health > 0)
      {
        return false;
      }
      else
      {
        return true;
      }
    }




    // ========================================================================================================
    // == Function: "Monster::TakeTurn"
    // == 
    // == Description:  The game is turn based, so this function is used to take the monster's turn.  If the
    // ==               monster's in the same location as the player, then it attacks.  Otherwise, the
    // ==               monster will randomly move to a different location adjecent to it's current location.
    // == 
    // == Invoked by       :  "TextAdventure::Update"
    // == Functions called :  "Monster::OnMonsterRandomMove", "Monster::Attack"
    // == 
    // == Input : *player -- pointer to the player (an instance of the "Person" object).
    // == Output: <none>
    // ==
    // ========================================================================================================

    public void TakeTurn(Person player)
    {
      if (location == player.location)
      {
        // the monster is in same location as player, and it always attacks in that situation because it wants to kill the player!
        Console.WriteLine("The {0} attacks you!", name);

        Attack(player, name);
      }
      else
      {
        // randomly move the vampire, which may result in the vampire not moving at all.
        OnMonsterRandomMove(player.location);
      }
    }
    
  }  //  End "class Monster"

}
