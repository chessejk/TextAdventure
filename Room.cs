using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
  //  ??????????????????    ?????????????????????????


  // Describe the "Room Class" and it's purpose, etc.


  //  ??????????????????    ?????????????????????????


  public class Room
  {
    const string MAID_WILL_CLEAN = "  I'm sure the maid will clean up any unpleasant items tomorrow that were left in this room...";
    const string FEELS_DIFFERENT = "  It feels different somehow.  ";

    // "Room class" attributes...

    public Room north, south, east, west, finalExit;  // pointers to other rooms/places connected to this room.

    public Item item;                                     // pointer to a potential item in this room.

    public string name;                                    // the name of this room.

    public string happyRoomDesc;                           // this text gets displayed as part of the room description only 
                                                    // when the player wins the game.


    // Room constructor details
    public Room(string strName, string strHappyRoomDesc)
    {
      north     = null;
      south     = null;
      east      = null;
      west      = null;
      finalExit = null;

      //item = null;

      name = strName;
      happyRoomDesc = strHappyRoomDesc;
    }

    public void OnPlayerEnter(bool playerWonGame, Room playerLocation, Monster monster)  //, Monster monster)
    {
      Console.Write("You are now in the {0}.", name);

      if (playerWonGame)
      {
        DisplayHappyRoomDesc(playerLocation, monster);

        if (finalExit != null)
        {
          Console.WriteLine("\n");

          // in this case, internal to the code, the direction is really "finalExit" but we're making the player feel like it's "East".
          Console.Write("{0} is to the East.  Funny, you don't remember seeing that before.", finalExit.name);
        }
      }

      Console.WriteLine();

      if (item != null)
      {
        Console.WriteLine("There is a {0} in the room.", item.name);
      }

    }

    // ========================================================================================================
    // == Function: "OnPlayerLook"
    // == 
    // == Description:  This function gives more details about the room when the player uses the "look"
    //                  command.  This is more info, including ways to leave the room, than what the player
    //                  is told when they simply enter the room.
    // == 
    // == Invoked by       :  "Person::OnPlayerLookAtRoom"
    // == Functions called :  "DisplayHappyRoomDesc"
    // == 
    // == Input : playerWonGame   -- flag indicating if the player has won the game.
    // ==         *playerLocation -- pointer to player's current location in the game.
    // ==         *monster        -- pointer to the monster, i.e. a pointer to an instance of the 
    // ==                            "Monster" object.
    // == Output: <none>
    // ==
    // ========================================================================================================

    public void OnPlayerLook(bool playerWonGame, Room playerLocation, Monster monster)
    {
      Console.Write("You are in the {0}.", name);

      if (playerWonGame)
      {
        DisplayHappyRoomDesc(playerLocation, monster);
      }

      Console.WriteLine();
      
      if (north != null)
      { Console.WriteLine("The {0} is to the North", north.name); }

      if (south != null)
      { Console.WriteLine("The {0} is to the South", south.name); }

      if (east != null)
      { Console.WriteLine("The {0} is to the East", east.name); }

      if (west != null)
      { Console.WriteLine("The {0} is to the West", west.name); }

      if (finalExit != null)
      {
        Console.WriteLine();

        // in this case, internal to the code, the direction is really "finalExit" but we're making the player feel like it's "East".
        Console.WriteLine("{0} is to the East.  Funny, you don't remember seeing that before.", finalExit.name);
      }


      if (item != null)
      {
        Console.WriteLine("There is a {0} in the room.", item.name);
      }
    }



    // ========================================================================================================
    // == Function: "DisplayHappyRoomDesc"
    // == 
    // == Description:  Displays an additional "happy" description of the location the player is in.  This is
    // ==               when the player has won the game to show that the locations are now brighter/happier.
    // == 
    // == Invoked by       :  "OnPlayerEnter", "OnPlayerLook"
    // == Functions called :  <none>
    // == 
    // == Input : *playerLocation -- pointer to player's current location in the game.
    // ==         *monster        -- pointer to the monster, i.e. a pointer to an instance of the 
    // ==                            "Monster" object.
    // == Output: <none>
    // ==
    // ========================================================================================================

    void DisplayHappyRoomDesc(Room playerLocation, Monster monster)
    {
      if (playerLocation == monster.location)
      {
        Console.WriteLine(MAID_WILL_CLEAN);
      }
      else
      {
        Console.WriteLine("{0}{1}", FEELS_DIFFERENT, happyRoomDesc);
      }
    }




    // ==========================================================================================================
    // == Function: "PutItem"
    // == 
    // == Description:  Places the item that was passed into the function into the Room, by assigning it to the
    // ==               related "item" attribute in the Room.
    // == 
    // == Invoked by       :  "Person::PlayerDropItem"
    //                        "TextAdventure::PlayerTriesToAttack" (to put the "DeadMonster" object in the room
    // ==                     if the monster dies)
    // ==
    // == Functions called :  <none>
    // == 
    // == Input : *itemToPut -- pointer to the player (an instance of the "Person" object).
    // == Output: <none>
    // ==
    // ==========================================================================================================

    public void PutItem(Item itemToPut)
    {
      if (item == null)
      {
        item = itemToPut;
      }
    }

  }

}
