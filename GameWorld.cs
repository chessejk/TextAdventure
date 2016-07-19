using System;  // namespace declaration
using TextAdventure;

public enum Direction { north, south, east, west, finalExit };


//  ??????????????????    ?????????????????????????


  // Describe the "Program Class" and it's purpose, etc.


//  ??????????????????    ?????????????????????????


public class GameWorld
{
  public Room  objDiningRoom;
  public Room  objKitchen;
  public Room  objBedroom;
  public Room  objStudy;
  public Room  objTheOutsideWorld;

  public Person      objPlayer;

  public Monster     objVampire;

  public Weapon      objSword;
  public DeadMonster objDeadVampire;

  public bool        playerWonGame;


  // ==================================================================================================
  // == Function: "Main"
  // == 
  // == Description:  
  // ==               
  // ==
  // == Invoked by       : "???????????????" 
  // == Functions called : "?????????????????????????????????????"
  // ==
  // == Input : <none>
  // == Output: <none>
  // ==
  // ==================================================================================================

  static void Main()
  {
    string Cmd           = "";
    bool   gameActive    = true;

    GameWorld gameWorld = new GameWorld();


    gameWorld.Initialize();


    // --------------------------------------------------------
    // -- THE GAME LOOP
    // --------------------------------------------------------

    while (gameActive)
    {
      Console.Write("What would you like to do? ");

      Cmd = gameWorld.PopulateCommands();
      gameWorld.Update(ref gameActive, ref Cmd);
    }


    gameWorld.Shutdown();

  }




  // ======================================================================================
  // ==  "Program Class" Constructor
  // ======================================================================================
  
  public GameWorld()
  {
    // -- Create an object for each "room" using the class of "Room"

    objDiningRoom      = new Room("Dining Room", "You can easily imagine laughter and good food here with friends and family.");
    objKitchen         = new Room("Kitchen"    , "You hear the uplifting sound of birds chirping outside.");
    objBedroom         = new Room("Bedroom"    , "You now feel very relaxed in this room.");
    objStudy           = new Room("Study"      , "Everything in the room looks shiny and new.");

    objTheOutsideWorld = new Room("A wooden door with scenes of nature hand-carved into it", " ");

    // -- Set the starting location of the person playing the game to "the Dining Room".
        
    objPlayer = new Person(objDiningRoom);


    // -- Create an object for the person who's playing the game, and set their starting
    // -- location to "the Dining Room".

    objVampire = new Monster("vampire", objBedroom);

    // -- Create an object for the sword, and the dead vampire.  The dead vampire cannot be moved.
    // -- both are subclasses of the "Item" class.

    objSword       = new Weapon("sword");
    objDeadVampire = new DeadMonster("dead vampire");

    playerWonGame  = false;

  }  // End "GameWorld Class" Constructor




  // ==================================================================================================
  // == Function: "InitializeGame"
  // == 
  // == Description:  Initializes where the player can go from each room, where the sword is located,
  // ==               displays an intro message, and tells the player what room they are starting in.
  // ==
  // == Invoked by       : "Main" 
  // == Functions called : "DisplayIntroMsg"
  // ==
  // == Input : <none>
  // == Output: <none>
  // ==
  // ==================================================================================================

  void Initialize()
  {
    Console.SetWindowPosition(0, 0);
    Console.SetWindowSize(130, 30);
    
    // Define where player can go from each room.  If a direction is not defined it defaults to no connection (null).

    objDiningRoom.east  = objKitchen;
    objDiningRoom.south = objBedroom;

    objKitchen.west     = objDiningRoom;
    objKitchen.south    = objStudy;

    objBedroom.north    = objDiningRoom;
    objBedroom.east     = objStudy;

    objStudy.north      = objKitchen;
    objStudy.west       = objBedroom;

    // put the sword in the Study.

    objStudy.item = objSword;

    // Display an introduction message about the player being magically transported into a house.
    DisplayIntroMsg();

    Console.WriteLine("You are currently in the {0}.\n", objPlayer.location.name);

  }  // End "Initialize"




  // ===============================================================================================
  // == Function: "TextAdventure::DisplayIntroMsg"
  // == 
  // == Description:  Displays an intro message to the player with "dots" that slowly appear before
  // ==               and after the message to add suspense...
  // ==
  // == Invoked by       : "TextAdventure::Init" 
  // == Functions called : "clock"
  // ==
  // == Input : <none>
  // == Output: <none>
  // ==
  // ===============================================================================================

  void DisplayIntroMsg()
  {
    DisplayThreeDots();

    Console.Write("a magical mist surrounds you and you are transported into a house");

    DisplayThreeDots();

    Console.WriteLine("\n");

  } // End "TextAdventure::DisplayIntroMsg"




  // ===============================================================================================
  // == Function: "DisplayThreeDots"
  // == 
  // == Description:  Displays three periods in a row with a delay between each period/dot displayed.
  // ==
  // == Invoked by       : "DisplayIntroMsg" 
  // == Functions called : <none>
  // ==
  // == Input : <none>
  // == Output: <none>
  // ==
  // ===============================================================================================

  void DisplayThreeDots()
  {
    for (int i = 0; i < 3; i++)
    {
      System.Threading.Thread.Sleep(700);
      Console.Write(".");
    }
  }




  // ===============================================================================================
  // == Function: "PopulateCommands"
  // == 
  // == Description:  Gets the text entered by the user.  Only the first word entered is currently
  // ==               being used by the game.  In a future version of the game, this will be where
  // ==               the string entered would be parsed into different commands; the first two
  // ==               words will be checked for "verb/action" (word-1), and "noun/object" (word-2)
  // ==               ex: "get sword".  Again, for now only the first word entered is being used.
  // ==
  // == Invoked by       : "Main" 
  // == Functions called : <none>
  // ==
  // == Input : <none>
  // == Output: <none>
  // ==
  // == Returns: Text entered by the user
  // ==
  // ===============================================================================================

  string PopulateCommands()
  {
    return Console.ReadLine();
  }



  // ===============================================================================================
  // == Function: "UpdateGame"
  // == 
  // == Description:  Processes the text entered by the player, and updates the "game state".
  // ==
  // == Invoked by       : "TextAdventure::Run" 
  // ==
  // == Functions called : "objPlayer.OnPlayerMove"            , "DisplayFinalExitText", 
  // ==                    "objPlayer.PlayerGetItem"           , "objPlayer.PlayerDropItem",
  // ==                    "objPlayer.OnPlayerInventory"       , "objPlayer.OnPlayerLookAtRoom",
  // ==                    "TextAdventure::PlayerTriesToAttack", "TextAdventure::DisplayHelp",
  // ==                    "objVampire.TakeTurn"
  // ==
  // == Input : <none>
  // == Output: <none>
  // ==
  // ===============================================================================================

  void Update(ref bool gameActive, ref string Cmd)
  {
    bool playerTurnHappens = true;

    // Process input command from the player
    if (Cmd == "quit" || Cmd == "q" || Cmd == "exit")
    {
      gameActive = false;
    }


    // -------------------------------------------------------------------------------
    // -- below is logic for input from the player related to moving in a direction.
    // -------------------------------------------------------------------------------
    else if (Cmd == "n" || Cmd == "north")
    {
      playerTurnHappens = objPlayer.OnPlayerMove(Direction.north, playerWonGame, objVampire);
    }
    else if (Cmd == "s" || Cmd == "south")
    {
      playerTurnHappens = objPlayer.OnPlayerMove(Direction.south, playerWonGame, objVampire);
    }
    else if ( (Cmd == "e" || Cmd == "east") && (objPlayer.location.finalExit == null) )
    {
      playerTurnHappens = objPlayer.OnPlayerMove(Direction.east, playerWonGame, objVampire);
    }
    else if (Cmd == "w" || Cmd == "west")
    {
      playerTurnHappens = objPlayer.OnPlayerMove(Direction.west, playerWonGame, objVampire);
    }
    else if (playerWonGame && objPlayer.location.finalExit != null && (Cmd == "door" || Cmd == "open" || Cmd == "east" || Cmd == "e"))
    {
      // The player won the game, and the finalExit is in the room the player is in, and they entered "door" or "open".
      playerTurnHappens = objPlayer.OnPlayerMove(Direction.finalExit, playerWonGame, objVampire);

      DisplayFinalExitText();

      gameActive = false;
    }

    else if (Cmd == "get" || Cmd == "g")
    {
      // attempt to get the item by calling the "PlayerGetItem" method; the return value of the method is if action was successful or not.
      if ( ! objPlayer.PlayerGetItem() )
      {
        // the player was unable to get an item, so their turn did not happen.
        playerTurnHappens = false;
      }
    }


    else if (Cmd == "drop" || Cmd == "d")
    {
      // attempt to get the item by calling the "PlayerDropItem" method; the return value of the method is if action was successful or not.
      if ( ! objPlayer.PlayerDropItem(objVampire) )
      {
        // the player was unable to drop an item, so their turn did not happen.
        playerTurnHappens = false;
      }
    }


    else if (Cmd == "i" || Cmd == "inv" || Cmd == "inventory")
    {
      objPlayer.OnPlayerInventory();
      playerTurnHappens = false;
    }


    else if (Cmd == "look" || Cmd == "l")
    {
      objPlayer.OnPlayerLookAtRoom(playerWonGame, objVampire);
      playerTurnHappens = false;
    }


    else if (Cmd == "attack" || Cmd == "a")
    {
      // try to attack a monster in the room by calling the "PlayerTriesToAttack" method; the method will return if action was successful or not.
      if ( ! PlayerTriesToAttack() )
      {
        // there was nothing for the player to attack, so their turn did not happen.
        playerTurnHappens = false;
      }
    }

    else if (Cmd == "help" || Cmd == "h" || Cmd == "?")
    {
      DisplayHelp();
      playerTurnHappens = false;
    }

    else
    {
      // user did not enter valid input
      Console.WriteLine("\"{0}\" is not a valid entry.  Enter 'help' for a list of valid commands.\n", Cmd);
      playerTurnHappens = false;
    }


    if (playerTurnHappens && gameActive == true && objVampire.IsDead() == false)
    {
      objVampire.TakeTurn(objPlayer);
    }


    if (objPlayer.IsDead())
    {
      Console.WriteLine("The {0} delivers a fatal blow, and you die.  You fought bravely, but it was not enough.", objVampire.name);
      gameActive = false;
    }

    Console.WriteLine();

  }  // End "Update"




  // ======================================================================================================
  // == Function: "TextAdventure::PlayerTriesToAttack"
  // == 
  // == Description:  Logic related to when the player tries to attack a monster.  Checks to make sure
  // ==               there is a monster at the same location as the player, and that the player has a
  // ==               weapon.
  // ==
  // == Invoked by       : "TextAdventure::Update" 
  // == Functions called : "objVampire.IsDead", "objPlayer.Attack", "Room::PutItem"
  // ==
  // == Input : <none>
  // ==
  // == Output: returns a boolean value that indicates if the player successfully attacked something or
  // ==         (for whatever reason) was unable to attack something.
  // ==
  // =====================================================================================================

  bool PlayerTriesToAttack()
  {
    bool playerAttackedMonster;

    playerAttackedMonster = true;

    if ((objPlayer.location == objVampire.location) && (objVampire.IsDead() == false))
    {
      if (objPlayer.weapon == null)
      {
        Console.WriteLine("You attack the vampire with your bare hands, but it only laughs at you.");
        Console.WriteLine("Maybe there's something in one of these rooms that would work better...");
      }
      else
      {
        Console.WriteLine("You attack the {0} ", objVampire.name);
        objPlayer.Attack(objVampire);

        if (objVampire.IsDead())
        {
          playerWonGame = true;
          
          // The player has won the game, so create an actual exit from the house just for fun.
          objKitchen.finalExit = objTheOutsideWorld;

          Console.WriteLine("You killed the {0}!\n", objVampire.name);

          Console.WriteLine("* * * YOU WON THE GAME * * *\n");
          Console.WriteLine("You clean your sword as you reflect upon your epic adventure... \n");
          Console.WriteLine("Even though it felt like your suffering continued without end, the dark presence in the house is finally gone.  Your mind suddenly clears, and a sense of peace and unconditional joy descends upon you.\n");

          // Don't set the monster location to NULL because it's later used as a cross-reference to know
          // that the item in the room is the dead monster.  The player is not allowed to drop the sword in the same room as the dead monster...
          //
          //objVampire.location = NULL;

          // Put the dead monster object in the location that the monster was killed.
          objPlayer.location.PutItem(objDeadVampire);
        }
      }
    }
    else
    {
      if (objVampire.IsDead())
      { Console.WriteLine("You want to attack something?!?  But the house seems so peaceful now that the {0} has been defeated.", objVampire.name); }
      else
      { Console.WriteLine("There's nothing for you to attack here.  Go find a monster..."); }

      playerAttackedMonster = false;
    }

    return playerAttackedMonster;

  }  // End "TextAdventure::PlayerTriesToAttack"




  // ===============================================================================================
  // == Function: "TextAdventure::DisplayHelp"
  // == 
  // == Description:  Displays a list of valid commands that the player is allowed to enter.
  // ==
  // == Invoked by       : "TextAdventure::Update" 
  // == Functions called : <none>
  // ==
  // == Input : <none>
  // == Output: <none>
  // ==
  // ===============================================================================================

  void DisplayHelp()
  {
    Console.WriteLine();
    Console.WriteLine("List of valid commands: \n");
    Console.WriteLine("(n)orth, (s)outh, (e)ast, (w)est");
    Console.WriteLine("(g)et");
    Console.WriteLine("(d)rop");
    Console.WriteLine("(l)ook");
    Console.WriteLine("(a)ttack");
    Console.WriteLine("(i)nventory");
    Console.WriteLine("(h)elp (?)");
    Console.WriteLine("(q)uit\n");
  }




  // ====================================================================================================
  // == Function: "TextAdventure::DisplayFinalExitText"
  // == 
  // == Description:  Displays text when the player wins the game __and__ finds a way out of the house.
  // ==
  // == Invoked by       : "TextAdventure::Update" 
  // == Functions called : <none>
  // ==
  // == Input : <none>
  // == Output: <none>
  // ==
  // ====================================================================================================

  void DisplayFinalExitText()
  {
    Console.WriteLine("\nYou found a way out of the house!\n");

    Console.WriteLine("You step outside into the sunlight, take a deep breath of fresh air, and begin your next adventure...\n");
  }




  // ===============================================================================================
  // == Function: "ShutdownGame"
  // == 
  // == Description:  Displays a generic message when the game is over (independent of how it ends)
  // ==               and then displayes a message to have the player press any key to before the
  // ==               program/game stops.
  // ==
  // == Invoked by       : "Main" 
  // == Functions called : <none>
  // ==
  // == Input : <none>
  // == Output: <none>
  // ==
  // ===============================================================================================

  void Shutdown()
  {
    Console.WriteLine("\nThank you for playing.  Goodbye.\n\n\n");
    
    Console.Write("Press any key to continue  . . .");

    while (Console.KeyAvailable == false)
    {
      // Do nothing, and just loop until user presses any key in which case we end the loop.
    }
    
  }

}  // End "GameWorld" Class