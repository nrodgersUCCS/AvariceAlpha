// if player is near enemy then set stance to attacking and
// begin shooting arrows.
if distance_to_object(obj_dummy)<=100{
	if(attack == false){
		alarm[0] = 80;
		attack = true;
	}
}

// if attacking then either find a barrack or keep distance
if(attack = true)
{
	// checks if with in range of barrack and if it exists
	if(instance_exists(obj_barrack) && distance_to_object(obj_barrack)<=50)
	{
			// path to barrack
			mp_potential_step(obj_barrack.x,obj_barrack.y,1,false);
	}
	// otherwise keep distance from player
	else if distance_to_object(obj_dummy)<=50
	{
		// keep distance in x direction
		if(x > obj_dummy.x)
		{
			mp_potential_step(obj_dummy.x + 50, y, 0.5, false);
		}
		else if(x < obj_dummy.x)
		{
			mp_potential_step(obj_dummy.x - 50, y, 0.5, false);
		}
		// keep distance in y direction
		if(y > obj_dummy.y)
		{
			mp_potential_step(x, obj_dummy.y + 50, 0.5, false);
		}
		else if(y < obj_dummy.y)
		{
			mp_potential_step(x, obj_dummy.y - 50, 0.5, false);
		}
	}
}