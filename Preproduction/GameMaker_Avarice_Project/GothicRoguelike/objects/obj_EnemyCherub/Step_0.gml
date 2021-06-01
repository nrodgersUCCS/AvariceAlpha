// Set state
if (distance_to_object(obj_dummy) <= radius)
{
	state = "attacking"
}
else if distance_to_object(obj_dummy) <= attackRadius
{
	state = "hunting";	
}
else
{
	state = "idle"	
}

// If attacking player
if(state == "attacking")
{
	
	if(instance_exists(obj_dummy))
	{
		// Shoot fireballs
		if(canAttack)
		{
			instance_create_layer(x, y, "Instances", obj_fireball);
			speed = 0;
			alarm[1] = room_speed * 2;
			canAttack = false;
			canMove = false;
			alarm[3] = room_speed;
		}
		
		// Circle around player
		else if canMove
		{
			theta = point_direction(obj_dummy.x, obj_dummy.y, x, y)
			theta += rotationSpeed;
			if (theta >= 360) theta -= 360;
			x = obj_dummy.x + lengthdir_x(radius, theta)
			y = obj_dummy.y + lengthdir_y(radius, theta)
		}
		
	}
}

// Chase palyer
else if (state == "hunting")
{
	direction = point_direction(x, y, obj_dummy.x, obj_dummy.y);
	if (canMove) speed = 1;
	
}

// Wander around
else
{
	if (canMove) speed = 0.25;
	if (canChangeDirection)
	{
		direction = random_range(0, 360);
		canChangeDirection = false;
		alarm_set(2, room_speed * 3);
	}
}