extends Sprite2D

var picked : bool = false

func _process(delta):
	# Mouse button requirement should be removed in final version
	if _is_mouse_over() and !picked:
		picked = true;
		var anim = $AnimationPlayer.get_animation("PickUp")
		anim.track_set_key_value(0,0,self.position)
		$AnimationPlayer.play("PickUp")

func _is_mouse_over() -> bool:
	var mouse_position = get_global_mouse_position()
	return get_rect().has_point(to_local(mouse_position))


func _on_animation_player_animation_finished(anim_name):
	get_tree().root.get_child(0).AddCurrency(1)
	self.queue_free()
