/* 

ENEMY CREATION GUIDE FOR FLUFFY
VERSION 0.1

BASIC BEHAVIOR
    An enemy will move back and forth when Junior is not visible. When Junior comes close enough 
    and the enemy is facing him, it will charge at him. It will attack horizontally if it is a 
    non-flying monster, and in a diagonal line if it is flying. Upon colliding with Junior, the
    enemy will be knocked back in the direction it came from. When not attacking or retreating, 
    flying enemies will move up or down to their starting point on the y axis and will continue 
    moving back and forth until Junior is spotted again.

    Every enemy has a certain amount of attack and health when it appears in the game. Any time
    an enemy collides with Junior, it will lose one health unless it is in its "attacking" state.
    The enemy will die if it runs out of health. The enemy will also deal damage to Junior when
    colliding with him during an attack.

THINGS TO ADD/CHANGE
    When colliding with Junior, Enemies should be able to check whether Junior is currently 
    attacking, and only take damage if he is

    Enemies should be able to shoot and take damage from projectiles

    There should be an option for us to make enemies that sit and wait for Junior instead of
    moving back and forth

    Enemies should turn red before dying (or some similar effect)

    There could be an "aggression" variable that affects the odds of an enemy attacking Junior

    Flying enemies could have more precise aim when flying down at Junior

    There is a bug that allows Junior to push enemies outside of their boundaries, causing them
    to go through walls or float in the air

HOW TO MAKE A NEW ENEMY
    Drag an enemy prefab into your scene so it's in the middle of where you want its left-right
    movement range to be. If it's a flying enemy, place it as high off the ground as you want it
    to be when it's looking for Junior. Otherwise, just place it on the ground.

    Make animations for the enemy. (Make sure "Loop Time" is checked for each one.) Duplicate/rename 
    the EnemyAnimator controller and add it to the enemy under "Controller" in the Animator component. 
    Open the controller and add an animation to each state by dragging it onto the state's "Motion" 
    field in the inspector. 

    The enemy has 1 capsule collider and 3 box colliders. If the enemy will fly, enable the large 
    box collider and disable the smaller ones. If the enemy will stay on the ground, enable the two smaller
    ones and disable the large one. Resize the capsule collider (and the large box collider if you use
    it) to fit the enemy's size. Move the smaller colliders so there is one on each side of
    the enemy, with both of them going into the ground. 
    
    Test the enemy's movement. For enemies on the ground, if they are glitchy then try readjusting the box
    colliders. (The enemy will change direction whenever a collider is either totally surrounded by
    wall/ground, or not touching it at all.)

    In the Enemy Script component in the inspector, make sure "Flying" is checked if the enemy can fly.
    Test the game with the enemy and Junior and play around with the other settings. When you're done,
    make a new prefab from the enemy and drag it wherever you want in the scene.


NOTES
    When you first make an enemy, moveSpeed and attackSpeed should be positive numbers (for right movement), 
    and knockbackSpeed should be negative (for left movement). During testing, these values will flip around 
    every time the enemy changes direction, so if you want to test a different value make sure you keep the
    sign the same.

    You probably won't need to mess with the "Max Y Axis Difference" or "Safe Zone" settings for normal-size
    enemies, but might have to change them when creating bosses.
 */
