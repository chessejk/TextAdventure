using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{

  //  ??????????????????    ?????????????????????????


  // Describe the "DeadMonster Class" and it's purpose, etc.


  //  ??????????????????    ?????????????????????????
 


  public class DeadMonster : Item
  {
    
    // DeadMonster constructor 

    public DeadMonster(string strName) : base(strName)
    {

    }




    // ===================================================================================================
    // == Function: "TryGetItem"
    // == 
    // == Description:  This function is called when the player tries to get an item in the room.  In
    // ==               this case, the item is a "DeadMonster", and the player is not allowed to get it.
    // == 
    // == Invoked by       :  "Person::PlayerGetItem"
    // == Functions called :  <none>
    // == 
    // == Input : <none>
    // == Output: returns a boolean value that indicates if the player successfully got the item or not.
    // ==
    // ===================================================================================================

    override public bool TryGetItem()
    {
      // For the "DeadMonster" class, the message when you try to get the DeadMonster is always that you can't get it.

      Console.WriteLine("You can't get a {0}.", name);

      return false;
    }

  };

}
