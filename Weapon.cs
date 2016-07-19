using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{

  //  ??????????????????    ?????????????????????????


  // Describe the "Weapon Class" and it's purpose, etc.


  //  ??????????????????    ?????????????????????????


  public class Weapon : Item
  {

    // Weapon constructor

    public Weapon(string strName) : base(strName)
    {

    }




    // ========================================================================================================
    // == Function: "Weapon::TryGetItem"
    // == 
    // == Description:  Displays message about getting the item including the customized name of the item.
    // == 
    // == Invoked by       :  "Person::OnPlayerGetItem"
    // == Functions called :  <none>
    // == 
    // == Input : <none>
    // == Output: returns a boolean value that the player successfully got the item.
    // ==
    // ========================================================================================================

    override public bool TryGetItem()
    {
      Console.WriteLine("You get the {0}.", name);

      return true;
    }



  }  // End "class Weapon"

}
