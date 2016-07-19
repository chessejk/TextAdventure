using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{

  //  ??????????????????    ?????????????????????????


  // Describe the "Item Class" and it's purpose, etc.


  //  ??????????????????    ?????????????????????????


  public class Item
  {
    public string name;
      
  
    // Item constructor 

    public Item(string strName)
    {
      name = strName;
    }

    // ===========================================================================================================
    // == Function: "Item::TryGetItem"
    // == 
    // == Description:  This is the function for the "Item" class, but the subclasses "Weapon" and "DeadMonster" 
    // ==               also have this function.  Those subclasses are used in the game, not the "Item" class,
    // ==               so the subclass versions are actually called not this version of the function.
    // == 

    // declaring a "virtual" method allows any subclass instances to access the subclass version of the method
    // instead of 

    // When a method is declared virtual on a class, pointers to that class type can call the method, treating
    //  the object like an instance of the base class but at run-time, the derived class's implementation of
    //  the virtual method will be called.

    // == Invoked by :  <none>
    // == Input      : <none>
    // == Output     : returns a boolean value that indicates if the player successfully got the item or not.
    // ==
    // ===========================================================================================================

    virtual public bool TryGetItem()
    { 
      return true;
    }

  };

}
