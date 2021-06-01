// If enemy can be attacked
if (canBeHit)
{
	// Get attack instance
	var inst = instance_place(x, y, obj_Attack)
	// Play damage sound
	audio_play_sound(damageSound, -100, false);
	
	// Start invincibility frames
	canBeHit = false;
	alarm_set(0, numIFrames)

	// Check if immune
	if (ds_list_find_index(immunities, inst.type) != -1){}

	// Check resistances - half damage
	else if (ds_list_find_index(resistances, inst.type) != -1)
		enemyHealth -= (inst.damage / 2);

	// Check weaknesses - double damage
	else if (ds_list_find_index(weaknesses, inst.type) != -1)
		enemyHealth -= (inst.damage * 2);
	
	// Otherwise do normal damage
	else
		enemyHealth -= inst.damage;
	
	// Check if dead
	if (enemyHealth <= 0)
	{
		instance_destroy()
	}
}