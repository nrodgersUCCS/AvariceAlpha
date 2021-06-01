/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 6424D588
/// @DnDArgument : "code" "var bonus = 0;$(13_10)if(room = CastleDemo)$(13_10){$(13_10)	bonus = 1 + obj_Controller.castleCompletion*.4;$(13_10)}$(13_10)else if(room = InfestedDemo)$(13_10){$(13_10)	bonus = 1 + obj_Controller.infestedCompletion*.4;$(13_10)}$(13_10)$(13_10)else if(room = DemonDemo)$(13_10){$(13_10)	bonus = 1 + obj_Controller.infestedCompletion*.4;$(13_10)}$(13_10)$(13_10)enemyHealth *= bonus;"
var bonus = 0;
if(room = CastleDemo)
{
	bonus = 1 + obj_Controller.castleCompletion*.4;
}
else if(room = InfestedDemo)
{
	bonus = 1 + obj_Controller.infestedCompletion*.4;
}

else if(room = DemonDemo)
{
	bonus = 1 + obj_Controller.infestedCompletion*.4;
}

enemyHealth *= bonus;