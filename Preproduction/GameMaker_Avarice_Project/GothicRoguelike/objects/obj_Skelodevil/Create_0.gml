/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 06DC29C4
/// @DnDArgument : "code" "attackDamage = 10;$(13_10)if(room = CastleDemo)$(13_10){$(13_10)	bonus = 1 + obj_Controller.castleCompletion*.4;$(13_10)}$(13_10)if(room = InfestedDemo)$(13_10){$(13_10)	bonus = 1 + obj_Controller.infestedCompletion*.4;$(13_10)}$(13_10)$(13_10)if(room = DemonDemo)$(13_10){$(13_10)	bonus = 1 + obj_Controller.infestedCompletion*.4;$(13_10)}$(13_10)$(13_10)monsterHealth = 6;$(13_10)$(13_10)canBeHit = true;$(13_10)$(13_10)//Presets the chance of item drops$(13_10)armorChance = 1;$(13_10)helmetChance = 1;$(13_10)swordChance = 1;$(13_10)greatswordChance = 1;$(13_10)$(13_10)//Creates odds easily.$(13_10)lootChance = armorChance + helmetChance + swordChance + greatswordChance;$(13_10)$(13_10)//Gives enemy pathing$(13_10)path = path_add();$(13_10)$(13_10)// Attack cooldown$(13_10)playerPosX = 0;$(13_10)playerPosY = 0;$(13_10)attackCooldown = false;$(13_10)$(13_10)// variables to get enemy to patrol while passive$(13_10)patrolingLeft = false;$(13_10)$(13_10)//line of sight variables$(13_10)facing = 1;$(13_10)inView  = ds_list_create();$(13_10)aggro = false;"
attackDamage = 10;
if(room = CastleDemo)
{
	bonus = 1 + obj_Controller.castleCompletion*.4;
}
if(room = InfestedDemo)
{
	bonus = 1 + obj_Controller.infestedCompletion*.4;
}

if(room = DemonDemo)
{
	bonus = 1 + obj_Controller.infestedCompletion*.4;
}

monsterHealth = 6;

canBeHit = true;

//Presets the chance of item drops
armorChance = 1;
helmetChance = 1;
swordChance = 1;
greatswordChance = 1;

//Creates odds easily.
lootChance = armorChance + helmetChance + swordChance + greatswordChance;

//Gives enemy pathing
path = path_add();

// Attack cooldown
playerPosX = 0;
playerPosY = 0;
attackCooldown = false;

// variables to get enemy to patrol while passive
patrolingLeft = false;

//line of sight variables
facing = 1;
inView  = ds_list_create();
aggro = false;