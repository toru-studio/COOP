extends Node

var refresh_fox : float
var refresh_music1: float

# Called when the node enters the scene tree for the first time.
func _ready():
	refresh_fox = 0.0
	refresh_music1 = 0.0


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	refresh_fox += delta
	refresh_music1 += delta
	if (refresh_fox > 6.0): 
		if (randf() > 0.5):
			$FoxNoises.stop()
			$FoxNoises.play()
		refresh_fox = 0.0
	if (refresh_music1 > 60): 
		if (randf() > 0.5):
			$Music.stop()
			$Music.play()
		refresh_music1 = 0.0
