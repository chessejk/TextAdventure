using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{

  //  ??????????????????    ?????????????????????????


  // Describe the "Person Class" and it's purpose, etc.


  //  ??????????????????    ?????????????????????????


  public class Person
  {
    //public:

    public Room location;
    public Item weapon;
    public int  health;                     // A quantifiable amount of health; if it goes to zero, the player dies! :)

    // Person constructor details
    public Person(Room CurrentLocation)
    {
      location = CurrentLocation;            // The location of the "person", or in the game the player's current location.
      weapon   = null;                       // A pointer to an instance of the weapon class if the player has a weapon.
      health   = 45;                         // The health of the player; this goes down if the player takes damage, and
                                           // will result in the player's death if health <= 0.
    }

    

    public bool OnPlayerMove(Direction d, bool playerWonGame, Monster monster)
    {
      const string WALL_IN_THE_WAY = "You can't go that direction, there's a wall in the way.";

      bool PlayerMoved;

      PlayerMoved = false;

      switch (d)
      {
        case (Direction.north):
          if (location.north == null)
          {
            Console.Write(WALL_IN_THE_WAY);
          }
          else
          {
            Console.WriteLine("You go North.");
            location = location.north;
            PlayerMoved = true;
            location.OnPlayerEnter(playerWonGame, location, monster);
          }
          break;
        case (Direction.south):
          if (location.south == null)
          {
            Console.WriteLine("{0}\n", WALL_IN_THE_WAY);
          }
          else
          {
            Console.WriteLine("You go South.");
            location = location.south;
            PlayerMoved = true;
            location.OnPlayerEnter(playerWonGame, location, monster);
          }
          break;
        case (Direction.east):
          if (location.east == null)
          {
            Console.WriteLine("{0}\n", WALL_IN_THE_WAY);
          }
          else
          {
            Console.WriteLine("You go East.");
            location = location.east;
            PlayerMoved = true;
            location.OnPlayerEnter(playerWonGame, location, monster);
          }
          break;
        case (Direction.west):
          if (location.west == null)
          {
            Console.WriteLine("{0}\n", WALL_IN_THE_WAY);
          }
          else
          {
            Console.WriteLine("You go West.");
            location = location.west;
            PlayerMoved = true;
            location.OnPlayerEnter(playerWonGame, location, monster);
          }
          break;
        case (Direction.finalExit):
          if (location.finalExit == null)
          {
            // do nothing
          }
          else
          {
            // in this case, internal to the code, the direction is really "finalExit" but we're making the player feel like it's "East".
            Console.WriteLine("You go east through the door.\n");
            location = location.finalExit;
            PlayerMoved = true;
          }
          break;
      } // end switch

      if (PlayerMoved && location == monster.location && monster.IsDead() == false)
      {
        Console.WriteLine("There is a {0} in the room!", monster.name);
      }

      return PlayerMoved;

    }  // End "OnPlayerMove"




    // ========================================================================================================
    // == Function: "PlayerGetItem"
    // == 
    // == Description:  Each location can only contain one item, so determine if there is an item in the
    // ==               current location, and if there's an item then attempt to get it.
    // == 
    // == Invoked by       : "TextAdventure::Update"
    // == Functions called : <none> 
    // == 
    // == Input : <none>
    // == Output: returns a boolean indicating if the "get item" was successful or not; this eventually allows
    // ==         the code to decide if the player took a turn.
    // ==
    // ========================================================================================================

    public bool PlayerGetItem()
    {
      bool itemWasRetrieved = false;

      if (location.item == null)
      {
        Console.WriteLine("There is nothing for you to get in this room.");
      }
      else
      {
        if (location.item.TryGetItem())
        {
          weapon = location.item;
          location.item = null;
          itemWasRetrieved = true;
        }
      }

      return itemWasRetrieved;

    }  // End "PlayerGetItem"




    // ========================================================================================================
    // == Function: "PlayerDropItem"
    // == 
    // == Description:  The player can only have one item -- a weapon -- so determine if the player has a 
    // ==               weapon, and if the player can drop it in the current location.
    // == 
    // == Invoked by       : "TextAdventure::Update"
    // == Functions called : <none> 
    // == 
    // == Input : *monster -- pointer to the monster, i.e. a pointer to an instance of the "Monster" object.
    // ==
    // == Output: returns a boolean indicating if the "drop item" was successful or not; this eventually allows
    // ==         the code to decide if the player took a turn.
    // ==
    // ========================================================================================================

    public bool PlayerDropItem(Monster monster)
    {
      bool itemWasDropped = false;

      if (weapon == null)
      {
        Console.WriteLine("You don't have anything to drop.");
        itemWasDropped = false;
      }
      else
      {
        if (location.item == null)
        {
          // No items at this location so just drop the item.
          Console.WriteLine("You drop the {0}.", weapon.name);

          location.PutItem(weapon);

          weapon = null;
          itemWasDropped = true;
        }
        else
        {
          Console.WriteLine("If you're going to drop your {0}, do it in a room where it won't get covered with {1} blood.", weapon.name, monster.name);

          itemWasDropped = false;
        }
      }

      return itemWasDropped;

    }  // End "PlayerDropItem"




    // ========================================================================================================
    // == Function: "OnPlayerInventory"
    // == 
    // == Description:  Tells the player what they have in their inventory.  In the current version of the game,
    //                  the player can only have one item; a weapon.
    // == 
    // == Invoked by       :  "TextAdventure::Update"
    // == Functions called :  <none>
    // == 
    // == Input : <none>
    // == Output: <none>
    // ==
    // ========================================================================================================

    public void OnPlayerInventory()
    {
      if (weapon == null)
      {
        Console.WriteLine("You don't have anything.");
      }
      else
      {
        Console.WriteLine("You have a {0}.", weapon.name);
      }
    }




    // ============================================================================================================
    // == Function: "OnPlayerLookAtRoom"
    // == 
    // == Description:  Gives more detailed information about the current location of the player by calling
    // ==               the related function for the Room class, and checking to see if the current location of
    // ==               the monster object is the same as the current location of the player, and displaying
    // ==               related text if needed.
    // ==              
    // == Invoked by       :  "TextAdventure::Update"
    // == Functions called :  "Room::OnPlayerLook"   , "Monster::IsDead"
    // == 
    // == Input : playerWonGame -- a flag that indicates if the player has won the game or not.
    // ==         *monster      -- pointer to the monster, i.e. a pointer to an instance of the "Monster" object.
    // ==
    // == Output: <none>
    // ==
    // ============================================================================================================

    public void OnPlayerLookAtRoom(bool playerWonGame, Monster monster)
    {
      location.OnPlayerLook(playerWonGame, location, monster);
      
      if (location == monster.location && monster.IsDead() == false)
      {
        Console.WriteLine("There is a {0} in the room!", monster.name);
      }

    }




    // ========================================================================================================
    // == Function: "Attack"
    // == 
    // == Description:  Determines a random number from 8 to 16 and then calls "Monster::TakeDamage" to
    // ==               to adjust the monster's health appropriately.
    // == 
    // == Invoked by       :  "TextAdventure::PlayerTriesToAttack"
    // == Functions called :  "Monster::TakeDamage"
    // == 
    // == Input : *monster -- pointer to the monster, i.e. a pointer to an instance of the "Monster" object.
    // == Output: <none>
    // ==
    // ========================================================================================================

    public void Attack(Monster monster)
    {
      int randDamage;

      Random rand = new Random();

      randDamage = rand.Next(9) + 8; // Generate a random number from 8 to 16 dmg

      monster.TakeDamage(randDamage);
    }




    // ========================================================================================================
    // == Function: "TakeDamage"
    // == 
    // == Description:  Adjusts the player's health by the amount of damage caused when the monster attacked.
    // == 
    // == Invoked by       :  "Monster::Attack"
    // == Functions called :  <none>
    // == 
    // == Input : damage -- the amount of damage done to the player by the monster.
    // == Output: <none>
    // ==
    // ========================================================================================================

    public void TakeDamage(int damage, string monsterName)
    {
      health = health - damage;

      Console.WriteLine("The {0} hits you for {1} points of damage!  Your remaining health = {2}.", monsterName, damage, health);
    }




    // ========================================================================================================
    // == Function: "IsDead"
    // == 
    // == Description:  Evaluates the health of the Person object, and determines if it is "dead" or not.
    // == 
    // == Invoked by       :  "TextAdventure::Update"
    // == Functions called :  <none>
    // == 
    // == Input : <none>
    // == Output: returns a boolean value indicating if the person/player is dead or not.
    // ==
    // ========================================================================================================

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

  };
}
