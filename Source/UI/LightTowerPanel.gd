extends Panel

var currImageCopy 
@onready var lightTower = preload("res://Source/Entities/Towers/light_tower.tscn")



func _on_gui_input(event):
	var tempTower = lightTower.instantiate()

	if event is InputEventMouseButton and event.button_mask == 1:
		currImageCopy = get_child(0).duplicate()
		currImageCopy.global_position = event.global_position
		get_tree().current_scene.add_child(currImageCopy)
		print("Tower Spawned")
	elif event is InputEventMouseMotion and event.button_mask == 1:
		#Dragging
		currImageCopy.global_position = event.global_position
		print("Dragging")
	elif event is InputEventMouseButton and event.button_mask == 0:
		get_tree().current_scene.add_child(tempTower)
		tempTower.global_position = event.global_position
		currImageCopy.queue_free()

		print("Released")
		
		
