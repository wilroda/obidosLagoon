# obidosLagoon
A game about the Óbidos Lagoon

# TODO
    
**Quests**
- Quests can be hidden from the quest log (hidden quests)

**Object Click Actions:**
- Need to add conditions for having specific quests or not
- Distance conditions
- Quest completion/fail system
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
        - Obstacle avoidance or not
- Trigger Another Object's Actions:
    - For example, you click on a tree or a rock, and out comes moving a hidden animal.
    - Parameters:
        - GameObject with actions
- Play sound
- Game Over action
  - Quest is "Find Amphybians", you press on a mamal, game over, restart
- Go to another scene action
  - When a quest is done, or if you click on a certain place: not navigation, idea is that whole system is reset after this
- Disappear/Appear/Toggle action
- Give quest action
         
**UI:**
- Mouse over interactable objects, makes the cursor change;
- Mission Accomplished Menu 
- Start Menu
- Level Selection Menu

**Animations:**
- Using scripted tweens that can be added to objects and parameterized.
- Idle Animation (was quickly done but can probably be more streamlined)
    - Parameters:
        - Animation Speed
        - Time Between Scale Interval
        - Min Scale
        - Max Scale
        - Time Between Rotate Interval
        - Min Rotation
        - Max Rotation
- Popup animation for speech bubbles
- Text appears over time, instead of all at once
- Show/Hide quest log with animation

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
