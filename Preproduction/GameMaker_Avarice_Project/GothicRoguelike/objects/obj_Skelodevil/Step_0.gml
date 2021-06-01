//checks to see if player is near
if distance_to_object(obj_dummy)<=48
{
aggro = true
}
//enemy fires attack when they are in range and off
// cooldown.
if(aggro = true && attackCooldown == false)
{
	//look at grid for player
	if(instance_exists(obj_dummy))
	{
		// set cooldown
		attackCooldown = true;
		
		// mark players previous position
		playerPosX = obj_dummy.x;
		playerPosY = obj_dummy.y;
		
		// Fires different pieces depending on health
		if(monsterHealth == 6)
		{
			instance_create_depth(x,y,-1, obj_SkelodevilArm);
			alarm_set(2, 20);
		}
		else if(monsterHealth == 5)
		{
			alarm_set(2, 1);
		}
		else if(monsterHealth == 4)
		{
			alarm_set(3, 1);
		}
		else if(monsterHealth == 3)
		{
			alarm_set(4, 1);
		}
		else if(monsterHealth == 2)
		{
			alarm_set(5, 1);
		}
		else if(monsterHealth == 1)
		{
			alarm_set(6, 1);
		}
	}
}

//determines where enemy is facing
if(direction = 0)
{
facing = 1;	
}
else
{
facing = -1;	
}

//checks to see if the player is in line with the enemy without any walls in the way
//if (collision_line_list(x,y,x+ 100 * facing, y, obj_dummy, true, true, inView ,true))
if (collision_line(x,y,x+ 100 * facing, y, obj_dummy, true, true))
{
	//if(ds_list_find_index(inView, 1) = obj_dummy)
	//{
		aggro = true
	//}
}