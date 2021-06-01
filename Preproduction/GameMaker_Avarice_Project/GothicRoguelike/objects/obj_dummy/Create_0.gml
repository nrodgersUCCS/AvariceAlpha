/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 6B478B5D
/// @DnDArgument : "code" "// Create variables used in movement$(13_10)right = keyboard_check(ord("D"));$(13_10)left = keyboard_check(ord("A"));$(13_10)up = keyboard_check(ord("W"));$(13_10)down = keyboard_check(ord("S"));$(13_10)moveSpeedx = 0;$(13_10)moveSpeedy = 0;$(13_10)moveAccel = .30;$(13_10)moveFric = .15;$(13_10)maxSpeed = 1.5;$(13_10)$(13_10)canAttack = true;$(13_10)facingx = 0;$(13_10)facingy = 0;$(13_10)xdistance = 0;$(13_10)ydistance = 0;$(13_10)$(13_10)attacking = false;$(13_10)sword = 0;$(13_10)$(13_10)movedDown = false;$(13_10)movedUp = false;"
// Create variables used in movement
right = keyboard_check(ord("D"));
left = keyboard_check(ord("A"));
up = keyboard_check(ord("W"));
down = keyboard_check(ord("S"));
moveSpeedx = 0;
moveSpeedy = 0;
moveAccel = .30;
moveFric = .15;
maxSpeed = 1.5;

canAttack = true;
facingx = 0;
facingy = 0;
xdistance = 0;
ydistance = 0;

attacking = false;
sword = 0;

movedDown = false;
movedUp = false;