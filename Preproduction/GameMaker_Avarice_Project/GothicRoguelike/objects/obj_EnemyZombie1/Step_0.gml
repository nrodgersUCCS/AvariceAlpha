// Set state
if (object_exists(obj_dummy) && distance_to_object(obj_dummy) <= attackRadius)
	state = "attacking"
else
	state = "idle"
	
// Attack behavior
if (state == "attacking" && canJump && object_exists(obj_dummy))
{
	// Get random jump sequence
	var jumpType = irandom_range(1, 3)
	var directionFromPlayer = point_direction(obj_dummy.x, obj_dummy.y, x, y)
	
	// Jump around player
	if (jumpType < 3)
	{
		// Clockwise
		if(jumpType == 1) directionFromPlayer += 45;
		
		// Counter-clockwise
		else directionFromPlayer -= 45;
		
		// Find jump point
		targetX = obj_dummy.x + lengthdir_x(attackRadius*0.7, directionFromPlayer);
		targetY = obj_dummy.y + lengthdir_y(attackRadius*0.7, directionFromPlayer);
	}
	
	// Jump towards player
	else
	{
		targetX = obj_dummy.x;
		targetY = obj_dummy.y;
	}
	
	// Start jump
	move_towards_point(targetX, targetY, 3);
	jumping = true;
	canJump = false;
	alarm[1] = room_speed * jumpDelay;
}

// If jumping
if (jumping)
{
	// Check if jump point reached
	if (distance_to_point(targetX, targetY) <= 1)
	{
		x = targetX;
		y = targetY;
		jumping = false;
		speed = 0;
	}
		
}