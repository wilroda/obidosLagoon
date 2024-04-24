# obidosLagoon
A game about the Óbidos Lagoon

# TODO

**Level structure:**
- A scriptable object that contains the objects/animals to be found
    - Data to be retrieved from this structure:
        - How many things to find to complete a level, to populate the quest log or other UI thingies.
        - The name of those things to populate the quest log.
    
**Object Click Actions:**
- Modular system where each action is a sperate script that can be added to an object, this way objects can have various actions fire at the same time. For example I click on an animal to "find" it and at the same time it displays some dialogue.

- Actions:
    - Find Object - Logic that triggers when and object is supposed to be found and count towards the quest objectives. Done but probably needs to be reworked to play nicely with a more systematic approach?
        - Parameters to add:
            - Secret - objects that can be found but aren't necesseary to completet he mission.
      
    - Dialogue
        - On click a speech bubble appears and stays visible for some time.
            - Parameters:
                - SpeechSource - Transform where the speech bubble points to
                - Text limited to 280 characters (tweet style)
                - Time it says visible
                - POLISH/NICE TO HAVE - Speech bubble pop uup/down animation
                - POLISH/NICE TO HAVE - Speech appears word by word
             
    - Movement:
        - On click the object starts/stops moving, on a pre-determined path (waypoints or bezier curves?)
        - Parameters:
            - Is Moving - start the game already moving
            - Speed
            - Stopping Time - while moving, when the object is clicked and stops, how many seconds should it stay still before start to move again
            - Movement Type:
                - If we can only do 1, maybe the path is the most versatile, because it can be used for flying, underwater, and ground animals.
                - Path - using bezier curves
                - POLISH/NICE TO HAVE:
                    - Waypoints - list of waypoints
                    - Randomly - go forward and change direction from time to time
  
    - Trigger Another Object's Actions:
        - For example, you click on a tree or a rock, and out comes moving a hidden animal.
        - Parameters:
            - GameObject with actions
         
**UI:**
- Mouse over interactable objects, makes the cursor change;
- Mission Accomplished Menu 
- Start Menu
- Level Selection Menu

**Animations:**
- Using scripted tweens that can be added to objects and parameterized.
- Idle Animation (was quickly done but can probably me more streamlined)
    - Parameters:
        - Animation Speed
        - Time Between Scale Interval
        - Min Scale
        - Max Scale
        - Time Between Rotate Interval
        - Min Rotation
        - Max Rotation

**Sound:**
- Level ambient sounds
- Object find sound
- Object click sound - sound the object makes on click
- Object sound - a sound that the object emits from time to time?
- Level Complete fanfare
- Menu Music
- Menu Clicks

**Assets:**
- Organize asset list
- Setup easy to use prefabs from the current asset list
- Add human characters
