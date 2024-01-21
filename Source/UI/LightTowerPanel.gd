extends Panel


@onready var lightTower = preload("res://Source/Entities/Towers/light_tower.tscn")



func _on_gui_input(event):
	if event is InputEventMouseButton and event.button_mask == 1:
		var tempTower = lightTower.instantiate()
		print("Left Click")
	pass
