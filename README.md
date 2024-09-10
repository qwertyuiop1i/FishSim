Play on Itch.io: https://qwertyuiop13.itch.io/fish-boids-experiment
Boids demo experiment:
Fish swim around looking for food, if they are unable to get food in time, they will die. If they are able to eat a surplus of food, they will reproduce asexually, and the offspring can get mutations of it's parents traits.
Everytime a fish reproduces, the following traits can be mutated:
CohesionWeight: the importance that the fish places on staying near other fish.
AlignmentWeight: the importance that the fish places on matching the direction of the fish nearby.
SeparationWeight: the importance that the fish places on maintaining distance from it's own species.
otherSpeciesSeperationWeight: the importance that the fish places on avoiding other species(More on this later).
WanderStrength: the importance that the fish plays on randomly exploring it's world.
FoodWanderStrength: the importance that the fish plays on going towards food.

There are two buttons, and a slider present in the game. 
The slider controls the speed of the game(0.2x-20x). The 'greyfish' button spawns in a 'grey'fish that mostly priotitizes staying in a school of fish, while seeking out food. The 'redfish' button spawns in a redfish that focuses on AVOIDING other fish, and focuses on seeking out food.

In the corner, there is a green graph, this graph shows how the average FoodWanderStrength of the greyfish changes over time. The redfish's traits over time is NOT considered in this graph.
Also note that the more red a fish is, the more it prioritizes FoodWanderStrength over the other weights.
You can click on a fish to toggle camera follow.
![image](https://github.com/user-attachments/assets/07ecc8bf-dd7f-4316-a60e-78f56e7af67a)
