// Call parent event
event_inherited()

// Stun if mid-jump
if (jumping)
{
	// Stop jumping
	jumping = false;
	speed = 0;
	
	// Stun
	canJump = false;
	alarm[1] = room_speed * stunDuration;
}