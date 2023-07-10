# Recycloid
This is a project done for my thesis/capstone related to gamification. The "R&D" for this took about six months. I mostly did the work in both research and developing/coding.
After finishing both thesis and practicum courses, I am now able to come back to this and change it bit-by-bit (although less often than I hope to). 
I get help from some of my friends in coming up with ideas for this now that the time constraint and restrictions for it's development has been lifted.
I am self-taught and lacking in A LOT of areas so I wish to learn more about game development as I progress with this project.


# More About the Project
All projects files have been removed indefinitely for polishing as this repository serves as my portfolio for job applications.
I plan on only updating the apk (and possibly build an exe) file/s in the future. I'll leave multiple apks in here as a way to see how to project progressed over time.

The README file will also serve as a changelog (maybe temporarily), so the most recent changes to the project will be written near the top and further down
will show previous changes.

For Recycloid_v2023.07.10, the game has been, for the lack of a better term, reverted to an unfinished state (though it doesn't feel finished to begin with, even after the thesis ended).
I want to improve the project to make it feel more like a game rather than a prototype.


# What's Changed/Working? (07/10/2023)
### Settings
  I've just recently noticed that the settings menu doesn't do anything (I swear it was working before, cross my heart). So upon looking into it, I face-palmed and quickly got it to work
  (including saving the data in playerprefs which is the most basic thing I've overlooked during thesis).
### "The Park"
  With this one I've disabled 3 out of 4 levels and made a huge change in its design. It doesn't look and feel as big and empty as before. Not bad for someone who's dabbling a bit into
  design if I do say so myself. Although this is just for "level navigation", I do have plans for it which is evident in the unchanged dialogue when you first run the game.
### "The House"
  I've stripped(just disabled) of it all the furniture so now it's just a room with 3 waste bins inside and a suspicious looking bubble in the middle. I wanted to focus on the gameplay
  for the time being. Just like with "The Park", I intend to improve its design.
### Gameplay
  After talking to a friend, we've came up with a few things to hopefully make the game a bit more engaging:
  - There are now 3 power-ups: speed boost, speed debuff, and add time. Only the first 2 are put into the game as I'm deciding whether to have it spawn in its own interval.
  - On the subject of spawning at intervals, waste items now spawn at different rates depending on the time remaining, while the power-ups spawn randomly between 5-9sec intervals
    splitting 50-50 chance of spawning.
  - The waste items don't have a glow to them but now show a name tag at the bottom of the screen so that players have to figure out which trash belong to which category of bin.


# Future Changes
### Scoring and Score Saving
I'm not sure how to go about with the game's scoring, I just feel it needs a bit more "depth". And with score saving right now, 
I have been saving scores using playerprefs which is not the optimal way to save data other than game settings. I want to learn how to do custom save files.

### Levels
I will redesign all 4 levels of the game so that each will have some form of uniqueness not just with the layout, but also with the types of items they spawn and obstacles placed in them.
I am not a designer so the goal for now is to at least make the other levels accessible with just the layout. "The Park" will also see just a little bit of change since I'm happy with the current area.

### UI Overhaul
The problem I have with this is that I have no theme in mind when I designed this. I also have no art skills to speak of, however I want to learn more about design. Either that or I get some else to work on this 
which is unlikely.

### Audio
I'm not too happy with the audio so yeah... There will be changes

### Game Modes (in a distant future)
##### "VS AI" mode
  - The player will be assigned to a random waste bin category and can only score with said category while battling 2 bots.
  - Controls, particularly with item pick-up, will remain the same. So the challenge comes from picking the right item strategically.
  - Spawn rate of all items will be higher to create chaos.
  - Looking into being able to inflict debuffs to opponents.
##### Multiplayer mode
  - Same mechanics with the VS AI mode, but against other humans, assuming you have friends... to play with.
  - developing this is going to be much more difficult from a technical perspective, the last thing that might be done.

### Reward System
The time spent on the game should somehow be rewarded right? I will be looking into unlockables, like maybe skins or some other change with the park to tell a little bit of story.


