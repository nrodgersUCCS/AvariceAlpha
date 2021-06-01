/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 7424D72B
/// @DnDArgument : "code" "// Moves the player if left or right is being pressed$(13_10)right = keyboard_check(ord("D"));$(13_10)left = keyboard_check(ord("A"));$(13_10)up = keyboard_check(ord("W"));$(13_10)down = keyboard_check(ord("S"));$(13_10)$(13_10)// Sets move speed if buttons are pressed$(13_10)if(global.isArmored == false){$(13_10)moveSpeedx += (right-left)*moveAccel;$(13_10)moveSpeedy += (down-up)*moveAccel;$(13_10)}$(13_10)$(13_10)if(global.ArmorLight == true){$(13_10)moveSpeedx += ((right-left)*moveAccel)*1.75;$(13_10)moveSpeedy += ((down-up)*moveAccel)*1.75;	$(13_10)	$(13_10)}$(13_10)$(13_10)if(global.ArmorHeavy == true){$(13_10)moveSpeedx += ((right-left)*moveAccel)*.6;$(13_10)moveSpeedy += ((down-up)*moveAccel)*.6;$(13_10)	$(13_10)}$(13_10)$(13_10)if(global.ArmorDemonic == true){$(13_10)moveSpeedx += ((right-left)*moveAccel)*1.5;$(13_10)moveSpeedy += ((down-up)*moveAccel)*1.5;	$(13_10)	$(13_10)}$(13_10)// Only animate the sprite if it's moving$(13_10)if (moveSpeedx = 0 and moveSpeedy == 0)$(13_10){$(13_10)	image_speed = 0;$(13_10)} $(13_10)else $(13_10){$(13_10)	image_speed = 1;$(13_10)}$(13_10)$(13_10)// If the player is moving to the left, flip the sprite$(13_10)if (moveSpeedx < 0)$(13_10){$(13_10)	image_xscale = -1;$(13_10)} $(13_10)else $(13_10){$(13_10)	image_xscale = 1;$(13_10)}$(13_10)$(13_10)// If a speed is not 0, get it closer to 0 using friction$(13_10)if (moveSpeedx > 0)$(13_10){$(13_10)	moveSpeedx -= moveFric;$(13_10)}$(13_10)if (moveSpeedx < 0)$(13_10){$(13_10)	moveSpeedx += moveFric;$(13_10)}$(13_10)if (moveSpeedy > 0)$(13_10){$(13_10)	moveSpeedy -= moveFric;$(13_10)}$(13_10)if (moveSpeedy < 0)$(13_10){$(13_10)	moveSpeedy += moveFric;$(13_10)}$(13_10)$(13_10)// Manage collision$(13_10)if (place_meeting(x+moveSpeedx, y, obj_Wall))$(13_10){$(13_10)	while (!place_meeting(x+sign(moveSpeedx),y,obj_Wall))$(13_10)	{$(13_10)		x += sign(moveSpeedx);$(13_10)	}$(13_10)	moveSpeedx = 0;$(13_10)}$(13_10)$(13_10)if (place_meeting(x, y+moveSpeedy, obj_Wall))$(13_10){$(13_10)	while (!place_meeting(x,y+sign(moveSpeedy),obj_Wall))$(13_10)	{$(13_10)		y += sign(moveSpeedy);$(13_10)	}$(13_10)	moveSpeedy = 0;$(13_10)}$(13_10)$(13_10)// Moves the object$(13_10)x += moveSpeedx;$(13_10)y += moveSpeedy;$(13_10)$(13_10)// Caps the speed in the cardinal dirrections$(13_10)if (moveSpeedx > maxSpeed)$(13_10){$(13_10)	moveSpeedx = maxSpeed;$(13_10)}$(13_10)if (moveSpeedx < -maxSpeed)$(13_10){$(13_10)	moveSpeedx = -maxSpeed;$(13_10)}$(13_10)if (moveSpeedy > maxSpeed)$(13_10){$(13_10)	moveSpeedy = maxSpeed;$(13_10)}$(13_10)if (moveSpeedy < -maxSpeed)$(13_10){$(13_10)	moveSpeedy = -maxSpeed;$(13_10)}$(13_10)$(13_10)//Equips Armor if they have one extra and lose the one they're carrying.$(13_10)if(!global.isArmored && global.armorCarried >0){$(13_10)	global.isArmored = true;$(13_10)	global.armorCarried--;$(13_10)}$(13_10)//Equips Helmet if they have one extra and lose the one they're carrying.$(13_10)if(!global.hasHelmet && global.helmetsCarried >0){$(13_10)	global.hasHelmet = true;$(13_10)	global.helmetsCarried--;$(13_10)}$(13_10)//Equips Sword if they have one extra and lose the one they're carrying.$(13_10)if(!global.hasSword && global.swordCarried >0){$(13_10)	global.hasSword = true;$(13_10)	global.swordCarried--;$(13_10)$(13_10)}$(13_10)//Equips GreatSword if they have one extra and lose the one they're carrying.$(13_10)if(!global.hasGreatSword && global.greatswordsCarried >0){$(13_10)	global.hasGreatSword = true;$(13_10)	global.greatswordsCarried--;$(13_10)}"
// Moves the player if left or right is being pressed
right = keyboard_check(ord("D"));
left = keyboard_check(ord("A"));
up = keyboard_check(ord("W"));
down = keyboard_check(ord("S"));

// Sets move speed if buttons are pressed
if(global.isArmored == false){
moveSpeedx += (right-left)*moveAccel;
moveSpeedy += (down-up)*moveAccel;
}

if(global.ArmorLight == true){
moveSpeedx += ((right-left)*moveAccel)*1.75;
moveSpeedy += ((down-up)*moveAccel)*1.75;	
	
}

if(global.ArmorHeavy == true){
moveSpeedx += ((right-left)*moveAccel)*.6;
moveSpeedy += ((down-up)*moveAccel)*.6;
	
}

if(global.ArmorDemonic == true){
moveSpeedx += ((right-left)*moveAccel)*1.5;
moveSpeedy += ((down-up)*moveAccel)*1.5;	
	
}
// Only animate the sprite if it's moving
if (moveSpeedx = 0 and moveSpeedy == 0)
{
	image_speed = 0;
} 
else 
{
	image_speed = 1;
}

// If the player is moving to the left, flip the sprite
if (moveSpeedx < 0)
{
	image_xscale = -1;
} 
else 
{
	image_xscale = 1;
}

// If a speed is not 0, get it closer to 0 using friction
if (moveSpeedx > 0)
{
	moveSpeedx -= moveFric;
}
if (moveSpeedx < 0)
{
	moveSpeedx += moveFric;
}
if (moveSpeedy > 0)
{
	moveSpeedy -= moveFric;
}
if (moveSpeedy < 0)
{
	moveSpeedy += moveFric;
}

// Manage collision
if (place_meeting(x+moveSpeedx, y, obj_Wall))
{
	while (!place_meeting(x+sign(moveSpeedx),y,obj_Wall))
	{
		x += sign(moveSpeedx);
	}
	moveSpeedx = 0;
}

if (place_meeting(x, y+moveSpeedy, obj_Wall))
{
	while (!place_meeting(x,y+sign(moveSpeedy),obj_Wall))
	{
		y += sign(moveSpeedy);
	}
	moveSpeedy = 0;
}

// Moves the object
x += moveSpeedx;
y += moveSpeedy;

// Caps the speed in the cardinal dirrections
if (moveSpeedx > maxSpeed)
{
	moveSpeedx = maxSpeed;
}
if (moveSpeedx < -maxSpeed)
{
	moveSpeedx = -maxSpeed;
}
if (moveSpeedy > maxSpeed)
{
	moveSpeedy = maxSpeed;
}
if (moveSpeedy < -maxSpeed)
{
	moveSpeedy = -maxSpeed;
}

//Equips Armor if they have one extra and lose the one they're carrying.
if(!global.isArmored && global.armorCarried >0){
	global.isArmored = true;
	global.armorCarried--;
}
//Equips Helmet if they have one extra and lose the one they're carrying.
if(!global.hasHelmet && global.helmetsCarried >0){
	global.hasHelmet = true;
	global.helmetsCarried--;
}
//Equips Sword if they have one extra and lose the one they're carrying.
if(!global.hasSword && global.swordCarried >0){
	global.hasSword = true;
	global.swordCarried--;

}
//Equips GreatSword if they have one extra and lose the one they're carrying.
if(!global.hasGreatSword && global.greatswordsCarried >0){
	global.hasGreatSword = true;
	global.greatswordsCarried--;
}