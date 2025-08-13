- Shoot - LeftClick. 
- MeleeAttack - RightClick 
- ChangeWeapon - Scroll 
- Dash - LeftShift 
- Movement - WASD or Arrows 
- Q - Throw Bomb


A Rogue Like game concept I made during my High Technician in Multi-platform Application Development course. This project include so many concepts of a Rogue Like as:

- A spawner Game Object who include :

1.  A script that generates the different rooms. The method of generating rooms is performed by saving the rooms in prefabs and saving on a List array seven random generated rooms. This rooms will be created on the scene when the player reach to one of them. Each room is generated a hundred units far from the other one. 

2. An enemy spawner script with a parameter object "Wave" who contains the number of enemies of two types "Explosive Enemy" and "Turret Enemy", the number of the wave, a Bool that check if the wave is defeated and a method tho CallTheWave that return the object itself with a random factor of extra enemies. 

The EnemySpawner scripts creates a number of Wave objects equivalent to the number of rooms tagged "combat". Each time a room with tag "combat" is created, it calls the spawner to instance the wave by number order.  The EnemySpawner with another method looks unit x unity using ray cast and layer mask all the possible squares that an enemy can spawn by the condition of don't being to close of the door where the Player spawns or don't being in another object position. 

When all enemies are defeated, the Game Manager notifies it with an "Open Door" event. 

- All Rooms have two doors, each door appear depending on the orders of the spawner. If a Room is on the start, the first door will disappear, if the door is at the end, the second door will do it instead. Passing through a door, will make the player travel to the door before if is the first door or after if is the second door.  Each door closes when a room is a "combat" one, but if there are no enemies an event called OpenDoors shows the doors GameObject, allowing the Player to travel trough them. 

- A State Machine made with scriptable objects. Each Scriptable Object is a state who contains the Scriptable Object Action, with the methods for the start, the update and the end and the Scriptable States to transition.  The conditions are controlled in the Script of the Game Object performing the States, and the Game Object only can do one state at a time. Create an inheritance is crucial for this method to work, because a Script has a method who allows the state to transition, and it controls if this next state is one that is able to transition, and important, to call the onStart, onUpdate and onFinish to perform the action.  This state machine was in use in the Player and Enemies. 

- A Weapon System made with Scriptable Objects who allows the PlayerController to don't look the type of weapon he is holding and just execute the action "Attack 1" and "Attack 2".  Each Scriptable object contains the Game Object of a weapon, who instantiate as a child of a Weapon Holder in player hands. Each weapon have a script inheritance from "WeaponController" who controls the two types of attack of each weapon. Normally a "Melee one" or a "Range one" but could be of any type. Similar methods are included in the Scriptable Object to share it with other weapons, making easier the creation of new weapons. 

- Finally, use of inheritance such as: StateMachineController > Character > Player/Enemy and from Enemy Turret and Kamikaze or ItemController > AnyType of Item and use of interfaces such as IDestroyable for enemies and boxes, so when they are destroyed it have a chance of dropping an item. 

Try to hit the proyectiles and return it to enemies ;)
